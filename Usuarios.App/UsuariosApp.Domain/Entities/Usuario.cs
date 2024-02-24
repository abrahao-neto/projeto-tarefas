namespace UsuariosApp.Domain.Entities
{
    /// <summary>
    /// Modelo de entidade de domínio
    /// </summary>
    public class Usuario
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}
