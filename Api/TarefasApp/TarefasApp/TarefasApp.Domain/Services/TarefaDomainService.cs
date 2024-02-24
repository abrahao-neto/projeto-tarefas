using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class TarefaDomainService : ITarefaDomainService
    {
        private readonly ITarefaRepository? _tarefaRepository;

        public TarefaDomainService(ITarefaRepository? tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public void Add(Tarefa tarefa)
        {
            _tarefaRepository?.Add(tarefa);
        }

        public void Update(Tarefa tarefa)
        {
            _tarefaRepository?.Update(tarefa);
        }

        public void Delete(Tarefa tarefa)
        {
            _tarefaRepository?.Delete(tarefa);
        }

        public List<Tarefa> GetAll(DateTime dataMin, DateTime dataMax)
        {
            return _tarefaRepository?.GetAll(dataMin, dataMax);
        }

        public Tarefa GetById(Guid id)
        {
            return _tarefaRepository?.GetById(id);
        }
    }
}
