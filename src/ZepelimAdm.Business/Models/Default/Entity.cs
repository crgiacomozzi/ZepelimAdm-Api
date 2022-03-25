using System;

namespace ZepelimAdm.Business.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public bool Removido { get; set; }
        public Entity()
        {
            Guid = Guid.NewGuid();
        }
    }
}
