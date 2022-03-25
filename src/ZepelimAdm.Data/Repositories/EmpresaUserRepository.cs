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
    public class EmpresaUserRepository : BaseRepository<EmpresaUser>, IEmpresaUserRepository
    {
        public EmpresaUserRepository(Context context) : base(context) { }
        public virtual async Task<List<EmpresaUser>> ListAllUsuariosPorEmpresa()
        {
            return await DbSet
                .Where(emp => !emp.Removido)
               .AsNoTracking()
               .ToListAsync();
        }

        public virtual async Task<EmpresaUser> CheckIsUnique(int EmpresaId, int UsuarioId)
        {
            var query = DbSet.Where(exu => exu.EmpresaId == EmpresaId && exu.UsuarioId == UsuarioId);

            return await query.FirstOrDefaultAsync();
        }

    }
}
