using System.Collections.Generic;
using System.Threading.Tasks;
using ZepelimAdm.Business.Interface;
using ZepelimAdm.Business.Models;
namespace ZepelimAdm.Business.Interfaces
{
    public interface IAcessoRepository : IBaseRepository<Acesso>
    {
        Task<List<Acesso>> ListAllAcessos();
        Task<Acesso> CheckIsUnique(string Descricao);
    }
}
