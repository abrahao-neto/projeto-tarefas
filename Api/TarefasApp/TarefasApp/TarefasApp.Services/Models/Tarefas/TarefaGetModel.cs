using TarefasApp.Services.Models.Categorias;

namespace TarefasApp.Services.Models.Tarefas
{
    /// <summary>
    /// Modelo de dados que será retornado na consulta de tarefas
    /// </summary>
    public class TarefaGetModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public CategoriaGetModel? Categoria { get; set; }
    }
}
