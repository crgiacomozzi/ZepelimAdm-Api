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
    public class AcessoPaginaRepository : BaseRepository<AcessoPagina>, IAcessoPaginaRepository
    {
        public AcessoPaginaRepository(Context context) : base(context) { }
        public virtual async Task<List<AcessoPagina>> ListAllAcessoPagina()
        {
            return await DbSet
                .Where(usr => !usr.Removido)
               .AsNoTracking()
               .ToListAsync();
        }
        public virtual async Task<AcessoPagina> CheckIsUnique(int AcessoId, int PaginaId)
        {
            var query = DbSet.Where(axp => axp.AcessoId == AcessoId && axp.PaginaId == PaginaId);

            return await query.FirstOrDefaultAsync();
        }

    }
}
