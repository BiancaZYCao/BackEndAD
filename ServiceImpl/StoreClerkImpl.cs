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
            if (s1 != null)
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
        public async Task<IList<StockAdjustment>> findAllStockAdjustmentAsync()
        {
            IList<StockAdjustment> list = await unitOfWork.GetRepository<StockAdjustment>().GetAllAsync();
            return list;
        }
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

        public async Task<IList<AdjustmentVocherInfo>> issueVoucher(StockAdjustSumById voc)
        {
            IList<AdjustmentVocherInfo> list = await getAllAdjustDetailLineByAdjustId(voc);
            List<AdjustmentVocherInfo> voucherResult = new List<AdjustmentVocherInfo>();
            
            foreach (AdjustmentVocherInfo eachInfo in list)
            {
                AdjustmentVoucherDetail vocDetail = new AdjustmentVoucherDetail()
                {
                    adjustmentVoucherId = eachInfo.stockAdustmentId,
                    StockAdjustmentDetailId = eachInfo.stockAdustmentDetailId,
                    price = eachInfo.amount,
                };
                unitOfWork.GetRepository<AdjustmentVoucherDetail>().Insert(vocDetail);
                unitOfWork.SaveChanges();

                AdjustmentVoucher adjVoc = new AdjustmentVoucher()
                {
                    StockAdjustmentId = eachInfo.stockAdustmentId,
                    EmployeeId = eachInfo.empId,
                    reason = eachInfo.reason,
                    date = DateTime.Now
                };
                unitOfWork.GetRepository<AdjustmentVoucher>().Insert(adjVoc);
                unitOfWork.SaveChanges();

                AdjustmentVocherInfo obj = await getEachVoucherDetail(eachInfo);
                if (obj != null)
                {
                    voucherResult.Add(obj);
                }
                
            }
            return voucherResult;
        }

        public async Task<AdjustmentVocherInfo> getEachVoucherDetail(AdjustmentVocherInfo info)
        {
            AdjustmentVoucherDetail vocDetail = unitOfWork
                .GetRepository<AdjustmentVoucherDetail>()
                .GetAllIncludeIQueryable(filter: x => x.StockAdjustmentDetailId == info.stockAdustmentDetailId).FirstOrDefault();

            if (vocDetail != null)
            {
                AdjustmentVoucher voc = unitOfWork
                .GetRepository<AdjustmentVoucher>()
                .GetAllIncludeIQueryable(filter: x => x.Id == vocDetail.adjustmentVoucherId).FirstOrDefault();

                if (voc != null)
                {
                    AdjustmentVocherInfo obj = new AdjustmentVocherInfo()
                    {
                        vocNo = voc.Id,
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

            }

            return null;
        }

        public async Task<IList<AdjustmentVocherInfo>> getAllAdjustDetailLineByAdjustId(StockAdjustSumById item)
        {
            float amounttotal = 0;
            IList<AdjustmentVocherInfo> voucherInfoList = new List<AdjustmentVocherInfo>();
            IList<AdjustmentVoucherDetail> vocDetails = await findAllAdjustmentVoucherDetailAsync();

            IList<StockAdjustmentDetail> list = unitOfWork
               .GetRepository<StockAdjustmentDetail>()
               .GetAllIncludeIQueryable(filter: x => x.stockAdjustmentId == item.stockAdustmentId).ToList();

            IList<AdjustmentVoucherDetail> vocList = await findAllAdjustmentVoucherDetailAsync();

            bool isApprove = false;
            foreach (StockAdjustmentDetail eachSAdjDetailRecord in list)
            {
                foreach (AdjustmentVoucherDetail eachVocDetailRecord in vocDetails)
                {
                    isApprove = false;
                    if (eachVocDetailRecord.StockAdjustmentDetailId == eachSAdjDetailRecord.Id)
                    {
                        isApprove = true;
                    }
                }
                if (!isApprove)
                {
                    SupplierItem supplierItem = await findSupplierItemByIdAsync(eachSAdjDetailRecord.StationeryId);
                    amounttotal = supplierItem.price * eachSAdjDetailRecord.discpQty;

                    StockAdjustment stockAdjustment = await findStockAdjustmentByIdAsync(eachSAdjDetailRecord.stockAdjustmentId);
                    if (stockAdjustment != null)
                    {
                        Employee emp = await findEmployeeByIdAsync(stockAdjustment.EmployeeId);
                        if (emp != null)
                        {
                            AdjustmentVocherInfo voucher = new AdjustmentVocherInfo()
                            {
                                stockAdustmentDetailId = eachSAdjDetailRecord.Id,
                                stockAdustmentId = eachSAdjDetailRecord.stockAdjustmentId,
                                reason = eachSAdjDetailRecord.comment,
                                empId = emp.Id,
                                empName = emp.name,
                                itemCode = eachSAdjDetailRecord.StationeryId,
                                quantity = eachSAdjDetailRecord.discpQty,
                                amount = amounttotal
                            };
                            voucherInfoList.Add(voucher);
                        }
                    }
                    else
                    {
                        AdjustmentVocherInfo voucher = new AdjustmentVocherInfo()
                        {
                            stockAdustmentDetailId = eachSAdjDetailRecord.Id,
                            stockAdustmentId = eachSAdjDetailRecord.stockAdjustmentId,
                            reason = eachSAdjDetailRecord.comment,
                            itemCode = eachSAdjDetailRecord.StationeryId,
                            quantity = eachSAdjDetailRecord.discpQty,
                            amount = amounttotal
                        };
                        voucherInfoList.Add(voucher);
                    }
                }
            }

            return voucherInfoList;
        }
        /*public async Task<IList<AdjustmentVocherInfo>> StockAdjustDetailInfo()
        {
            float amounttotal = 0;
            IList<AdjustmentVocherInfo> voucherInfoList = new List<AdjustmentVocherInfo>();
            IList<StockAdjustmentDetail> list = await findAllStockAdjustDetailAsync();

            IList<AdjustmentVoucherDetail> vocDetails = await findAllAdjustmentVoucherDetailAsync();
            
            IList<StockAdjustment> stoAdjlist = await findAllStockAdjustmentAsync();

            IList<AdjustmentVoucher> vocList = await findAllAdjustmentVoucherAsync();
            
            IList<AdjustmentVoucherDetail> vocDetails = await findAllAdjustmentVoucherDetailAsync();
            bool isApprove=false;
            foreach (StockAdjustmentDetail eachSAdjDetailRecord in list)
            { 
                foreach (AdjustmentVoucherDetail eachVocDetailRecord in vocDetails)
                {
                    isApprove = false;
                    if (eachVocDetailRecord.StockAdjustmentDetailId == eachSAdjDetailRecord.Id)
                    {
                        isApprove = true;
                    }
                }
                if (!isApprove)
                {
                    SupplierItem supplierItem = await findSupplierItemByIdAsync(eachSAdjDetailRecord.StationeryId);
                    amounttotal = supplierItem.price * eachSAdjDetailRecord.discpQty;

                    StockAdjustment stockAdjustment = await findStockAdjustmentByIdAsync(eachSAdjDetailRecord.stockAdjustmentId);
                    if (stockAdjustment != null)
                    {
                        Employee emp = await findEmployeeByIdAsync(stockAdjustment.EmployeeId);
                        if (emp != null)
                        {
                            AdjustmentVocherInfo voucher = new AdjustmentVocherInfo()
                            {
                                stockAdustmentDetailId = eachSAdjDetailRecord.Id,
                                stockAdustmentId = eachSAdjDetailRecord.stockAdjustmentId,
                                reason = eachSAdjDetailRecord.comment,
                                empId = emp.Id,
                                empName = emp.name,
                                itemCode = eachSAdjDetailRecord.StationeryId,
                                quantity = eachSAdjDetailRecord.discpQty,
                                amount = amounttotal
                            };
                            voucherInfoList.Add(voucher);
                        }
                    }
                    else
                    {
                        AdjustmentVocherInfo voucher = new AdjustmentVocherInfo()
                        {
                            stockAdustmentDetailId = eachSAdjDetailRecord.Id,
                            stockAdustmentId = eachSAdjDetailRecord.stockAdjustmentId,
                            reason = eachSAdjDetailRecord.comment,
                            itemCode = eachSAdjDetailRecord.StationeryId,
                            quantity = eachSAdjDetailRecord.discpQty,
                            amount = amounttotal
                        };
                        voucherInfoList.Add(voucher);
                    }
                }
            }

            return voucherInfoList;
        }*/
        public async Task<IList<StockAdjustSumById>> StockAdjustDetailInfo()
        {
            IList<StockAdjustSumById> stockAdjustSumByIdList = new List<StockAdjustSumById>();
            float amounttotal = 0;
            IList<StockAdjustment> stoAdjlist = await findAllStockAdjustmentAsync();
            IList<AdjustmentVoucherDetail> vocDetails = await findAllAdjustmentVoucherDetailAsync();
            IList<AdjustmentVoucher> vocList = await findAllAdjustmentVoucherAsync();
            bool isApprove = false;
            StockAdjustSumById adjustSumbyId = new StockAdjustSumById() { };

            foreach (StockAdjustment eachSAdjRecord in stoAdjlist)
            {
                List<StockAdjustmentDetail> stockAdjDetailList = unitOfWork
               .GetRepository<StockAdjustmentDetail>()
               .GetAllIncludeIQueryable(filter: x => x.stockAdjustmentId == eachSAdjRecord.Id).ToList();

                if (stockAdjDetailList != null)
                {
                    foreach (StockAdjustmentDetail eachSAdjDetailRecord in stockAdjDetailList)
                    {
                        foreach (AdjustmentVoucherDetail eachVocRecord in vocDetails)
                        {
                            isApprove = false;
                            if (eachSAdjDetailRecord.Id == eachVocRecord.StockAdjustmentDetailId)
                            {
                                isApprove = true;
                            }
                            SupplierItem supplierItem = await findSupplierItemByIdAsync(eachSAdjDetailRecord.StationeryId);
                            amounttotal += supplierItem.price * eachSAdjDetailRecord.discpQty;

                            StockAdjustment stockAdjustment = await findStockAdjustmentByIdAsync(eachSAdjDetailRecord.stockAdjustmentId);
                            if (stockAdjustment != null)
                            {
                                Employee emp = await findEmployeeByIdAsync(stockAdjustment.EmployeeId);
                                if (emp != null)
                                {
                                    adjustSumbyId = new StockAdjustSumById()
                                    {
                                        stockAdustmentId = eachSAdjDetailRecord.stockAdjustmentId,
                                        empId = emp.Id,
                                        empName = emp.name,
                                        amount = amounttotal
                                    };

                                }
                            }
                            else
                            {
                                adjustSumbyId = new StockAdjustSumById()
                                {
                                    stockAdustmentId = eachSAdjDetailRecord.stockAdjustmentId,
                                    amount = amounttotal
                                };
                            }
                        }
                    }
                    if (!isApprove)
                    {
                        stockAdjustSumByIdList.Add(adjustSumbyId);
                    }
                }
            }

            return stockAdjustSumByIdList;
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


        //Bianca PO-step2
        public IList<SupplierItem> findSuppliersByStationeryId(int id)
        {
            IList<SupplierItem> itemlist = unitOfWork
                .GetRepository<SupplierItem>()
                .GetAllIncludeIQueryable(filter: x => x.StationeryId == id).ToList();
            return itemlist;
        }

        /// <summary>
        /// place order GET
        /// 1. get all items need order invt-Lvl<reorder-Lvl 
        /// 2. get supplierItems for Items using step1-result
        /// 3. get suppliers using step2-result
        /// </summary>
        public void savePurchaseOrder(PurchaseOrder po)
        {
            unitOfWork.GetRepository<PurchaseOrder>().Insert(po);
        }

        public Task<IList<Requisition>> findAllRequsitionAsync()
        {
            return unitOfWork.GetRepository<Requisition>().GetAllAsync();
            
        }

        public Task<IList<RequisitionDetail>> findAllRequsitionDetailsAsync()
        {
           return unitOfWork.GetRepository<RequisitionDetail>().GetAllAsync();
        }

      
    }
}
