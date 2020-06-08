using Doctrina.ExperienceApi.Data.Security.Cryptography;
using FluentValidation;
using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Doctrina.ExperienceApi.Data.Json;

namespace Doctrina.ExperienceApi.Data.Validation
{
    public class StatementValidator : AbstractValidator<Statement>
    {
        public StatementValidator()
        {
            Include(new StatementBaseValidator());

            RuleFor(x => x.Object as SubStatement)
               .SetValidator(new SubStatementValidator())
               .When(x => x.Object != null && x.Object.ObjectType == ObjectType.SubStatement);

            // TODO: https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Data.md#requirements-14
            RuleFor(x => x.Authority).SetValidator(new AgentValidator())
                .When(x => x.Authority != null && x.Authority.ObjectType == ObjectType.Agent);

            RuleFor(x => x.Authority)
                .Must(auth =>
                {
                    var grp = auth as Group;
                    return grp.Member.Count == 2 && auth.IsAnonymous();
                })
                .When(x => x.Authority != null && x.Authority.ObjectType == ObjectType.Group)
                .WithMessage("When 3-legged OAuth, the anonymous group must have 2 Agents. The two Agents represent an application and user together.");

            RuleFor(x => x.Object.ObjectType)
                .Equal(ObjectType.StatementRef)
                .When(x => x.Verb?.Id?.ToString() == Verbs.Voided)
                .WithMessage("When statement verb is voided, statement object must be StatementRef.");

            RuleFor(x => x).Custom((statement, context) =>
            {
                var attachments = statement.Attachments;
                for(int i = 0; i < attachments.Count; i++)
                {
                    var attachment = attachments.ElementAt(i);
                    if(attachment.UsageType == new Iri("http://adlnet.gov/expapi/attachments/signature"))
                    {
                        if (attachment.ContentType != "application/octet-stream")
                        {
                            context.AddFailure($"Attachments[{i}].ContentType", "Must be \"application/octet-stream\"");
                            continue;
                        }

                        var jws = JsonWebSignature.Parse(Encoding.UTF8.GetString(attachment.Payload));
                        if (jws.Errors.Count() > 0)
                        {
                            string key = $"Attachment[{i}].Payload";
                            // context.AddFailure(key, "Invalid JWS Signature.");
                            foreach(var error in jws.Errors)
                            {
                                context.AddFailure(key, error.Message);
                            }
                            continue;
                        }

                        var jsonString = new JsonString(jws.Payload);
                        if(!jsonString.IsValid())
                        {
                            context.AddFailure($"Attachments[{i}].Pyload", "JWS Payload is not valid json format.");
                            continue;
                        }

                        var payloadStatement = new Statement(jsonString);
                        if (!payloadStatement.Equals(statement))
                        {
                            context.AddFailure($"Attachments[{i}].Pyload", "JWS Payload does not match the signed statement.");
                        }
                    }
                }
            });
        }
    }
}
