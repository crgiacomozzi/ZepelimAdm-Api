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
    public class EmpresaProdutoRepository : BaseRepository<EmpresaProduto>, IEmpresaProdutoRepository
    {
        public EmpresaProdutoRepository(Context context) : base(context) { }
        public virtual async Task<List<EmpresaProduto>> ListAllEmpresaProduto()
        {
            return await DbSet
                .Where(emp => !emp.Removido)
               .AsNoTracking()
               .ToListAsync();
        }
        public virtual async Task<EmpresaProduto> CheckIsUnique(int EmpresaId, int ProdutoId)
        {
            var query = DbSet.Where(exp => exp.EmpresaId == EmpresaId && exp.ProdutoId == ProdutoId);

            return await query.FirstOrDefaultAsync();
        }

    }
}
