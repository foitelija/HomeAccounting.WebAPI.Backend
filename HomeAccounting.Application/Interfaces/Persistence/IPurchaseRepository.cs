using HomeAccounting.Domain;

namespace HomeAccounting.Application.Interfaces.Persistence
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<bool> IsCategoryUsedAsync(int categoryId);
        Task<List<Purchase>> GetPurchasesListWithDetailsAsync();
        Task<Purchase> GetPurchasesWithDetailsAsync(int id);
    }
}
