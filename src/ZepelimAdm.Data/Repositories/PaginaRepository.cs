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
    public class PaginaRepository : BaseRepository<Pagina>, IPaginaRepository
    {
        public PaginaRepository(Context context) : base(context) { }
        public virtual async Task<List<Pagina>> ListAllPaginas()
        {
            return await DbSet
                .Where(pag => !pag.Removido)
               .AsNoTracking()
               .ToListAsync();
        }
        public virtual async Task<Pagina> CheckIsUnique(string Descricao)
        {
            var query = DbSet.Where(emp => emp.Descricao == Descricao);

            return await query.FirstOrDefaultAsync();
        }
    }
}
