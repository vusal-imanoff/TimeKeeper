using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Core.IRepositories;

namespace TimeKeeperFinal.Data.Repositories
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        public SliderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
