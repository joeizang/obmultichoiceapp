using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObmultichoiceRetailer.Web.ApiModel.Category
{
    public class CreateCategoryApiModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
