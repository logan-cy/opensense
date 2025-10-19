namespace OpenSense;

public static class EvaluationService
{
    public static Evaluator<T> Evaluate<T>(T entity) where T : class => new(entity);
}
