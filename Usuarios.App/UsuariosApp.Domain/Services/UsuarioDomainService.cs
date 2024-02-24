using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository? _repository;

        public UsuarioDomainService(IUsuarioRepository? repository)
        {
            _repository = repository;
        }

        public async Task Criar(Usuario usuario)
        {
            if (await _repository.Find(usuario.Email) != null)
                throw new ApplicationException("O email informado já está cadastrado.");

            await _repository.Add(usuario);
        }

        public async Task<Usuario> Autenticar(string email, string senha)
        {
            var usuario = await _repository.Find(email, senha);
            if (usuario == null)
                throw new ApplicationException("Usuário não encontrado.");

            return usuario;
        }
    }
}
