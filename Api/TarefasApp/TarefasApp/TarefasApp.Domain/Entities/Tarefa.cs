namespace TarefasApp.Domain.Entities
{
    /// <summary>
    /// Modelo de entidade
    /// </summary>
    public class Tarefa
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public Guid? CategoriaId { get; set; }

        #region Relacionamentos

        public Categoria? Categoria { get; set; }

        #endregion
    }
}
