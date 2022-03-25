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
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(Context context) : base(context) { }
        public virtual async Task<List<Empresa>> ListAllEmpresas()
        {
            return await DbSet
                .Where(pro => !pro.Removido)
               .AsNoTracking()
               .ToListAsync();
        }

        public virtual async Task<Empresa> CheckIsUnique(string Documento)
        {
            var query = DbSet.Where(emp => emp.Documento == Documento);

            return await query.FirstOrDefaultAsync();
        }

    }
}
