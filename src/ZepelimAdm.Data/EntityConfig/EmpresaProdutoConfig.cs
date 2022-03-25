using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Data.EntityConfig
{
    public class EmpresaProdutoConfig : IEntityTypeConfiguration<EmpresaProduto>
    {
        public void Configure(EntityTypeBuilder<EmpresaProduto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Guid).IsUnique();

            builder.HasOne(p => p.Empresa)
                .WithMany(a => a.EmpresaProdutos)
                .HasForeignKey(p => p.EmpresaId);

            builder.HasOne(p => p.Produto)
                .WithMany(a => a.EmpresaProdutos)
                .HasForeignKey(p => p.ProdutoId);

            builder.Property(p => p.ConnectionString);

            builder.ToTable("empresa_produto");
        }
    }
}
