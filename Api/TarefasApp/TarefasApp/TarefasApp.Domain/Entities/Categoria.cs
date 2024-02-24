namespace TarefasApp.Domain.Entities
{
    /// <summary>
    /// Modelo de entidade
    /// </summary>
    public class Categoria
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        #region Relacionamentos

        public List<Tarefa>? Tarefas { get; set; }

        #endregion
    }
}
