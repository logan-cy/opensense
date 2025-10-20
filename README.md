##🧠 OpenSense

**A lightweight, fluent validation and evaluation framework for building self-healing business logic.**

OpenSense lets you define logical health checks for your entities in a clear, composable way.
It’s inspired by autonomic systems — software that can detect, diagnose, and heal its own invariants.
```cs
var client = new Client();

var result = new EvaluationResult();

EvaluationService
    .Evaluate(client)
    .Check(c => c.Contract != null, "Client contract missing.", "/clients/contract", 1)
    .Check(c => c.Members.Any(), "No active members found.", "/clients/members", 2)
    .Build(result);

if (!result.IsValid)
{
    Console.WriteLine("Client invalid:");
    foreach (var issue in result.Issues)
        Console.WriteLine($"- {issue.Message}");
}
```
##✨ Features

- Generic evaluator pattern – works with any class
- Fluent API for logical rule chaining
- Priority-ordered issue reporting
- Perfect foundation for self-healing or monitoring systems
- Zero dependencies

##🚀 Install
```bash
dotnet add package OpenSense
```

##🧩 License
MIT—free for personal and commercial use.
