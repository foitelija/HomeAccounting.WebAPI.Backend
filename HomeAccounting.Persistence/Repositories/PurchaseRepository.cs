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

        public async Task<List<Purchase>> GetPurchasesListWithFiltersAsync(int? month, int? userId, int[] categoryIds)
        {
            IQueryable<Purchase> query = _context.PurchaseOrders.Include(u => u.FamilyMember).Include(x => x.Category);

            if (month.HasValue)
            {
                query = query.Where(d => d.DateCreated.Value.Date.Month == month);
            }

            if (userId.HasValue)
            {
                query = query.Where(u => u.FamilyMemberId == userId.Value);
            }

            if (categoryIds != null && categoryIds.Length > 0)
            {
                query = query.Where(e=>categoryIds.Contains(e.CategoryId));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Purchase>> GetPurchasesListWithDetailsAndPaginationAsync(int skip, int take)
        {
            IQueryable<Purchase> query = _context.PurchaseOrders.Include(u => u.FamilyMember).Include(x => x.Category);


            var response =  await _context.PurchaseOrders
                .Include(u => u.FamilyMember)
                .Where(w => w.FamilyMemberId == 1)
                .Include(c => c.Category)
                .Where(x=>x.CategoryId == 1)
                .Skip((skip -1 )*take)
                .Take(take)
                .ToListAsync();
            return response;
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
