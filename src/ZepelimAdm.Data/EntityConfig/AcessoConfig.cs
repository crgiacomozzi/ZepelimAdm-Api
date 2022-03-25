using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Data.EntityConfig
{
    public class AcessoConfig : IEntityTypeConfiguration<Acesso>
    {
        public void Configure(EntityTypeBuilder<Acesso> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Guid).IsUnique();

            builder.Property(p => p.Descricao);
            
            builder.Property(p => p.Status);

            builder.HasOne(p => p.Empresa)
                .WithMany(a => a.Acesso)
                .HasForeignKey(p => p.EmpresaId);

            builder.HasMany<User>(p => p.Usuario)
                .WithOne(a => a.Acesso)
                .HasForeignKey(p => p.AcessoId);

            builder.ToTable("acesso");
        }
    }
}
