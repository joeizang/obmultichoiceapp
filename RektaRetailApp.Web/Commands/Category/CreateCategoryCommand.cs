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
    public class CreateCategoryCommand : IRequest<CategoryApiModel>
    {
        [Required] 
        [StringLength(50)] 
        public string Name { get; set; } = null!;

        [StringLength(450)]
        public string? Description { get; set; }
    }


    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryApiModel>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<CategoryApiModel> Handle(CreateCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            var model = _mapper.Map<CreateCategoryApiModel>(request);
            _repo.Create(model);
            await _repo.SaveAsync().ConfigureAwait(false);
            var response = await _repo.GetCategoryBy(model.Name, model.Description);
            return response;
        }
    }
}
