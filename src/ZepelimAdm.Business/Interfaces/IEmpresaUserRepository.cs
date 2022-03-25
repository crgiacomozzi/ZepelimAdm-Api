using System.Collections.Generic;
using System.Threading.Tasks;
using ZepelimAdm.Business.Interface;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Business.Interfaces
{
    public interface IEmpresaUserRepository : IBaseRepository<EmpresaUser>
    {
        Task<List<EmpresaUser>> ListAllUsuariosPorEmpresa();
        Task<EmpresaUser> CheckIsUnique(int EmpresaId, int UsuarioId);
    }
}
