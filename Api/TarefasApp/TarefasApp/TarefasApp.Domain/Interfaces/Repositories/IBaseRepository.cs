namespace TarefasApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface genérica para criação dos métodos do repositório
    /// </summary>
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        List<TEntity> GetAll();
        TEntity GetById(Guid id);
    }
}
