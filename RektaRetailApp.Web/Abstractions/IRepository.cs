using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Abstractions;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Web.Abstractions
{
    public interface IRepository
    {
        Task Commit<T>() where T : BaseDomainModel;

        Task<T> GetOneBy<T>(Expression<Func<T, object>>[]? includes = null, params Expression<Func<T, bool>>[] searchTerms ) where T : BaseDomainModel;

    }
}
