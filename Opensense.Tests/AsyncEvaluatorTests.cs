using OpenSense.DTO;
using OpenSense.Tests.TestData;

namespace OpenSense.Tests;

public class AsyncEvaluatorTests
{
    [Fact]
    public async Task Check_ShouldAddIssue_WhenConditionFailsAsync()
    {
        var client = new Client();

        var result = new EvaluationResult();
        var evaluator = await EvaluationService
            .Evaluate(client)
            .CheckAsync(async (c, ct) =>
            {
                // simulate async work
                await Task.Delay(10, ct);
                return c.Contract != null;
            },
            "Client contract missing.",
            "/clients/contract",
            priority: 1);


        evaluator.Build(result);

        Assert.False(result.IsValid);
        Assert.Single(result.Issues);
        Assert.Equal("Client contract missing.", result.Issues[0].Message);
    }

    [Fact]
    public async Task CheckShouldNotAddIssue_WhenConditionPassesAsync()
    {
        var client = new Client { Contract = new() };

        var result = new EvaluationResult();
        var evaluator = await EvaluationService
            .Evaluate(client)
            .CheckAsync(async (c, ct) =>
            {
                // simulate async work
                await Task.Delay(10, ct);
                return c.Contract != null;
            },
            "Client contract missing.",
            "/clients/contract",
            priority: 1);

        evaluator.Build(result);

        Assert.True(result.IsValid);
        Assert.Empty(result.Issues);
    }
}
