using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento para o banco de dados
    /// </summary>
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("TAREFA");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("ID");

            builder.Property(t => t.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(t => t.DataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            builder.Property(t => t.CategoriaId)
                .HasColumnName("CATEGORIAID")
                .IsRequired();

            builder.HasOne(t => t.Categoria) //Tarefa TEM 1 Categoria
                .WithMany(c => c.Tarefas) //Categoria TEM Muitas Tarefas
                .HasForeignKey(t => t.CategoriaId) //Chave estrangeira
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
