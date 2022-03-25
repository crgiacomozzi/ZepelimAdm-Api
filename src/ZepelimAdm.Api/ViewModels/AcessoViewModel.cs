using System.Collections.Generic;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.ViewModels
{
    public class AcessoViewModel
    {
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public int? EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public List<AcessoPagina> AcessoPaginas { get; set; }
    }
}
