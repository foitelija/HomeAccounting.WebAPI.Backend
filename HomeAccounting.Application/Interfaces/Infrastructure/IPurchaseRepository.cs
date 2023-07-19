using HomeAccounting.Domain;

namespace HomeAccounting.Application.Interfaces.Infrastructure
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<bool> IsCategoryUsedAsync(int categoryId);
        Task<List<Purchase>> GetPurchasesListWithDetailsAsync();
        Task<Purchase> GetPurchasesWithDetailsAsync(int id);
    }
}
