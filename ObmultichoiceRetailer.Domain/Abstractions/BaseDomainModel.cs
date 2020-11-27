using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ObmultichoiceRetailer.Domain.Abstractions
{
    public abstract class BaseDomainModel : IDomainModel
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; } = null!;

        [StringLength(50)]
        public string UpdatedBy { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
