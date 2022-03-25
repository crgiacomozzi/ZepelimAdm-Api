using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.ViewModels
{
    public class EmpresaProdutoViewModel
    {
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public string ConnectionString { get; set; }
    }
}
