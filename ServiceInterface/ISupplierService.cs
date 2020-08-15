using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceInterface
{
    public interface ISupplierService
    {
        public Task<List<Supplier>> findAllSuppliersAsync();
    }
}
