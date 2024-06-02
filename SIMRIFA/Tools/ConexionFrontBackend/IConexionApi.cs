namespace SIMRIFA.Tools.ConexionFrontBackend
{
    public interface IConexionApi<TEntity, TObject> where TEntity : class where TObject : class
    {
        Task<IEnumerable<TEntity>> GetAll(string url);
        Task<HttpResponseMessage> GetResponse(string url);
        Task<TEntity> GetByItem(string url);
        Task<TEntity> Post(string url, TObject model);
        Task<string> PostFile(string url, TObject model, string nombre, string rutaParcial);
        Task<IEnumerable<TEntity>> PostAll(string url, TObject model);
        Task<TEntity> Put(string url, TObject model);
        Task<TEntity> Delete(string url, TObject model);
    }
}
