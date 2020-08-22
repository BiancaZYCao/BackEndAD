using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.TempService;

namespace BackEndAD.ServiceInterface
{
    public interface IStoreClerkService
    {
        public Task<IList<Stationery>> findAllStationeriesAsync();
        public Task<Stationery> findStationeryByIdAsync(int id);
        public Task<StockAdjustment> findStockAdjustmentByIdAsync(int stockAdjustmentId);

        public void saveStationery(Stationery stationery);
        public Task<IList<Supplier>> findAllSuppliersAsync();
        public Task<Supplier> findSupplierByIdAsync(int id);

        public void saveSupplier(Supplier s);

        public void deleteSupplier(int id);
       
        public void updateSupplier(Supplier s);
        public void savePurchaseOrder(PurchaseOrder po);
        public Task<IList<Supplier>> findSupplierByStationeryId(int id);
        public IList<SupplierItem> findSuppliersByStationeryId(int id);
        


    }

}
