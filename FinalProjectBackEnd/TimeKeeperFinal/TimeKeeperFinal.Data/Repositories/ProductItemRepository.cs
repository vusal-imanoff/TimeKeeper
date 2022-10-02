using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Core.IRepositories;

namespace TimeKeeperFinal.Data.Repositories
{
    public class ProductItemRepository : Repository<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
