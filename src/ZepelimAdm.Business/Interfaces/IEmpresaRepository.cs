using System.Collections.Generic;
using System.Threading.Tasks;
using ZepelimAdm.Business.Interface;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Business.Interfaces
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        Task<List<Empresa>> ListAllEmpresas();
        Task<Empresa> CheckIsUnique(string Documento);
    }
}
