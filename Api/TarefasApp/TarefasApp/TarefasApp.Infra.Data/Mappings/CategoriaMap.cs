using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento para o banco de dados
    /// </summary>
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("CATEGORIA");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID");

            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
