using System.Collections.Generic;
using System.Threading.Tasks;
using ZepelimAdm.Business.Interface;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Business.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> ListAllUsuarios();
        Task<User> CheckIsUnique(string Email);
    }
}
