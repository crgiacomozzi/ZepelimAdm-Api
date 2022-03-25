using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZepelimAdm.Business.Models
{
    public class Empresa : Entity
    {
        public string Descricao { get; set; }
        public string Documento { get; set; }
        public string ConnectionString { get; set; }
        public virtual List<EmpresaUser> EmpresaUsuarios { get; set; }
        public virtual  List<Acesso> Acesso { get; set; }
        public virtual List<EmpresaProduto> EmpresaProdutos { get; set; }
    }
}
