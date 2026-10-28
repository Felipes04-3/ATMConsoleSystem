using System;
using System.Collections.Generic;
using System.Text;

namespace ATMSystem.ATMSystem.Models
{
    abstract public class BaseEntity
    {
        internal Guid Id { get; init; }
        internal DateTime CreationDate { get; init; }


        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

    }
}
