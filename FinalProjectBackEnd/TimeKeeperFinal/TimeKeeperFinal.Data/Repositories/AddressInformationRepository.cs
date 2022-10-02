using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Core.IRepositories;

namespace TimeKeeperFinal.Data.Repositories
{
    public class AddressInformationRepository : Repository<AddressInformation>, IAddressInformationRepository
    {
        public AddressInformationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
