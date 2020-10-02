using System.Collections.Generic;
using System.Text;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class ApplicationUser : BaseIdentityUser
    {
        public ApplicationUser()
        {
            SalesYouOwn = new List<Sale>();
        }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string OtherNames { get; set; } = null!;

        public Shift WorkShift { get; set; } = null!;

        public List<Sale> SalesYouOwn { get; set; }
    }
}
