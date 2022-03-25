using System.Collections.Generic;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.ViewModels
{
    public class PaginaViewModel
    {
        public string Descricao { get; set; }
        public string URL { get; set; }
        public int Ordem { get; set; }
        public string Icone { get; set; }
        public int PaginaPaiId { get; set; }
        public bool Status { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public List<AcessoPagina> AcessoPaginas { get; set; }

    }
}
