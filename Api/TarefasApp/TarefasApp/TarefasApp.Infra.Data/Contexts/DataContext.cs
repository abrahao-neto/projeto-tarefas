using Microsoft.EntityFrameworkCore;
using TarefasApp.Infra.Data.Mappings;

namespace TarefasApp.Infra.Data.Contexts
{
    /// <summary>
    /// Classe de contexto para configurar 
    /// a conexão de banco de dados do EntityFramework
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Método para mapear a conexão do banco de dados
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //mapeando um banco de dados do sqlserver
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDTarefasApp;Integrated Security=True;");
        }

        /// <summary>
        /// Método para adicionar as classes de mapeamento do banco de dados
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
        }
    }
}
