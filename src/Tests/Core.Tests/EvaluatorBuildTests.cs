using OpenSense.Tests.TestData;

namespace OpenSense.Tests;

public class EvaluatorBuildTests
{
    [Fact]
    public void Build_ShouldReturnValidResult_WhenNoIssuesAdded()
    {
        var client = new Client();
        var evaluator = EvaluationService.Evaluate(client);

        var result = evaluator.Build();

        Assert.NotNull(result);
        Assert.True(result.IsValid);
        Assert.Empty(result.Issues);
    }

    [Fact]
    public void Build_ShouldReturnResultWithIssues_InPriorityOrder()
    {
        var client = new Client();
        var evaluator = EvaluationService.Evaluate(client)
            .Check(c => c.Contract != null, "Contract missing", "/fix", priority: 2)
            .Check(c => c.Members.Any(), "No members", "/fix", priority: 1);

        var result = evaluator.Build();

        Assert.False(result.IsValid);
        Assert.Equal(2, result.Issues.Count);
        Assert.Equal("No members", result.Issues[0].Message);
        Assert.Equal("Contract missing", result.Issues[1].Message);
    }
}
