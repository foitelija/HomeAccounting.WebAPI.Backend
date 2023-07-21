﻿using HomeAccounting.Application.Interfaces.Infrastructure;
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

namespace HomeAccounting.Infrastructure.Services
{
    public class PurchaseReportService : IPurchaseReportService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseReportService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<List<PurchaseReportDto>> GetMonthPurchaseReports(int? month, int? userId = null, int[] categoryIds = null)
        {
            var filtredPurchases = await _purchaseRepository.GetPurchasesByMonthAsync(month,userId,categoryIds);

            var groupedPurchases = filtredPurchases.GroupBy(x => x.FamilyMember.Name).ToDictionary(g=>g.Key, g=>g.ToList());

            var purchaseReports = groupedPurchases.Select(group => new PurchaseReportDto
            {
                Username = group.Key,
                Purchases = group.Value.Select(p=> new PurchaseDto
                {
                    Id = p.Id,
                    FamilyMember = new FamilyMemberDto { Name = p.FamilyMember.Name, FamilyMemberId = p.FamilyMember.Id },
                    Category = new CategoryDto { Name = p.Category.Name, CategoryId = p.Category.Id },
                    Price = p.Price,
                    Comment = p.Comment,
                    Created = p.DateCreated,
                }).ToList(),
            }).ToList();

            return purchaseReports;
        }

        public async Task<StatisticsResultDto> GetStatisticsPurchaseReports(DateTime dateStart, DateTime dateEnd, int? userId = null, int[] categoryIds = null)
        {
            var query = await _purchaseRepository.GetPurchasesByPeriodAsync(dateStart, dateEnd, userId, categoryIds);
            return null;
        }
    }
}
