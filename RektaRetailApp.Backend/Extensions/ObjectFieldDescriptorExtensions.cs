using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using RektaRetailApp.Backend.Data;

namespace RektaRetailApp.Backend.Extensions
{
    public static class ObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UseDbContext<TContext>(this IObjectFieldDescriptor descriptor)
            where TContext : DbContext
        {
            return descriptor.UseScopedService<TContext>(
                create: s => s.GetRequiredService<DbContextPool<TContext>>().Rent(),
                dispose: (s, c) => s.GetRequiredService<DbContextPool<TContext>>().Return(c)
            );
        }
    }

    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<RektaContext>();
        }
    }
}
