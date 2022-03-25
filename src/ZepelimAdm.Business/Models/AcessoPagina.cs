using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZepelimAdm.Business.Models
{
    public class AcessoPagina : Entity
    {
        public int AcessoId { get; set; }
        public int PaginaId { get; set; }
        public Acesso Acessos { get; set; }
        public Pagina Paginas { get; set; }
    }
}
