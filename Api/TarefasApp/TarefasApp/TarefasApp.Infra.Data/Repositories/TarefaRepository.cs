using Microsoft.EntityFrameworkCore;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Infra.Data.Contexts;

namespace TarefasApp.Infra.Data.Repositories
{
    /// <summary>
    /// Classe de repositório de dados para Tarefa
    /// </summary>
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        public List<Tarefa> GetAll(DateTime dataMin, DateTime dataMax)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Tarefa>()
                    .Include(t => t.Categoria)
                    .Where(t => t.DataHora >= dataMin && t.DataHora <= dataMax)
                    .OrderBy(t => t.DataHora)
                    .ToList();
            }
        }
    }
}
