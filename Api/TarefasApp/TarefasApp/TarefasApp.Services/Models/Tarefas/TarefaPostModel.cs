namespace TarefasApp.Services.Models.Tarefas
{
    /// <summary>
    /// Modelo de dados para requisição de cadastro de tarefa
    /// </summary>
    public class TarefaPostModel
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public Guid? CategoriaId { get; set; }
    }
}
