using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ObmultichoiceRetailer.Domain.Abstractions
{
    public abstract class BaseDomainModel : IDomainModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Key]
        public int Id { get; set; }

        public string CreatedBy { get; set; } = null!;

        public string UpdatedBy { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
