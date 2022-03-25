using System.Collections.Generic;
using System.Threading.Tasks;
using ZepelimAdm.Business.Interface;
using ZepelimAdm.Business.Models;
namespace ZepelimAdm.Business.Interfaces
{
    public interface IAcessoPaginaRepository : IBaseRepository<AcessoPagina>
    {
        Task<List<AcessoPagina>> ListAllAcessoPagina();
        Task<AcessoPagina> CheckIsUnique(int AcessoId, int PaginaId);
    }
}
