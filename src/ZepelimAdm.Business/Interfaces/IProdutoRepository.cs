using System.Collections.Generic;
using System.Threading.Tasks;
using ZepelimAdm.Business.Interface;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Business.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<List<Produto>> ListAllProdutos();
        Task<List<Produto>> FindByDescricao(string descricao);
    }
}
