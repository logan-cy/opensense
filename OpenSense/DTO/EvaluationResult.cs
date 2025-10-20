namespace OpenSense.DTO;

public class EvaluationResult
{
    public IReadOnlyList<EvaluationIssue> Issues { get; private set; } = [];
    public bool IsValid => !Issues.Any();

    public EvaluationResult() { }

    public EvaluationResult(IEnumerable<EvaluationIssue> issues)
    {
        Issues = issues.OrderBy(i => i.Priority).ToList();
    }

    internal void Populate(IEnumerable<EvaluationIssue> issues)
    {
        Issues = issues.OrderBy(i => i.Priority).ToList();
    }
}