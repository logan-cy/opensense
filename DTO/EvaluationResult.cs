namespace OpenSense.DTO;

public class EvaluationResult
{
    public bool IsValid { get; set; }
    public List<EvaluationIssue> Issues { get; set; } = [];
}
