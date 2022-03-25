using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZepelimAdm.Business.Models
{
    public class Acesso : Entity
    {
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public int? EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public virtual List<User> Usuario { get; set; }
    }
}
