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
    public class AcessoRepository : BaseRepository<Acesso>, IAcessoRepository
    {
        public AcessoRepository(Context context) : base(context) { }
        public virtual async Task<List<Acesso>> ListAllAcessos()
        {
            return await DbSet
                .Where(usr => !usr.Removido)
               .AsNoTracking()
               .ToListAsync();
        }
        public virtual async Task<Acesso> CheckIsUnique(string Descricao)
        {
            var query = DbSet.Where(emp => emp.Descricao == Descricao);

            return await query.FirstOrDefaultAsync();
        }
    }
}
