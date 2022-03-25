using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZepelimAdm.Business.Models
{
    public class Pagina : Entity
    {
        public string Descricao { get; set; }
        public string URL { get; set; }
        public int Ordem { get; set; }
        public string Icone { get; set; }
        public int PaginaPaiId { get; set; }
        public bool Status { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public virtual List<AcessoPagina> AcessoPaginas { get; set; }
    }
}
