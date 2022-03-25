using System.Collections.Generic;
using System.Threading.Tasks;
using ZepelimAdm.Business.Interface;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Business.Interfaces
{
    public interface IEmpresaProdutoRepository : IBaseRepository<EmpresaProduto>
    {
        Task<List<EmpresaProduto>> ListAllEmpresaProduto();
        Task<EmpresaProduto> CheckIsUnique(int EmpresaId, int UsuarioId);
    }
}
