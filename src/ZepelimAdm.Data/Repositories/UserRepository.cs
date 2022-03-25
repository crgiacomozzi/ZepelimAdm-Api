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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }
        public virtual async Task<List<User>> ListAllUsuarios()
        {
            return await DbSet
                .Where(usr => !usr.Removido)
               .AsNoTracking()
               .ToListAsync();
        }

        public virtual async Task<User> CheckIsUnique(string Email)
        {
            var query = DbSet.Where(usr => usr.Email == Email);

            return await query.FirstOrDefaultAsync();
        }
    }
}
