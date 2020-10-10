using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Category;
using RektaRetailApp.Web.Data;

namespace RektaRetailApp.Web.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RektaContext _db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public CategoryRepository(RektaContext db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            _accessor = accessor;
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

        public async Task<CategoryApiModel> GetCategoryById(long id)
        {
            if (id == long.MaxValue || id == long.MinValue)
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
            if(entity == null)
                throw new ArgumentException("category to be created cannot be null!");
            var category = _mapper.Map<CreateCategoryApiModel, Category>(entity);
            category.Name = category.Name.Trim().ToUpperInvariant();
            category.Description = category.Description?.Trim().ToUpperInvariant();
            _db.Categories.Add(category);
        }

        public void Update(UpdateCategoryApiModel entity)
        {
            if(entity is null)
                throw new ArgumentException("cannot update null values");
            var category = _mapper.Map<UpdateCategoryApiModel, Category>(entity);
            category.Description = category.Description?.Trim().ToUpperInvariant();
            category.Name = category.Name.Trim().ToUpperInvariant();
            _db.Entry(category).State = EntityState.Modified;
        }

        public void Delete(DeleteCategoryApiModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task Commit()
        {
            var user = _accessor.HttpContext.User.Identity.Name ?? "Anonymous User";
            foreach (var category in _db.ChangeTracker.Entries<Category>())
            {
                if (category.State == EntityState.Added)
                {
                    category.Entity.CreatedAt = DateTimeOffset.Now.LocalDateTime;
                    category.Entity.UpdatedAt = DateTimeOffset.Now.LocalDateTime;
                    if (string.IsNullOrEmpty(category.Entity.CreatedBy))
                    {
                        category.Entity.CreatedBy = user;
                        category.Entity.UpdatedBy = user;
                    }
                }

                if (category.State == EntityState.Modified)
                {
                    category.Entity.UpdatedAt = DateTimeOffset.Now.LocalDateTime;
                    if (string.IsNullOrEmpty(category.Entity.UpdatedBy))
                    {
                        category.Entity.UpdatedBy = user;
                    }
                }

            }

            await _db.SaveChangesAsync();//.ConfigureAwait(false);
        }
    }
}
