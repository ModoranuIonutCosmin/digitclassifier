using System;
using System.ComponentModel.DataAnnotations;

namespace DataAcces.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
