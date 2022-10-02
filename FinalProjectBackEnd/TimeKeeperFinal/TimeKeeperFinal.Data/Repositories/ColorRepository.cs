using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Core.IRepositories;

namespace TimeKeeperFinal.Data.Repositories
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
