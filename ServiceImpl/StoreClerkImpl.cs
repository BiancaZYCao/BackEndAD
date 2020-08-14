using BackEndAD.Models;
using BackEndAD.Repo;
using BackEndAD.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceImpl
{
    public class StoreClerkServiceImpl : IStoreClerkService
    {
        public IInventoryRepo invtrepo;

        public StoreClerkServiceImpl(IInventoryRepo invtrepo)
        {
            this.invtrepo = invtrepo;
        }
        public async Task<List<Inventory>> findAllInventoriesAsync()
        {
            List<Inventory> list = await invtrepo.findAllInventoriesAsync();
            return list;
        }

        public async Task<Inventory> findInventoryByIdAsync(int id)
        {
            Inventory invt = await invtrepo.findInventoryByIdAsync(id);
            return invt;
        }
    }
}
