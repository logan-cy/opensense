## 🔧 Changelog

### Version 1.1.0
- Expose `Issues` as `IReadOnlyList` for safer consumption
- `Build()` now optionally returns a new `EvaluationResult` for immutability
- Maintained backward compatibility with previous `Build(result)` usage

### Version 1.0.3

- Updated `Check` method in Evaluator so that it can process more complex LINQ queries. `Check` can process queries like nested `Sum` lookups.
- Added asynchronous checking with cancellation token.
