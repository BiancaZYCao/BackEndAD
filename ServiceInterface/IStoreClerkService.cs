using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceInterface
{
    public interface IStoreClerkService
    {
        public Task<IList<Stationery>> findAllStationeriesAsync();
        public Task<Stationery> findStationeryByIdAsync(int id);
        public Task<IList<Supplier>> findAllSuppliersAsync();
        public Task<Supplier> findSupplierByIdAsync(int id);
<<<<<<< HEAD
        public Task<StockAdjustment> generateStkAdjustmentAsync(StockAdjustment stkAdj,
                        List<StockAdjustmentDetail> stockAdjustmentDetails);
        public Task<StockAdjustment> findStockAdjustmentByIdAsync(int id);


=======
        public void deleteSupplier(int id);
        public void saveSupplier(Supplier s);
>>>>>>> 87d49e0550241237eab22f747f50d02c207d6a52
    }

}
