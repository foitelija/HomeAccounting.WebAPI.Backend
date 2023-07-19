using HomeAccounting.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Categories.Requests.Queries
{
    public class GetCategoriesListRequest : IRequest<List<Category>>
    {
        public int Id { get; set; }
    }
}
