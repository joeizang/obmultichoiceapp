using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.Abstractions;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel.Category;
using ObmultichoiceRetailer.Web.Data;

namespace ObmultichoiceRetailer.Web.Services
{
  public class CategoryRepository : GenericBaseRepository ,ICategoryRepository
  {
    private readonly ObmultichoiceContext _db;
    private readonly IMapper _mapper;

    public CategoryRepository(ObmultichoiceContext db, IMapper mapper, IHttpContextAccessor accessor) : base(accessor,db)
    {
      _db = db;
      _mapper = mapper;
    }
    public async Task<IEnumerable<CategoryApiModel>> GetCategories()
    {
      var result = await _db.Categories.AsNoTracking()
          .ProjectTo<CategoryApiModel>(_mapper.ConfigurationProvider)
          .ToListAsync();
      return result;
    }

    public async Task<IEnumerable<CategoryDropDownApiModel>> GetForDropDown()
    {
      var result = await _db.Categories.AsNoTracking()
          .ProjectTo<CategoryDropDownApiModel>(_mapper.ConfigurationProvider)
          .ToArrayAsync();
      return result;
    }

    public async Task<CategoryApiModel> GetCategoryById(int id)
    {
      if (id == int.MaxValue || id == int.MinValue)
        throw new ArgumentException("the id passed doesn't identify a category");
      var result = await _db.Categories.AsNoTracking()
          .Where(x => x.Id == id)
          .ProjectTo<CategoryApiModel>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync()
          .ConfigureAwait(false);
      return result;
    }

    public async Task<CategoryApiModel> GetCategoryBy(string name, string desc)
    {
      var result = await _db.Categories.AsNoTracking()
          .Where(x => x.Description != null &&
                      x.Name.Equals(name.ToUpperInvariant()) &&
                      x.Description.Equals(desc.ToUpperInvariant()))
          .SingleOrDefaultAsync();
      var category = _mapper.Map<Category, CategoryApiModel>(result);
      return category;
    }

    public void Create(CreateCategoryApiModel entity)
    {
      if (entity == null)
        throw new ArgumentException("category to be created cannot be null!");
      var category = _mapper.Map<CreateCategoryApiModel, Category>(entity);
      category.Name = category.Name.Trim().ToUpperInvariant();
      category.Description = category.Description?.Trim().ToUpperInvariant();
      _db.Categories.Add(category);
    }

    public async Task Update(UpdateCategoryApiModel entity)
    {
      if (entity is null)
        throw new ArgumentException("cannot update null values");
      //var category = _mapper.Map<UpdateCategoryApiModel, Category>(entity);
      var category = await _db.Categories.AsNoTracking()
                      .SingleOrDefaultAsync(x => x.Id == entity.CategoryId);
      category.Description = category.Description?.Trim().ToUpperInvariant();
      category.Name = category.Name.Trim().ToUpperInvariant();
      _db.Entry(category).State = EntityState.Modified;
    }

    public async Task Delete(DeleteCategoryApiModel entity)
    {
      if(entity is null)
          throw new ArgumentNullException("cannot delete null!");
      var category = await _db.Categories.AsNoTracking()
          .SingleOrDefaultAsync(x => x.Id == entity.Id);

      _db.Categories.Remove(category);
    }

    public Task SaveAsync()
    {
        return Commit<Category>();
    }

  }
}
