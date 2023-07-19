using HomeAccounting.Domain.Common;

namespace HomeAccounting.Domain
{
    public class Category : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
