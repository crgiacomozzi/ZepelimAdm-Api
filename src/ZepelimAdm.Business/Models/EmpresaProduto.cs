using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZepelimAdm.Business.Models
{
    public class EmpresaProduto : Entity
    {
        public int EmpresaId { get; set; }
        public Empresa Empresa {get; set;}
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public string ConnectionString { get; set; }
    }
}
