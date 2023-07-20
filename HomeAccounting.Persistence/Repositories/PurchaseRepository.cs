using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain;
using HomeAccounting.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Persistence.Repositories
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        private readonly AccountingDbContext _context;

        public PurchaseRepository(AccountingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Purchase>> GetPurchasesListWithDetailsAsync()
        {
            var response = await _context.PurchaseOrders.Include(u=>u.FamilyMember).Include(c=>c.Category).ToListAsync();
            return response;

        }

        public async Task<Purchase> GetPurchasesWithDetailsAsync(int id)
        {
            var purchase = await _context.PurchaseOrders.Where(x=>x.Id == id).Include(u => u.FamilyMember).Include(c => c.Category).FirstOrDefaultAsync();
            return purchase;
        }

        public async Task<bool> IsCategoryUsedAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }
    }
}
