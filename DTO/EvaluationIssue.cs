namespace OpenSense.DTO;

public class EvaluationIssue
{
	public string Message { get; set; } = string.Empty;
	public string FixUrl { get; set; } = string.Empty;
	public int Priority { get; set; } = 10;
}
