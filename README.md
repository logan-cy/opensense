## 🧠 OpenSense

**A lightweight, fluent validation and evaluation framework for building self-healing business logic.**

![NuGet](https://img.shields.io/nuget/v/OpenSense)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)

OpenSense lets you define logical health checks for your entities in a clear, composable way.
It’s inspired by autonomic systems — software that can detect, diagnose, and heal its own invariants.

OpenSense offers 2 means by which to check your data; one using mutation of the result object as you
chain evaluations and one that builds the result object once all of the evaluations are done.

The difference is that without mutation, OpenSense is purely evaluating the data you give it.

### With Mutation
``` csharp
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

### Without Mutation
``` csharp
var client = new Client();
var result = EvaluationService
    .Evaluate(client)
    .Check(c => c.Contract != null, "Client contract missing.", "/clients/contract", 1)
    .Check(c => c.Members.Any(), "No active members found.", "/clients/members", 2)
    .Build();

if (!result.IsValid)
{
    Console.WriteLine("Client invalid:");
    foreach (var issue in result.Issues)
        Console.WriteLine($"- {issue.Message}");
}
```

## 💊 Self-Healing Example

OpenSense can not only validate but also help keep your data healthy by automatically applying fixes based on rule evaluation.

``` csharp
var client = await context.Clients
    .Include(c => c.Contract)
    .Include(c => c.Members)
    .FirstOrDefaultAsync(c => c.Id == clientId);

var result = new EvaluationResult();

EvaluationService
    .Evaluate(client)
    .Check(c => c.Contract != null, "Client contract missing.", "/clients/contract", 1)
    .Check(c => c.Members.Any(), "No active members found.", "/clients/members", 2)
    .Build(result);

// Self-heal: update the client IsActive flag based on evaluation
client.IsActive = result.IsValid;
await context.SaveChangesAsync();
```

## ✨ Features

- Generic evaluator pattern – works with any class
- Fluent API for logical rule chaining
- Priority-ordered issue reporting
- Perfect foundation for self-healing or monitoring systems
- Zero dependencies

## 🚀 Install
``` bash
dotnet add package OpenSense
```

## 🧩 License
MIT—free for personal and commercial use.