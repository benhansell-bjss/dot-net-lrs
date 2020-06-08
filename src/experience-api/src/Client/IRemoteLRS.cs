using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Client
{
    public interface ILRSClient
    {
        Task<About> GetAbout(CancellationToken cancellaton = default);
        Task<Statement> SaveStatement(Statement statement, CancellationToken cancellaton = default);
        Task PutStatement(Statement statement, CancellationToken cancellaton = default);
        Task<Statement[]> SaveStatements(Statement[] statement, CancellationToken cancellaton = default);
        Task<Statement> GetStatement(Guid id, bool attachments, ResultFormat format, CancellationToken cancellaton = default);
        Task<Statement> GetVoidedStatement(Guid id, bool attachments, ResultFormat format, CancellationToken cancellaton = default);
        Task<StatementsResult> QueryStatements(StatementsQuery query, CancellationToken cancellaton = default);
        Task<StatementsResult> MoreStatements(StatementsResult result, CancellationToken cancellaton = default);
        Task<Statement> VoidStatement(Guid id, Agent agent, CancellationToken cancellaton = default);

        Task<Guid[]> GetStateIds(Iri activityId, Agent agent, Guid? registration = null, CancellationToken cancellaton = default);
        Task<ActivityStateDocument> GetState(string id, Iri activityId, Agent agent, Guid? registration = null, CancellationToken cancellaton = default);
        Task SaveState(ActivityStateDocument state, ETagMatch? matchType = null, CancellationToken cancellaton = default);
        Task DeleteState(ActivityStateDocument state, ETagMatch? matchType = null, CancellationToken cancellaton = default);
        Task ClearState(Iri activityId, Agent agent, Guid? registration = null, ETagMatch? matchType = null, CancellationToken cancellaton = default);

        Task<Guid[]> GetActivityProfileIds(Iri activityId, DateTimeOffset? since = null, CancellationToken cancellaton = default);
        Task<ActivityProfileDocument> GetActivityProfile(string id, Iri activityId, CancellationToken cancellaton = default);
        Task SaveActivityProfile(ActivityProfileDocument profile, ETagMatch? matchType = null, CancellationToken cancellaton = default);
        Task DeleteActivityProfile(ActivityProfileDocument profile, ETagMatch? matchType = null, CancellationToken cancellaton = default);

        Task<Guid[]> GetAgentProfileIds(Agent agent, CancellationToken cancellaton = default);
        Task<AgentProfileDocument> GetAgentProfile(string id, Agent agent, CancellationToken cancellaton = default);
        Task SaveAgentProfile(AgentProfileDocument profile, ETagMatch? matchType = null, CancellationToken cancellaton = default);
        Task DeleteAgentProfile(AgentProfileDocument profile, ETagMatch? matchType = null, CancellationToken cancellaton = default);
    }
}