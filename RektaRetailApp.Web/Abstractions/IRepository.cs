using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Abstractions;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Web.Abstractions
{
    public interface IRepository
    {
        Task Commit<T>(DbContext db) where T : BaseDomainModel;

    }
}
