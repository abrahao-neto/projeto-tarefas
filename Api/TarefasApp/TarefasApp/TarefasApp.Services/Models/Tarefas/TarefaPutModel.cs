namespace TarefasApp.Services.Models.Tarefas
{
    /// <summary>
    /// Modelo de dados para requisição de edição de tarefa
    /// </summary>
    public class TarefaPutModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public Guid? CategoriaId { get; set; }
    }
}
