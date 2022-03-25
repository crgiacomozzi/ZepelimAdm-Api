using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZepelimAdm.Business.Models
{
    public class Produto : Entity
    {
        public string Descricao { get; set; }
        public virtual List<EmpresaProduto> EmpresaProdutos { get; set; }
    }
}
