using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Web.Abstractions
{
    public interface IRepository
    {
        Task Commit();

    }
}
