using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObmultichoiceRetailer.Web.ApiModel.AuthManager
{
    public class UserApiModel
    {
    }

    public class UserInputModel
    {
        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? OtherNames { get; set; }

        public string LastName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
