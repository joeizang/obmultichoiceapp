using System;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Shift : BaseDomainModel
    {
        public string Name { get; set; } = null!;

        public DateTimeOffset ShiftStartsAt { get; set; }

        public DateTimeOffset SiftEndsAt { get; set; }

        public decimal HourlyRate { get; set; }
    }
}