using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.TempService;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace BackEndAD.ServiceInterface
{
    public interface IStoreClerkService
    {
        public Task<IList<Stationery>> findAllStationeriesAsync();
        public Task<Stationery> findStationeryByIdAsync(int id);
        public void saveStationery(Stationery stationery);
        public void deleteStationery(int id);
        public Task<IList<Supplier>> findAllSuppliersAsync();
        public Task<Supplier> findSupplierByIdAsync(int id);
        public Task<PurchaseOrder> findPOById(int id);
        public IList<PurchaseOrderDetail> findPODById(int id);

        public Task<StockAdjustment> generateStkAdjustmentAsync(StockAdjustment stkAdj,
                        List<StockAdjustmentDetail> stockAdjustmentDetails);
        public Task<IEnumerable<StockAdjustmentDetail>> findStockAdjustmentByIdAsync(int stockAdjustmentId);
        public void updateStockAdjustment(List<StockAdjustmentDetail> stockAdjustmentDetails);

        public void saveSupplier(Supplier s);

        public void deleteSupplier(int id);
       
        public void updateSupplier(Supplier s);

        public void savePurchaseOrder(PurchaseOrder po);
        public void savePurchaseOrderDetail(PurchaseOrderDetail pod);

        public IList<SupplierItem> findSuppliersByStationeryId(int id);
        public Task<IList<RequesterRow>> GetAllRequesterRow();
        public Task<IList<DisbursementList>> findAllDisbursementListAsync();
        public Task<IList<DisbursementDetail>> findAllDisbursementDetailAsync();
        public Task<Employee> findEmployeeByIdAsync(int eId);
        public Task<IList<StockAdjustSumById>> StockAdjustDetailInfo();
        public Task<IList<AdjustmentVocherInfo>> getAllAdjustDetailLineByAdjustId(StockAdjustSumById item);
        public Task<AdjustmentVocherInfo> getEachVoucherDetail(AdjustmentVocherInfo info);
        public Task<IList<AdjustmentVocherInfo>> issueVoucher(StockAdjustSumById voc);
        public Task<IList<Requisition>> findAllRequsitionAsync();
         public Task<IList<PurchaseOrder>> findAllPOAsync();
        public void updatePO(PurchaseOrder po);
        public Task<IList<RequisitionDetail>> findAllRequsitionDetailsAsync();
        public void saveStockAdjustment(StockAdjustment newSA);
        public void saveStockAdjustmentDetail(StockAdjustmentDetail sAD);
        public Task<IList<Department>> findAllDepartmentAsync();
        public Task<IList<DisburseItemDetails>> getDisburseItemDetail(RequesterRow row);
        
        void updateStationery(Stationery s);
        public Task<IList<CollectionInfo>> findAllCollectionPointAsync();
        void saveDisbursementList(DisbursementList newDL);
        void saveDisbursementDetail(DisbursementDetail currDB);
        void udpateRequisitionDetail(RequisitionDetail rd);
    }

}
