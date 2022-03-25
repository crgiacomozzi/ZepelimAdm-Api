using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Data.EntityConfig
{
    public class AcessoPaginaConfig : IEntityTypeConfiguration<AcessoPagina>
    {
        public void Configure(EntityTypeBuilder<AcessoPagina> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Guid).IsUnique();

            builder.ToTable("acesso_pagina");
        }
    }
}
