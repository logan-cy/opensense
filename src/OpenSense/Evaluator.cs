using OpenSense.DTO;

namespace OpenSense;

public class Evaluator<T> where T : class
{
	public T Entity { get; }
	private readonly List<EvaluationIssue> _issues = [];
	private readonly Lock _issuesLock = new();

	public Evaluator(T entity)
	{
		Entity = entity ?? throw new ArgumentNullException(nameof(entity));
	}

	public Evaluator<T> Check(
		Func<T, bool> condition,
		string message,
		string fixUrl,
		int priority = 10)
	{
		if (condition == null) throw new ArgumentException("Check condition cannot be null.");

		if (!condition(Entity))
			AddIssue(message, fixUrl, priority);

		return this;
	}

	public Evaluator<T> Check(Func<T, bool> condition, string message)
    {
        if (condition == null) throw new ArgumentException("Check condition cannot be null.");

        if (!condition(Entity))
			AddIssue(message);

		return this;
	}

	public async Task<Evaluator<T>> CheckAsync(
		Func<T, CancellationToken, Task<bool>> conditionAsync,
		string message,
		string fixUrl,
		int priority = 10,
		CancellationToken cancellationToken = default)
    {
        if (conditionAsync == null) throw new ArgumentException("Check condition cannot be null.");

        if (!await conditionAsync(Entity, cancellationToken).ConfigureAwait(false))
			AddIssue(message, fixUrl, priority);

		return this;
	}

	private void AddIssue(string message, string fixUrl = "", int priority = 10)
	{
		var issue = new EvaluationIssue()
		{
			Message = message,
			FixUrl = fixUrl,
			Priority = priority
		};

		lock (_issuesLock)
		{
			_issues.Add(issue);
		}
	}

	public void Build(EvaluationResult result)
	{
		if (result == null) throw new ArgumentException("Evaluation result parameter value cannot be null.");
		result.Populate(_issues);
	}

	public EvaluationResult Build()
	{
		return new EvaluationResult(_issues);
	}
}