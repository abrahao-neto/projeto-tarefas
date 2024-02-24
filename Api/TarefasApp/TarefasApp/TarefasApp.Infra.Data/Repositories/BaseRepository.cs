using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Infra.Data.Contexts;

namespace TarefasApp.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório genérico do EntityFramework
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        public void Add(TEntity entity)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(entity);
                dataContext.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(entity);
                dataContext.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Remove(entity);
                dataContext.SaveChanges();
            }
        }

        public List<TEntity> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<TEntity>().ToList(); 
            }
        }

        public TEntity GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<TEntity>().Find(id);
            }
        }
    }
}
