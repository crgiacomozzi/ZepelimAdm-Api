using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Data.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Guid).IsUnique();

            builder.Property(p => p.Nome);

            builder.Property(p => p.Email);

            builder.Property(p => p.Role);

            builder.HasOne(p => p.Acesso)
                .WithMany(a => a.Usuario)
                .HasForeignKey(p => p.AcessoId);

            builder.ToTable("usuario");
        }
    }
}
