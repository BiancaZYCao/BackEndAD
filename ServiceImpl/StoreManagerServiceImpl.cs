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
    public class StoreManagerServiceImpl: IStoreManagerService
    {
        public IUnitOfWork<ProjectContext> unitOfWork;

        public StoreManagerServiceImpl(IUnitOfWork<ProjectContext> unitOfWork)
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


    }
}
