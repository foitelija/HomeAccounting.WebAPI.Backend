using HomeAccounting.Domain.Common;

namespace HomeAccounting.Domain
{
    public class Purchase : BaseDomainEntity
    {
        public FamilyMember FamilyMember { get; set; }
        public int FamilyMemberId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? Comment { get; set; }
    }
}
