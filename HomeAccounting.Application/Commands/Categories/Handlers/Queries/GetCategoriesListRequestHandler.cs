using HomeAccounting.Application.Commands.Categories.Requests.Queries;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Categories.Handlers.Queries
{
    public class GetCategoriesListRequestHandler : IRequestHandler<GetCategoriesListRequest, List<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesListRequestHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> Handle(GetCategoriesListRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll();
            return categories.ToList();
        }
    }
}
