using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Data.EntityConfig
{
    public class PaginaConfig : IEntityTypeConfiguration<Pagina>
    {
        public void Configure(EntityTypeBuilder<Pagina> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Guid).IsUnique();

            builder.Property(p => p.Descricao);

            builder.Property(p => p.URL);

            builder.Property(p => p.Ordem);

            builder.Property(p => p.Icone);

            builder.Property(p => p.PaginaPaiId);

            builder.Property(p => p.Status);

            builder.ToTable("pagina");
        }
    }
}
