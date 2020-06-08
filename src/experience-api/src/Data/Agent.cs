using Doctrina.ExperienceApi.Data.Exceptions;
using Doctrina.ExperienceApi.Data.Helpers;
using Doctrina.ExperienceApi.Data.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Doctrina.ExperienceApi.Data
{
    /// <summary>
    /// An Agent (an individual) is a persona or system.
    /// </summary>
    public class Agent : StatementObjectBase, IInvenseFunctionalIdenfitiers, IAgent, IStatementObject
    {
        protected override ObjectType OBJECT_TYPE => ObjectType.Agent;

        public Agent() : base() { }
        public Agent(JsonString jsonString) : this(jsonString.ToJToken(), ApiVersion.GetLatest()) { }
        public Agent(JToken jobj, ApiVersion version) : base(jobj, version)
        {
            GuardType(jobj, JTokenType.Object);

            var objectType = jobj["objectType"];
            if (objectType != null)
            {
                GuardType(objectType, JTokenType.String);
                ParseObjectType(objectType, OBJECT_TYPE);
            }

            var name = jobj["name"];
            if (name != null)
            {
                GuardType(name, JTokenType.String);
                Name = jobj.Value<string>("name");
            }

            var mbox = jobj["mbox"];
            if (mbox != null)
            {
                GuardType(mbox, JTokenType.String);
                try
                {
                    Mbox = new Mbox(mbox.Value<string>());
                }
                catch (MboxFormatException ex)
                {
                    throw new JsonTokenModelException(mbox, ex);
                }
            }

            var mbox_sha1sum = jobj["mbox_sha1sum"];
            if (mbox_sha1sum != null)
            {
                GuardType(mbox_sha1sum, JTokenType.String);
                Mbox_SHA1SUM = mbox_sha1sum.Value<string>();
            }

            var openid = jobj["openid"];
            if (openid != null)
            {
                GuardType(openid, JTokenType.String);
                try
                {
                    OpenId = new Iri(openid.Value<string>());
                }
                catch (IriFormatException ex)
                {
                    throw new JsonTokenModelException(openid, ex);
                }
            }

            var account = jobj["account"];
            if (account != null)
            {
                GuardType(account, JTokenType.Object);
                Account = new Account(account, version);
            }
        }

        /// <summary>
        /// Agent. This property is optional except when the Agent is used as a Statement's object.
        /// </summary>
        public new ObjectType ObjectType { get { return OBJECT_TYPE; } }

        /// <summary>
        /// Full name of the Agent. (Optional)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The required format is "mailto:email address".
        /// Only email addresses that have only ever been and will ever be assigned to this Agent, but no others, SHOULD be used for this property and mbox_sha1sum.
        /// </summary>
        public Mbox Mbox { get; set; }

        /// <summary>
        /// The hex-encoded SHA1 hash of a mailto IRI (i.e. the value of an mbox property). An LRS MAY include Agents with a matching hash when a request is based on an mbox.
        /// </summary>
        public string Mbox_SHA1SUM { get; set; }

        /// <summary>
        /// An openID that uniquely identifies the Agent.
        /// </summary>
        public Iri OpenId { get; set; }

        /// <summary>
        /// A user account on an existing system e.g. an LMS or intranet.
        /// </summary>
        public Account Account { get; set; }

        public override JToken ToJToken(ApiVersion version, ResultFormat format)
        {
            var jobj = base.ToJToken(version, format);

            if (Name != null)
            {
                jobj["name"] = Name;
            }

            if (Mbox != null)
            {
                jobj["mbox"] = Mbox.ToString();
            }

            if (Mbox_SHA1SUM != null)
            {
                jobj["mbox_sha1sum"] = Mbox_SHA1SUM;
            }

            if (OpenId != null)
            {
                jobj["openid"] = OpenId.ToString();
            }

            if (Account != null)
            {
                jobj["account"] = Account.ToJToken(version, format);
            }

            return jobj;
        }

        public bool IsAnonymous()
        {
            return (Mbox == null
                && string.IsNullOrEmpty(Mbox_SHA1SUM)
                && Account == null
                && OpenId == null);
        }

        public bool IsIdentified()
        {
            return !IsAnonymous();
        }

        public List<string> GetIdentifiersByName()
        {
            var ids = new List<string>();
            if (Mbox != null)
            {
                ids.Add(nameof(Mbox).ToLower());
            }

            if (!string.IsNullOrEmpty(Mbox_SHA1SUM))
            {
                ids.Add(nameof(Mbox_SHA1SUM));
            }

            if (Account != null)
            {
                ids.Add(nameof(Account));
            }

            if (OpenId != null)
            {
                ids.Add(nameof(OpenId));
            }

            return ids;
        }

        /// <summary>
        /// Compute IFI hash
        /// </summary>
        public string ComputeHash()
        {
            Func<string, string> computeHash = SHAHelper.SHA1.ComputeHash;

            if (Mbox != null)
            {
                return computeHash(Mbox.ToString());
            }

            if (!string.IsNullOrWhiteSpace(Mbox_SHA1SUM))
            {
                // We need to re-compute for mbox and mbox_sha1sum not being equal.
                return computeHash(Mbox_SHA1SUM);
            }

            if (OpenId != null)
            {
                return OpenId.ComputeHash();
            }

            if (Account != null)
            {
                var uriBuilder = new UriBuilder(Account.HomePage)
                {
                    UserName = Account.Name
                };
                return computeHash(uriBuilder.ToString());
            }

            if (ObjectType == ObjectType.Group)
            {
                // Is anonymous group, generate unique id
                return computeHash(Guid.NewGuid().ToString());
            }

            throw new InvalidOperationException("Cannot compute hash for an Agent without identifier.");
        }

        public override bool Equals(object obj)
        {
            var agent = obj as Agent;
            return agent != null &&
                   base.Equals(obj) &&
                   ObjectType == agent.ObjectType &&
                   Name == agent.Name &&
                   EqualityComparer<Mbox>.Default.Equals(Mbox, agent.Mbox) &&
                   Mbox_SHA1SUM == agent.Mbox_SHA1SUM &&
                   EqualityComparer<Iri>.Default.Equals(OpenId, agent.OpenId) &&
                   EqualityComparer<Account>.Default.Equals(Account, agent.Account);
        }

        public override int GetHashCode()
        {
            var hashCode = -790879124;
            hashCode = hashCode * -1521134295 + ObjectType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Mbox>.Default.GetHashCode(Mbox);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Mbox_SHA1SUM);
            hashCode = hashCode * -1521134295 + EqualityComparer<Iri>.Default.GetHashCode(OpenId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Account>.Default.GetHashCode(Account);
            return hashCode;
        }

        public static bool operator ==(Agent agent1, Agent agent2)
        {
            return EqualityComparer<Agent>.Default.Equals(agent1, agent2);
        }

        public static bool operator !=(Agent agent1, Agent agent2)
        {
            return !(agent1 == agent2);
        }
    }
}
