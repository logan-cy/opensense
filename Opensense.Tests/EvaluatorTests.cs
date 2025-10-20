using OpenSense;
using OpenSense.DTO;
using OpenSense.Tests.TestData;

namespace Opensense.Tests;

public class EvaluatorTests
{
    [Fact]
    public void Check_ShouldAddIssue_WhenConditionFails()
    {
        var client = new Client();
        var evaluator = EvaluationService.Evaluate(client);

        var result = new EvaluationResult();
        evaluator.Check(c => c.Contract != null, "Contract missing", "/fix").Build(result);

        Assert.False(result.IsValid);
        Assert.Single(result.Issues);
        Assert.Equal("Contract missing", result.Issues[0].Message);
    }

    [Fact]
    public void CheckShouldNotAddIssue_WhenConditionPasses()
    {
        var client = new Client { Contract = new() };
        var evaluator = EvaluationService.Evaluate(client);

        var result = new EvaluationResult();
        evaluator.Check(c => c.Contract != null, "Contract missing", "/fix").Build(result);

        Assert.True(result.IsValid);
        Assert.Empty(result.Issues);
    }
}
