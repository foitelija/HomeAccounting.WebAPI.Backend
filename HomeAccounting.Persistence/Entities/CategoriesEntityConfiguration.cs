using HomeAccounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Persistence.Entities
{
    public class CategoriesEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasData
                (
                new Category { Id = 1, Name = "Продукты питания" },
                new Category { Id = 2, Name = "Транспорт" },
                new Category { Id = 3, Name = "Мобильная связь" },
                new Category { Id = 4, Name = "Интернет" },
                new Category { Id = 5, Name = "Развлечения" }
                );
        }
    }
}
