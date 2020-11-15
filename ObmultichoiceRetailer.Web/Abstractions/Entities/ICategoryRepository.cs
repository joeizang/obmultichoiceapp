using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.Category;

namespace ObmultichoiceRetailer.Web.Abstractions.Entities
{
  public interface ICategoryRepository : IRepository
  {
    Task<IEnumerable<CategoryApiModel>> GetCategories();

    Task<IEnumerable<CategoryDropDownApiModel>> GetForDropDown();

    Task<CategoryApiModel> GetCategoryById(int id);

    Task<CategoryApiModel> GetCategoryBy(string name, string desc);

    void Create(CreateCategoryApiModel model);

    //IEnumerable<object> Get();

    //Task<T> GetById(long id);

    //void Create(object entity);

    Task Update(UpdateCategoryApiModel entity);

    Task Delete(DeleteCategoryApiModel entity);

    Task SaveAsync();
  }
}
