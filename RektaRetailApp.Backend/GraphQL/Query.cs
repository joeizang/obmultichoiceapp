using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace RektaRetailApp.Backend.GraphQL
{
    public class Query : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            base.Configure(descriptor);

        }
    }
}
