using BackEndAD.DataContext;
using BackEndAD.Models;
using BackEndAD.Repo;
using BackEndAD.TempService;
using BackEndAD.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceImpl
{
    public class StoreClerkServiceImpl : IStoreClerkService
    {
        public IUnitOfWork<ProjectContext> unitOfWork;

        public StoreClerkServiceImpl(IUnitOfWork<ProjectContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Stationery
        public async Task<IList<Stationery>> findAllStationeriesAsync()
        {
            IList<Stationery> list = await unitOfWork.GetRepository<Stationery>().GetAllAsync();
            return list;
        }

        public async Task<Stationery> findStationeryByIdAsync(int stationeryId)
        {
            Stationery s = await unitOfWork.GetRepository<Stationery>().FindAsync(stationeryId);
            return s;
        }

        public void saveStationery(Stationery stationery)
        {
            Stationery s1 = unitOfWork.GetRepository<Stationery>().GetById(stationery.Id);
            if(s1 != null)
            {
                s1.category = stationery.category;
                s1.desc = stationery.desc;
                s1.inventoryQty = stationery.inventoryQty;
                unitOfWork.GetRepository<Stationery>().Update(s1);
                unitOfWork.SaveChanges();
            }
            else
            {
                unitOfWork.GetRepository<Stationery>().Insert(stationery);
                unitOfWork.SaveChanges();
            }
        }
        #endregion

        #region supplier
        public async Task<IList<Supplier>> findAllSuppliersAsync()
        {
            IList<Supplier> list = await unitOfWork.GetRepository<Supplier>().GetAllAsync();
            return list;
        }

        public async Task<Supplier> findSupplierByIdAsync(int supplierId)
        {
            Supplier sup = await unitOfWork.GetRepository<Supplier>().FindAsync(supplierId);
            return sup;
        }

        public void deleteSupplier(int id)
        {
            unitOfWork.GetRepository<Supplier>().Delete(id);
            unitOfWork.SaveChanges();
        }



        public void saveSupplier(Supplier s)
        {
            unitOfWork.GetRepository<Supplier>().Insert(s);
            unitOfWork.SaveChanges();

        }

        public void updateSupplier(Supplier s)
        {
            unitOfWork.GetRepository<Supplier>().Update(s);
            unitOfWork.SaveChanges();

        }
        #endregion


        //StoreManager
        public async Task<IList<StockAdjustmentDetail>> findAllStockAdjustDetailAsync()
        {
            IList<StockAdjustmentDetail> list = await unitOfWork.GetRepository<StockAdjustmentDetail>().GetAllAsync();
            return list;
        }
        
        public async Task<StockAdjustmentDetail> findAllStockAdjustDetailByIdAsync(int stkAdjId)
        {
            StockAdjustmentDetail stockAdjDetail = await unitOfWork.GetRepository<StockAdjustmentDetail>().FindAsync(stkAdjId);
            return stockAdjDetail;
        }

        public async Task<SupplierItem> findSupplierItemByIdAsync(int stkAdjId)
        {
            SupplierItem supplierItem = await unitOfWork.GetRepository<SupplierItem>().FindAsync(stkAdjId);
            return supplierItem;
        }

        public async Task<Employee> findEmployeeByIdAsync(int eId)
        {
            Employee emp = await unitOfWork.GetRepository<Employee>().FindAsync(eId);
            return emp;
        }
        public async Task<IList<AdjustmentVoucherDetail>> findAllAdjustmentVoucherDetailAsync()
        {
            IList<AdjustmentVoucherDetail> list = await unitOfWork.GetRepository<AdjustmentVoucherDetail>().GetAllAsync();
            return list;
        }

        public async Task<IList<AdjustmentVoucher>> findAllAdjustmentVoucherAsync()
        {
            IList<AdjustmentVoucher> list = await unitOfWork.GetRepository<AdjustmentVoucher>().GetAllAsync();
            return list;
        }
    
        public void issueVoucher(AdjustmentVocherInfo voc)
        {
            AdjustmentVoucherDetail vocDetail = new AdjustmentVoucherDetail() {
                AdjustmentVoucherId = voc.stockAdustmentId,
                StockAdjustmentDetailId = voc.stockAdustmentDetailId,
                price = voc.amount,
            };
            unitOfWork.GetRepository<AdjustmentVoucherDetail>().Insert(vocDetail);
            unitOfWork.SaveChanges();

            AdjustmentVoucher adjVoc = new AdjustmentVoucher()
            {
                StockAdjustmentId = voc.stockAdustmentId,
                EmployeeId = voc.empId,
                reason = voc.reason,
                date = DateTime.Now
            };
            unitOfWork.GetRepository<AdjustmentVoucher>().Insert(adjVoc);
            unitOfWork.SaveChanges();

        }

        public async Task<AdjustmentVocherInfo> getEachVoucherDetail(AdjustmentVocherInfo info)
        {

            AdjustmentVoucher voc = unitOfWork
                .GetRepository<AdjustmentVoucher>()
                .GetAllIncludeIQueryable(filter: x => x.StockAdjustmentId == info.stockAdustmentId).FirstOrDefault();

            AdjustmentVoucherDetail vocDetail = unitOfWork
                .GetRepository<AdjustmentVoucherDetail>()
                .GetAllIncludeIQueryable(filter: x => x.AdjustmentVoucherId == voc.Id).FirstOrDefault();

            AdjustmentVocherInfo obj = new AdjustmentVocherInfo()
            {
                stockAdustmentDetailId = vocDetail.Id,
                stockAdustmentId = voc.StockAdjustmentId,
                empId = voc.EmployeeId,
                date = voc.date,
                reason = "Missing",
                empName = "Mary1",
                itemCode = info.itemCode,
                quantity = info.quantity,
                amount = vocDetail.price
            };
            return obj;
        }
        public async Task<IList<AdjustmentVocherInfo>> StockAdjustDetailInfo()
        {
            float amounttotal = 0;
            IList<AdjustmentVocherInfo> voucherInfoList = new List<AdjustmentVocherInfo>();
            AdjustmentVocherInfo voucher1 = new AdjustmentVocherInfo()
            {
                stockAdustmentDetailId = 1,
                stockAdustmentId = 1,
                empId = 1,
                reason = "Missing",
                empName = "Mary1",
                itemCode = 46,
                quantity = 2,
                amount = 100
            };
            voucherInfoList.Add(voucher1);

            IList<StockAdjustmentDetail> list = await findAllStockAdjustDetailAsync();

            IList<AdjustmentVoucherDetail> vocDetails = await findAllAdjustmentVoucherDetailAsync();

            foreach (StockAdjustmentDetail eachSAdjDetailRecord in list)
            {
                foreach (AdjustmentVoucherDetail eachVocDetailRecord in vocDetails)
                {
                    if (eachVocDetailRecord.StockAdjustmentDetailId != eachSAdjDetailRecord.id)
                    {
                        SupplierItem supplierItem = await findSupplierItemByIdAsync(eachSAdjDetailRecord.StationeryId);
                        amounttotal = supplierItem.price * eachSAdjDetailRecord.discpQty;

                StockAdjustment stockAdjustment = await findStockAdjustmentByIdAsync(eachSAdjDetailRecord.id);
                Employee emp = await findEmployeeByIdAsync(stockAdjustment.EmployeeId);

                AdjustmentVocherInfo voucher = new AdjustmentVocherInfo()
                {
                    stockAdustmentDetailId = eachSAdjDetailRecord.id,
                    stockAdustmentId = eachSAdjDetailRecord.StockAdjustmentId,
                    reason = eachSAdjDetailRecord.comment,
                    empName = emp.name,
                    itemCode = eachSAdjDetailRecord.StationeryId,
                    quantity = eachSAdjDetailRecord.discpQty,
                    amount = amounttotal
                };
                
            }

            return voucherInfoList;
        }
        //end

        #region store clerk adjustment

        public async Task<StockAdjustment> generateStkAdjustmentAsync(StockAdjustment stkAdj,
                        List<StockAdjustmentDetail> stockAdjustmentDetails)
        {
            //using transcation
            using (var tran = unitOfWork.BeginTransaction())
            {
                try
                {
                    // step1 insert StkAdj
                    unitOfWork.GetRepository<StockAdjustment>().Insert(stkAdj);
                    //unitOfWork.GetRepository<StockAdjustment>().Save();

                    // step2 insert StkAdjDetial and update inventory one by one in the list
                    foreach (StockAdjustmentDetail stkAdjDet in stockAdjustmentDetails)
                    {
                        // step2.1 add stkAdjDetails
                        stkAdjDet.stockAdjustment = stkAdj;
                        unitOfWork.GetRepository<StockAdjustmentDetail>().Insert(stkAdjDet);
                        //unitOfWork.GetRepository<StockAdjustment>().Save();
                        // step2.1 get stationery and update inventory level
                        Stationery s = unitOfWork.GetRepository<Stationery>().GetById(stkAdjDet.StationeryId);
                        s.inventoryQty += stkAdjDet.discpQty;
                        unitOfWork.GetRepository<Stationery>().Update(s);
                        //unitOfWork.GetRepository<Stationery>().Save();
                    }
                    // save changes
                    await unitOfWork.SaveChangesAsync();
                    await tran.CommitAsync();
                    //finish transaction if success
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    return null;
                }
            }
            //return stockAdjustment
            var result = await unitOfWork.GetRepository<StockAdjustment>().FindAsync(stkAdj.Id);
            return result;

        }

        public async Task<StockAdjustment> findStockAdjustmentByIdAsync(int stockAdjustmentId)
        {
            StockAdjustment stkadj = await unitOfWork.GetRepository<StockAdjustment>()
                .FindAsync(stockAdjustmentId);
            return stkadj;
        }
        #endregion

        /*
         * public IList<Department> findAllDepartmentsAsyncEager()
        {
            IList<Department> deptlist = 
                unitOfWork.GetRepository<Department>()
                .GetAllIncludeIQueryable(null, null,"Collection").ToList();
            return deptlist;
        }
         */
        #region place order
        public async Task<IList<Supplier>> findSupplierByStationeryId(int id)
        {
            IList<Supplier> list = new List<Supplier>();

            //get all suppliers
            IList<Supplier> ilist = await unitOfWork.GetRepository<Supplier>().GetAllAsync();

            foreach (Supplier supplier in ilist)
            {
                List<SupplierItem> supplierItems = (List<SupplierItem>)supplier.supplierItems;
                //check if supplier has item
                if (supplierItems == null) { return null; }
                else
                {
                    bool hasItem = supplierItems.Select(x => x.Stationery.Id == id ? true : false).FirstOrDefault();
                    if (hasItem) list.Add(supplier);
                }
            }

            List<Supplier> orderedList = (List<Supplier>)list.OrderBy(x => x.priority);

            return orderedList;
        }

        public void savePurchaseOrder(PurchaseOrder po)
        {
            unitOfWork.GetRepository<PurchaseOrder>().Insert(po);
        }

        public Task<SupplierItem> findAllSupplierItemByIdAsync(int stkAdjId)
        {
            throw new NotImplementedException();
        }

        
        //Bianca PO-step2
        public IList<SupplierItem> findSuppliersByStationeryId(int id)
        {
            IList<SupplierItem> itemlist = unitOfWork
                .GetRepository<SupplierItem>()
                .GetAllIncludeIQueryable(filter: x => x.StationeryId == id).ToList();
            return itemlist;
        }
        #endregion
    }
}
