using OpenSense.DTO;
using System.Security.Cryptography.X509Certificates;

namespace OpenSense;

public class Evaluator<T> where T : class
{
    private readonly T _entity;
    private readonly List<EvaluationIssue> _issues = [];

    public Evaluator(T entity) => _entity = entity;

    public Evaluator<T> Check(
        Func<T, bool> condition,
        string message,
        string fixUrl,
        int priority = 10)
    {
        if (!condition(_entity))
        {
            _issues.Add(new EvaluationIssue
            {
                Message = message,
                FixUrl = fixUrl,
                Priority = priority
            });
        }
        return this;
    }

    public void Build(EvaluationResult result)
    {
        result.Issues = _issues.OrderBy(i => i.Priority).ToList();
        result.IsValid = result.Issues.Count == 0;
    }
}