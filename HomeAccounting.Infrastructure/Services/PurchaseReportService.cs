using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain.Statistics;
using HomeAccounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAccounting.Domain.Reports.Months;
using HomeAccounting.Domain.Reports.Statistics;
using System.Collections;
using HomeAccounting.Application.Responses;

namespace HomeAccounting.Infrastructure.Services
{
    public class PurchaseReportService : IPurchaseReportService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseReportService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<PageResponse<List<PurchaseReportDto>>> GetMonthPurchaseReports(int? month, int? userId = null, int[] categoryIds = null, int page = 1, int pageSize = 10)
        {
            var filtredPurchases = await _purchaseRepository.GetPurchasesByMonthAsync(month, userId, categoryIds);

            var pagedPurchases = filtredPurchases.Skip((page -1)* pageSize).Take(pageSize).ToList();

            var groupedPurchases = pagedPurchases.GroupBy(x => x.FamilyMember.Name).ToDictionary(g => g.Key, g => g.ToList());

            var purchaseReports = groupedPurchases.Select(group => new PurchaseReportDto
            {
                Username = group.Key,
                Purchases = group.Value.Select(p => new PurchaseDto
                {
                    Id = p.Id,
                    FamilyMember = new FamilyMemberDto { Name = p.FamilyMember.Name, FamilyMemberId = p.FamilyMember.Id },
                    Category = new CategoryDto { Name = p.Category.Name, CategoryId = p.Category.Id },
                    Price = p.Price,
                    Comment = p.Comment,
                    Created = p.DateCreated,
                }).ToList(),
            }).ToList();

            var result = new PageResponse<List<PurchaseReportDto>>
            {
                Items = purchaseReports,
                TotalCount = filtredPurchases.Count(),
                PageNumber = page,
                PageSize = pageSize,
            };

            return result;
        }

        public async Task<PageResponse<StatisticsResultDto>> GetStatisticsPurchaseReports(DateTime dateStart, DateTime dateEnd, int? userId = null, int[] categoryIds = null, int page = 1, int pageSize = 10)
        {
            var purchases = await _purchaseRepository.GetPurchasesByPeriodAsync(dateStart, dateEnd, userId, categoryIds);

            var paged = purchases.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var statisticsByCategory = paged.GroupBy(p => p.CategoryId)
                .Select(g => new CategoryStatisticsDto
                {
                    CategoryId = g.Key,
                    CategoryName = g.FirstOrDefault().Category.Name,
                    TotalPercentage = Math.Round(((decimal)g.Sum(p => p.Price) / paged.Sum(p => p.Price) * 100), 2),
                    TotalAmount = g.Sum(p => p.Price)
                }).ToList();

            var statisticsByFamilyMember = paged.GroupBy(p => p.FamilyMemberId).Select(g => new FamilyMemberStatisticsDto
            {
                FamilyMemberId = g.Key,
                FamilyMemberName = g.FirstOrDefault().FamilyMember.Name,
                TotalAmount = g.Sum(p => p.Price)
            }).ToList();

            var result = new PageResponse<StatisticsResultDto>
            {
                Items =  
                    new StatisticsResultDto
                    {
                        StatisticsByCategory = statisticsByCategory,
                        StatisticsByFamilyMember = statisticsByFamilyMember,
                        TotalAmount = purchases.Sum(p => p.Price)
                    
                },
                TotalCount = purchases.Count(),
                PageNumber = page,
                PageSize = pageSize
            };



            return result;
        }
    }
}
