using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Categories.Requests.Commands
{
    public class CreateCategoryCommand : IRequest<BaseServicesResponse>
    {
        public Category Category { get; set; }
    }
}
