using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.ViewModels
{
    public class AcessoPaginaViewModel
    {
        public int AcessoId { get; set; }
        public int PaginaId { get; set; }
        public Acesso Acessos { get; set; }
        public Pagina Paginas { get; set; }

    }
}
