using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAuth.Database.Repository;
using ZepelimAdm.Business.Interfaces;
using ZepelimAdm.Business.Models;
using ZepelimAdm.Database;

namespace ZepelimAdm.Data.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(Context context) : base(context) { }
        public virtual async Task<List<Produto>> ListAllProdutos()
        {
            return await DbSet
                .Where(pro => !pro.Removido)
               .AsNoTracking()
               .ToListAsync();
        }
        public virtual async Task<List<Produto>> FindByDescricao(string descricao)
        {
            return await DbSet
                .Where(pro => !pro.Removido && pro.Descricao == descricao)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
