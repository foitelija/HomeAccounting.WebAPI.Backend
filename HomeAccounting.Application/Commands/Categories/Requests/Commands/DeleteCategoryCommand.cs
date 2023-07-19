using HomeAccounting.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Categories.Requests.Commands
{
    public class DeleteCategoryCommand : IRequest<BaseServicesResponse>
    {
        public int Id { get; set; }
    }
}
