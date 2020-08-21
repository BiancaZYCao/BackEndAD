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
        public Task<IList<Supplier>> findAllSuppliersAsync();
        public Task<Supplier> findSupplierByIdAsync(int id);

        public Task<StockAdjustment> generateStkAdjustmentAsync(StockAdjustment stkAdj,
                        List<StockAdjustmentDetail> stockAdjustmentDetails);
        public Task<StockAdjustment> findStockAdjustmentByIdAsync(int id);

        public void saveSupplier(Supplier s);

        public void deleteSupplier(int id);
       
        public void updateSupplier(Supplier s);


        public void savePurchaseOrder(PurchaseOrder po);
        public Task<SupplierItem> findAllSupplierItemByIdAsync(int stkAdjId);
        public Task<StockAdjustmentDetail> findAllStockAdjustDetailByIdAsync(int stkAdjId);
        public Task<IList<StockAdjustmentDetail>> findAllStockAdjustDetailAsync();

        public Task<SupplierItem> findSupplierItemByIdAsync(int stkAdjId);
        public Task<Employee> findEmployeeByIdAsync(int eId);
        public Task<IList<AdjustmentVocherInfo>> StockAdjustDetailInfo();

        public Task<IList<Supplier>> findSupplierByStationeryId(int id);
        public IList<SupplierItem> findSuppliersByStationeryId(int id);


    }

}
