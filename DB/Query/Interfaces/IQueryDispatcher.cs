namespace DB.Query.Interfaces
{
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Método <c>Dispatch</c> despacha el request según el handler adecuado. Se debe especificar en 
        /// los genericos los TQuery y TResult.
        /// <example>
        /// Por ejemplo:
        /// dispatcher.Dispatch&lt;TQuery,TResult&gt;(query,cancellation); Donde query es del tipo TQuery.
        /// </example>
        /// </summary>
        Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation);
    }
}
