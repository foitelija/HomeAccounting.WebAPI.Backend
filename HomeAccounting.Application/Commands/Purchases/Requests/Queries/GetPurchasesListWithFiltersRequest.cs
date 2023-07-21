using HomeAccounting.Domain;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Queries
{
    public class GetPurchasesListWithFiltersRequest : IRequest<List<Purchase>>
    {
        public int? month { get; set; }
        public int? userId { get; set; }
        public int[] categoryIds { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
