using Microsoft.EntityFrameworkCore;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Infra.Data.Mappings;

namespace UsuariosApp.Infra.Data.Contexts
{
    /// <summary>
    /// Classe de contexto para conexão com o banco de dados
    /// </summary>
    public class DataContext : DbContext
    {
        //Método para definir o tipo de conexão de banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDUsuariosApp;Integrated Security=True;");
        }

        //método para incluir as classes de mapeamento ORM
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

        //DbSet -> Permitir gravar, alterar, excluir e consultar usuários
        public DbSet<Usuario> Usuario { get; set; }
    }
}
