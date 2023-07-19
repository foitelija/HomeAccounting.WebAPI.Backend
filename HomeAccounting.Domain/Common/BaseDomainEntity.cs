namespace HomeAccounting.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
