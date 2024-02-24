namespace Usuarios.App.Services.Models
{
    /// <summary>
    /// Modelo de dados da resposta do serviço de autenticação dee usuário
    /// </summary>
    public class AutenticarUsuarioResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraAcesso { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? DataHoraExpiracao { get; set; }
    }
}
