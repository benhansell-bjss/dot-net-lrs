﻿using FluentValidation;
using Microsoft.Azure.KeyVault.Cryptography.Algorithms;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Doctrina.ExperienceApi.Data.Validation
{
    public class AttachmentValidator : AbstractValidator<Attachment>
    {
        public AttachmentValidator()
        {
            RuleFor(x => x.UsageType).NotEmpty();
            RuleFor(x => x.Display).NotEmpty().SetValidator(new LanguageMapValidator());
            RuleFor(x => x.Description).SetValidator(new LanguageMapValidator()).When(x => x.Description != null);
            RuleFor(x => x.ContentType).NotEmpty();
            RuleFor(x => x.Length).NotEmpty();
            RuleFor(x => x.SHA2).NotEmpty();
            RuleFor(x => x.Payload)
                .NotEmpty()
                .When(x => x.FileUrl == null)
                .WithMessage(p => $"Attachment payload cannot be empty, when fileUrl is also empty.");

            // Signed Statement rules
            RuleFor(x => x.ContentType).Equal("application/octet-stream")
                .When(x => x.UsageType == new Iri("http://adlnet.gov/expapi/attachments/signature"));
        }
    }
}
