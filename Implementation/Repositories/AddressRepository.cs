using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{
    public class AddressRepository : GenericRepository<Address> , IAddressRepository 
    {
        public AddressRepository(LuceDbContext Context) : base(Context)
        {
            _Context = Context;
        }
    
    }
} 