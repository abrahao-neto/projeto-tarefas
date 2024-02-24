namespace TarefasApp.Services.Models.Categorias
{
    /// <summary>
    /// Modelo de dados que será retornado na consulta de categorias
    /// </summary>
    public class CategoriaGetModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
