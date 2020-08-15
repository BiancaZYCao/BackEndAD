using BackEndAD.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.ServiceInterface;
using BackEndAD.Models;
using BackEndAD.Repo;

namespace BackEndAD.ServiceImpl
{
    public class SupplierServiceImpl : ISupplierService
    {

        public ISupplierRepo srepo;

        public SupplierServiceImpl(ISupplierRepo srepo)
        {
            this.srepo = srepo;
        }
        public async Task<List<Supplier>> findAllSuppliersAsync()
        {
            List<Supplier> slist = await srepo.findAllSuppliersAsync();
            return slist;
        }
    }
}
