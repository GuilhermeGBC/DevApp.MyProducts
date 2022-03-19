using System;

namespace DevApp.Business.Core.Models
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            Id = Guid.NewGuid();
        }

        protected Entity()
        {

        }

        public Guid Id { get; set; }


    }
}
