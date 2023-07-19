using HomeAccounting.Application.Commands.Categories.Requests.Queries;
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
        public async Task<List<Category>> Handle(GetCategoriesListRequest request, CancellationToken cancellationToken)
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Color = "Green", Name = "Test Name" },
                new Category { Id = 2, Color = "Yellow", Name = "Yellow Name" },
                new Category { Id = 3, Color = "Dark Purple", Name = "Foitelija" },
            };

            await Task.Delay(TimeSpan.FromSeconds(2));

            return categories;
        }
    }
}
