using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Core.IRepositories;

namespace TimeKeeperFinal.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
