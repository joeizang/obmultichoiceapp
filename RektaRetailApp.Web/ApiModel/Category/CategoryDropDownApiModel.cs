using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.ApiModel.Category
{
    public class CategoryDropDownApiModel
    {
        public long Key { get; set; }

        public string CategoryName { get; set; } = null!;
    }
}
