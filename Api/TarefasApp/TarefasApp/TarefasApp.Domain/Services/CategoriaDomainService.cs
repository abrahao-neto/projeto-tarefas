using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class CategoriaDomainService : ICategoriaDomainService
    {
        private readonly ICategoriaRepository? _categoriaRepository;

        public CategoriaDomainService(ICategoriaRepository? categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public List<Categoria> GetAll()
        {
            return _categoriaRepository?.GetAll();
        }

        public Categoria? GetById(Guid id)
        {
            return _categoriaRepository?.GetById(id);
        }
    }
}
