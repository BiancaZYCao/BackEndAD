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
    public class StoreSupervisorServiceImpl: IStoreSupervisorService
    {
        public IUnitOfWork<ProjectContext> unitOfWork;

        public StoreSupervisorServiceImpl(IUnitOfWork<ProjectContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

        public async Task<IList<StockAdjustSumById>> rejectRequest(StockAdjustSumById voc)
        {
            IList<AdjustmentVocherInfo> list = await getAllAdjustDetailLineByAdjustId(voc);
            List<AdjustmentVocherInfo> voucherResult = new List<AdjustmentVocherInfo>();

            foreach (AdjustmentVocherInfo eachInfo in list)
            {
                StockAdjustmentDetail stkDetail = unitOfWork
               .GetRepository<StockAdjustmentDetail>()
               .GetAllIncludeIQueryable(filter: x => x.Id == eachInfo.stockAdustmentDetailId).FirstOrDefault();

                if (stkDetail != null)
                {
                    stkDetail.Status = "Rejected";
                    unitOfWork.GetRepository<StockAdjustmentDetail>().Update(stkDetail);
                    unitOfWork.SaveChanges();
                }

                StockAdjustment stkAdj = new StockAdjustment()
                {
                    type = "Revert",
                    date = eachInfo.date,
                    EmployeeId = eachInfo.empId
                };
                unitOfWork.GetRepository<StockAdjustment>().Insert(stkAdj);
                unitOfWork.SaveChanges();

                StockAdjustmentDetail stkAdjDetail = new StockAdjustmentDetail()
                {
                    stockAdjustmentId = stkAdj.Id,
                    StationeryId = eachInfo.empId,
                    discpQty = -(eachInfo.quantity),
                    comment = eachInfo.reason,
                    Status = "Reverted"
                };
                unitOfWork.GetRepository<StockAdjustmentDetail>().Insert(stkAdjDetail);
                unitOfWork.SaveChanges();

                Stationery stationery = unitOfWork
               .GetRepository<Stationery>()
               .GetAllIncludeIQueryable(filter: x => x.Id == eachInfo.itemCode).FirstOrDefault();
                if (stationery != null)
                {
                    stationery.inventoryQty += -(eachInfo.quantity);
                    unitOfWork.GetRepository<Stationery>().Update(stationery);
                    unitOfWork.SaveChanges();
                }
            }
            return await StockAdjustDetailInfo();
        }
        public async Task<IList<AdjustmentVocherInfo>> issueVoucher(StockAdjustSumById voc)
        {
            IList<AdjustmentVocherInfo> list = await getAllAdjustDetailLineByAdjustId(voc);
            List<AdjustmentVocherInfo> voucherResult = new List<AdjustmentVocherInfo>();

            foreach (AdjustmentVocherInfo eachInfo in list)
            {
                StockAdjustmentDetail stkDetail = unitOfWork
               .GetRepository<StockAdjustmentDetail>()
               .GetAllIncludeIQueryable(filter: x => x.Id == eachInfo.stockAdustmentDetailId).FirstOrDefault();

                if (stkDetail != null)
                {
                    stkDetail.Status = "Approved";
                    unitOfWork.GetRepository<StockAdjustmentDetail>().Update(stkDetail);
                    unitOfWork.SaveChanges();
                }

                Employee empObj = unitOfWork
                .GetRepository<Employee>()
                .GetAllIncludeIQueryable(filter: x => x.Id == eachInfo.empId).FirstOrDefault();


                AdjustmentVoucher adjVoc = new AdjustmentVoucher()
                {
                    StockAdjustmentId = eachInfo.stockAdustmentId,
                    EmployeeId = eachInfo.empId,
                    Employee = empObj,
                    date = DateTime.Now
                };
                unitOfWork.GetRepository<AdjustmentVoucher>().Insert(adjVoc);
                unitOfWork.SaveChanges();

                AdjustmentVoucherDetail vocDetail = new AdjustmentVoucherDetail()
                {
                    adjustmentVoucherId = adjVoc.Id,
                    StockAdjustmentDetailId = eachInfo.stockAdustmentDetailId,
                    price = eachInfo.amount,
                    reason = eachInfo.reason,
                };
                unitOfWork.GetRepository<AdjustmentVoucherDetail>().Insert(vocDetail);
                unitOfWork.SaveChanges();

                AdjustmentVocherInfo obj = await getEachVoucherDetail(eachInfo);
                if (obj != null)
                {
                    voucherResult.Add(obj);
                }

            }
            return voucherResult;
        }

        public async Task<IList<StockAdjustSumById>> StockAdjustDetailInfo()
        {
            IList<StockAdjustSumById> stockAdjustSumByIdList = new List<StockAdjustSumById>();
            float amounttotal = 0;
            IList<StockAdjustment> stoAdjlist = await findAllStockAdjustmentAsync();
            IList<StockAdjustmentDetail> stoAdjDetaillist = await findAllStockAdjustDetailAsync();
            IList<AdjustmentVoucherDetail> vocDetails = await findAllAdjustmentVoucherDetailAsync();
            IList<AdjustmentVoucher> vocList = await findAllAdjustmentVoucherAsync();
            bool isApprove = false;
            StockAdjustSumById adjustSumbyId = new StockAdjustSumById() { };

            foreach (StockAdjustment eachSAdjRecord in stoAdjlist)
            {
                adjustSumbyId = null;
                amounttotal = 0;

                List<StockAdjustmentDetail> stockAdjDetailList = unitOfWork
               .GetRepository<StockAdjustmentDetail>()
               .GetAllIncludeIQueryable(filter: x => x.stockAdjustmentId == eachSAdjRecord.Id && x.Status != "Reverted" && x.Status!= "Rejected" && x.Status!="Approved" && x.discpQty!=0).ToList();

                if (stockAdjDetailList != null)
                {
                    foreach (StockAdjustmentDetail eachSAdjDetailRecord in stockAdjDetailList)
                    {
                        isApprove = false;
                        foreach (AdjustmentVoucherDetail eachVocRecord in vocDetails)
                        {
                            if (eachSAdjDetailRecord.Id == eachVocRecord.StockAdjustmentDetailId)
                            {
                                isApprove = true;
                            }
                        }

                        SupplierItem supplierItem = await findSupplierItemByIdAsync(eachSAdjDetailRecord.StationeryId);
                        amounttotal += supplierItem.price * eachSAdjDetailRecord.discpQty;
                        double eachItemAmount = supplierItem.price * eachSAdjDetailRecord.discpQty;
                        if (Math.Abs(eachItemAmount) <= 250)
                        {
                            StockAdjustment stockAdjustment = await findStockAdjustmentByIdAsync(eachSAdjDetailRecord.stockAdjustmentId);
                            if (stockAdjustment != null)
                            {
                                Employee emp = await findEmployeeByIdAsync(stockAdjustment.EmployeeId);
                                if (emp != null)
                                {
                                    adjustSumbyId = new StockAdjustSumById()
                                    {
                                        stockAdustmentId = eachSAdjRecord.Id,
                                        empId = emp.Id,
                                        empName = emp.name,
                                        amount = amounttotal
                                    };
                                }
                            }
                        }
                    } //End StockAdjustmentDetail loop
                    if (!isApprove && adjustSumbyId!=null) 
                    {
                        stockAdjustSumByIdList.Add(adjustSumbyId); // Sum
                    }
                }
            }

            return stockAdjustSumByIdList;
        }
        //end
        public async Task<IList<AdjustmentVocherInfo>> getAllAdjustDetailLineByAdjustId(StockAdjustSumById item)
        {
            float amounttotal = 0;
            IList<AdjustmentVocherInfo> voucherInfoList = new List<AdjustmentVocherInfo>();
            IList<AdjustmentVoucherDetail> vocDetails = await findAllAdjustmentVoucherDetailAsync();

            IList<StockAdjustmentDetail> list = unitOfWork
               .GetRepository<StockAdjustmentDetail>()
               .GetAllIncludeIQueryable(filter: x => x.stockAdjustmentId == item.stockAdustmentId && x.Status != "Reverted" && x.Status != "Rejected" && x.Status != "Approved" && x.discpQty != 0).ToList();

            IList<AdjustmentVoucherDetail> vocList = await findAllAdjustmentVoucherDetailAsync();

            bool isApprove = false;
            foreach (StockAdjustmentDetail eachSAdjDetailRecord in list)
            {
                amounttotal = 0;
                isApprove = false;
                foreach (AdjustmentVoucherDetail eachVocDetailRecord in vocDetails)
                {
                    if (eachVocDetailRecord.StockAdjustmentDetailId == eachSAdjDetailRecord.Id)
                    {
                        isApprove = true;
                    }
                }
                if (!isApprove && eachSAdjDetailRecord.Status!="Reverted" && eachSAdjDetailRecord.Status!="Rejected")
                {
                    SupplierItem supplierItem = await findSupplierItemByIdAsync(eachSAdjDetailRecord.StationeryId);
                    amounttotal = supplierItem.price * eachSAdjDetailRecord.discpQty;

                    double eachItemAmount= supplierItem.price * eachSAdjDetailRecord.discpQty;

                    StockAdjustment stockAdjustment = await findStockAdjustmentByIdAsync(eachSAdjDetailRecord.stockAdjustmentId);
                    if (stockAdjustment != null && Math.Abs(eachItemAmount) <= 250)
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
                    
                }
            }

            return voucherInfoList;
        }
        public async Task<StockAdjustment> findStockAdjustmentByIdAsync(int stockAdjustmentId)
        {
            StockAdjustment stkadj = await unitOfWork.GetRepository<StockAdjustment>()
                .FindAsync(stockAdjustmentId);
            return stkadj;
        }
        public async Task<AdjustmentVocherInfo> getEachVoucherDetail(AdjustmentVocherInfo info)
        {
            AdjustmentVoucherDetail vocDetail = unitOfWork
                .GetRepository<AdjustmentVoucherDetail>()
                .GetAllIncludeIQueryable(filter: x => x.StockAdjustmentDetailId == info.stockAdustmentDetailId).FirstOrDefault();

            if (vocDetail != null)
            {
                Employee empObj = unitOfWork
                .GetRepository<Employee>()
                .GetAllIncludeIQueryable(filter: x => x.Id == info.empId).FirstOrDefault();


                AdjustmentVoucher voc = unitOfWork
                .GetRepository<AdjustmentVoucher>()
                .GetAllIncludeIQueryable(filter: x => x.Id == vocDetail.adjustmentVoucherId).FirstOrDefault();

                if (voc != null && empObj!=null)
                {
                    AdjustmentVocherInfo obj = new AdjustmentVocherInfo()
                    {
                        vocNo = voc.Id,
                        stockAdustmentDetailId = vocDetail.Id,
                        stockAdustmentId = voc.StockAdjustmentId,
                        empId = voc.EmployeeId,
                        date = voc.date,
                        reason = vocDetail.reason,
                        empName = empObj.name,
                        itemCode = info.itemCode,
                        quantity = info.quantity,
                        amount = vocDetail.price
                    };
                    return obj;
                }

            }

            return null;
        }


    }
}
