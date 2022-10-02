using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Core.IRepositories;

namespace TimeKeeperFinal.Data.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
