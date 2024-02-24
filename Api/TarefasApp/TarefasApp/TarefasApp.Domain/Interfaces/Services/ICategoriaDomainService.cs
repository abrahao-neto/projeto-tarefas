using TarefasApp.Domain.Entities;

namespace TarefasApp.Domain.Interfaces.Services
{
    public interface ICategoriaDomainService
    {
        List<Categoria> GetAll();
        Categoria? GetById(Guid id);
    }
}
