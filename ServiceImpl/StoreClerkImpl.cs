using BackEndAD.DataContext;
using BackEndAD.Models;
using BackEndAD.Repo;
using BackEndAD.TempService;
using BackEndAD.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

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
                s1.unit = stationery.unit;
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
            unitOfWork.SaveChanges();
        }

        public Task<SupplierItem> findAllSupplierItemByIdAsync(int stkAdjId)
        {
            throw new NotImplementedException();
        }

        public void savePurchaseOrderDetail(PurchaseOrderDetail pod)
        {
            unitOfWork.GetRepository<PurchaseOrderDetail>().Insert(pod);
            unitOfWork.SaveChanges();
        }


        //Bianca PO-step2
        public IList<SupplierItem> findSuppliersByStationeryId(int id)
        {
            IList<SupplierItem> itemlist = unitOfWork
                .GetRepository<SupplierItem>()
                .GetAllIncludeIQueryable(filter: x => x.StationeryId == id).ToList();
            return itemlist;
        }

        public async Task<PurchaseOrder> findPOById(int id) {
            
            PurchaseOrder po= await unitOfWork.GetRepository<PurchaseOrder>().FindAsync(id);

            return po;

        }
        public IList<PurchaseOrderDetail> findPODById(int id) {

            IList<PurchaseOrderDetail> podlist = unitOfWork
                .GetRepository<PurchaseOrderDetail>()
                .GetAllIncludeIQueryable(filter: x => x.PurchaseOrderId == id).ToList();

            return podlist;
        }

    
        #endregion

        //Disbursement
        public async Task<Employee> findEmployeeByIdAsync(int eId)
        {
            Employee e = await unitOfWork.GetRepository<Employee>().FindAsync(eId);
            return e;
        }
        public async Task<IList<DisbursementList>> findAllDisbursementListAsync()
        {
            IList<DisbursementList> list = await unitOfWork.GetRepository<DisbursementList>().GetAllAsync();
            return list;
        }
        public async Task<IList<RequesterRow>> GetAllRequesterRow()
        {
            IList<DisbursementList> disbursementlist = await findAllDisbursementListAsync();

            IList<RequesterRow> resultList = new List<RequesterRow>();
           /* RequesterRow row1 = new RequesterRow()
            {
                date = DateTime.Today,
                departmentId = 1,
                departmentName = "Zoology Department",
                itemCount = 1,
                status = "Approved",
                //representativeName = emp.name;
                representativeName = "Mr.John",
            };

            resultList.Add(row1);*/

            foreach (DisbursementList disburseList in disbursementlist)
            {
                List<DisbursementDetail> disburseDetailList = unitOfWork
                   .GetRepository<DisbursementDetail>()
                   .GetAllIncludeIQueryable(filter: x => x.DisbursementListId == disburseList.id).ToList();

                String requisitionStatus = "";
                int itemCountTotal = 0;
                RequisitionDetail requestionDetail = new RequisitionDetail();

                foreach (DisbursementDetail disburseDetail in disburseDetailList)
                {
                    itemCountTotal += disburseDetail.qty;

                    requestionDetail = unitOfWork
                   .GetRepository<RequisitionDetail>()
                   .GetAllIncludeIQueryable(filter: x => x.Id == disburseDetail.RequisitionDetailId).FirstOrDefault();

                }
                Department dept = unitOfWork
                       .GetRepository<Department>()
                       .GetAllIncludeIQueryable(filter: x => x.Id == disburseList.DepartmentId).FirstOrDefault();

                if (dept != null && requestionDetail != null)
                {
                    Requisition requisition = unitOfWork
                      .GetRepository<Requisition>()
                      .GetAllIncludeIQueryable(filter: x => x.Id == requestionDetail.RequisitionId).FirstOrDefault();
                    Employee emp = unitOfWork
                      .GetRepository<Employee>()
                      .GetAllIncludeIQueryable(filter: x => x.departmentId == dept.Id)
                      .Where(x => x.role == "REPRESENTATIVE").FirstOrDefault();
                    //Employee emp = findEmployeeByIdAsync(dept.repId);
                    if (requisition != null)
                    {
                        RequesterRow row = new RequesterRow()
                        {
                            date = disburseList.date,
                            disbursementListId = disburseList.id,
                            departmentId = disburseList.DepartmentId,
                            departmentName = dept.deptName,
                            itemCount = itemCountTotal,
                            status = requisition.status,
                            collectionPoint = disburseList.deliveryPoint,
                            representativeName = emp.name
                        };
                        resultList.Add(row);
                    }
                }

            }//end forEach
            return resultList;
        }

        public async Task<IList<DisburseItemDetails>> getDisburseItemDetail(RequesterRow row)
        {
            IList<DisburseItemDetails> returnList = new List<DisburseItemDetails>();
            /*DisburseItemDetails itemObj1 = new DisburseItemDetails()
            {
                itemDescription = "Clip Double 1\"",
                requisitionDetailId = 1,
                requisitionId = 1,
                revQuantity = 5,
            };
            returnList.Add(itemObj1);*/

            List<DisbursementDetail> detailList = unitOfWork
               .GetRepository<DisbursementDetail>()
               .GetAllIncludeIQueryable(filter: x => x.DisbursementListId == row.disbursementListId).ToList();

            foreach(DisbursementDetail detail in detailList)
            {
                RequisitionDetail requisitionDetail = unitOfWork
               .GetRepository<RequisitionDetail>()
               .GetAllIncludeIQueryable(filter: x => x.Id == detail.RequisitionDetailId).FirstOrDefault();

                if (requisitionDetail != null)
                {
                    Stationery stationery = unitOfWork
                   .GetRepository<Stationery>()
                   .GetAllIncludeIQueryable(filter: x => x.Id == requisitionDetail.StationeryId).FirstOrDefault();

                    if (stationery != null)
                    {
                        DisburseItemDetails itemObj = new DisburseItemDetails()
                        {
                            itemDescription = stationery.desc,
                            requisitionDetailId = requisitionDetail.Id,
                            requisitionId = requisitionDetail.RequisitionId,
                            revQuantity = requisitionDetail.rcvQty,
                        };
                        returnList.Add(itemObj);
                    }
                }
            }
            return returnList;
        }

        //end

        public Task<IList<Requisition>> findAllRequsitionAsync()
        {
            return unitOfWork.GetRepository<Requisition>().GetAllAsync();
            
        }

        public Task<IList<RequisitionDetail>> findAllRequsitionDetailsAsync()
        {
           return unitOfWork.GetRepository<RequisitionDetail>().GetAllAsync();
        }

        public Task<IList<StockAdjustSumById>> StockAdjustDetailInfo()
        {
            throw new NotImplementedException();
        }

        public Task<IList<AdjustmentVocherInfo>> getAllAdjustDetailLineByAdjustId(StockAdjustSumById item)
        {
            throw new NotImplementedException();
        }

        public Task<AdjustmentVocherInfo> getEachVoucherDetail(AdjustmentVocherInfo info)
        {
            throw new NotImplementedException();
        }

        public Task<IList<AdjustmentVocherInfo>> issueVoucher(StockAdjustSumById voc)
        {
            throw new NotImplementedException();
        }

        public void saveStockAdjustment(StockAdjustment newSA)
        {
            unitOfWork.GetRepository<StockAdjustment>().Insert(newSA);
            unitOfWork.SaveChanges();
        }

        public void saveStockAdjustmentDetail(StockAdjustmentDetail SAD)
        {
            unitOfWork.GetRepository<StockAdjustmentDetail>().Insert(SAD);
            unitOfWork.SaveChanges();
        }

        public Task<IList<Department>> findAllDepartmentAsync()
        {
            return unitOfWork.GetRepository<Department>().GetAllAsync();
        }

        public void updateStationery(Stationery s)
        {
            unitOfWork.GetRepository<Stationery>().Update(s);
            unitOfWork.SaveChanges();
        }

        public Task<IList<CollectionInfo>> findAllCollectionPointAsync()
        {
            return unitOfWork.GetRepository<CollectionInfo>().GetAllAsync() ;
        }

        public void saveDisbursementList(DisbursementList newDL)
        {
            unitOfWork.GetRepository<DisbursementList>().Insert(newDL);
            unitOfWork.SaveChanges();
        }

        public void saveDisbursementDetail(DisbursementDetail currDB)
        {
            unitOfWork.GetRepository<DisbursementDetail>().Insert(currDB);
            unitOfWork.SaveChanges();
        }

        public void udpateRequisitionDetail(RequisitionDetail rd)
        {
            unitOfWork.GetRepository<RequisitionDetail>().Update(rd);
            unitOfWork.SaveChanges();
        }
    }
}
