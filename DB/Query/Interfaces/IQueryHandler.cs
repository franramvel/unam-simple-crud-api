namespace DB.Query.Interfaces
{
    public interface IQueryHandler<in TQuery, TQueryResult>
    {
        //GetTabla, TablaResult
        Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
    }
}