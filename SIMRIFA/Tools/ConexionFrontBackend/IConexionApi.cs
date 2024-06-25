namespace SIMRIFA.Tools.ConexionFrontBackend
{
    public interface IConexionApi<TEntity, TObject> where TEntity : class where TObject : class
    {
		Task<TEntity> Delete(string url, TObject model);
		Task<IEnumerable<TEntity>> GetAll(string url);
		Task<TEntity> GetByItem(string url);
		Task<HttpResponseMessage> GetResponse(string url);
		Task<TEntity> Post(string url, TObject model);
		Task<IEnumerable<TEntity>> PostAll(string url, TObject model);
		Task<string> PostFile(string url, TObject model, string nombre, string rutaParcial);
		Task<TEntity> Put(string url, TObject model);
	}
}
