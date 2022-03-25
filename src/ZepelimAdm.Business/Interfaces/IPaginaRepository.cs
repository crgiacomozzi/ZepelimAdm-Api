using System.Collections.Generic;
using System.Threading.Tasks;
using ZepelimAdm.Business.Interface;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Business.Interfaces
{
    public interface IPaginaRepository : IBaseRepository<Pagina>
    {
        Task<List<Pagina>> ListAllPaginas();
        Task<Pagina> CheckIsUnique(string Documento);
    }
}
