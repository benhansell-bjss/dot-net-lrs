using System;

namespace Doctrina.ExperienceApi.Data
{
    public interface IStatementsResult
    {
        Uri More { get; set; }
        StatementCollection Statements { get; set; }
    }
}