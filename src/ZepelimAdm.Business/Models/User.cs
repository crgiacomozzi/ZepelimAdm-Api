using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZepelimAdm.Business.Models
{
    public class User : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public virtual List<EmpresaUser> EmpresaUsuarios { get; set; }
        public int AcessoId { get; set; }
        public Acesso Acesso { get; set; }
    }
}
