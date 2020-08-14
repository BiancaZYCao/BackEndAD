using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceInterface
{
    public interface IStoreClerkService
    {
        public Task<List<Inventory>> findAllInventoriesAsync();
        public Task<Inventory> findInventoryByIdAsync(int id);


    }

}
