using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZepelimAdm.Business.Models
{
    public class EmpresaUser : Entity
    {
        public int EmpresaId { get; set; }
        public int UsuarioId { get; set; }
        public Empresa Empresas { get; set; }
        public User Usuario { get; set; }

    }
}
