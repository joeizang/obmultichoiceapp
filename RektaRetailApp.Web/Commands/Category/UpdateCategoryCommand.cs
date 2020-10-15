using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Category;

namespace RektaRetailApp.Web.Commands.Category
{
    public class UpdateCategoryCommand : IRequest<CategoryApiModel>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(450)]
        public string? Description { get; set; }

        [Required]
        public int Id { get; set; }
    }


    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryApiModel>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repo;

        public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<CategoryApiModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<UpdateCategoryCommand,UpdateCategoryApiModel>(request);
            await _repo.Update(model);
            await _repo.SaveAsync().ConfigureAwait(false);
            return await _repo.GetCategoryById(model.CategoryId);
        }
    }
}
