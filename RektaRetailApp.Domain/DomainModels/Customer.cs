using System;
using System.ComponentModel.DataAnnotations;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Customer
    {
        [Required] 
        [StringLength(50)] 
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        [Required]
        public CustomerStatus CustomerStatus { get; set; }
    }
}