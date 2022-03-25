using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Data.EntityConfig
{
    public class EmpresaUserConfig : IEntityTypeConfiguration<EmpresaUser>
    {
        public void Configure(EntityTypeBuilder<EmpresaUser> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Guid).IsUnique();

            builder.HasOne(p => p.Empresas)
                .WithMany(a => a.EmpresaUsuarios)
                .HasForeignKey(p => p.EmpresaId);

            builder.HasOne(p => p.Usuario)
                .WithMany(a => a.EmpresaUsuarios)
                .HasForeignKey(p => p.UsuarioId);

            builder.ToTable("empresa_usuario");
        }
    }
}
