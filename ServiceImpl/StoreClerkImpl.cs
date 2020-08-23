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
            RequesterRow row1 = new RequesterRow()
            {
                date = DateTime.Today,
                departmentId = 1,
                departmentName = "Zoology Department",
                itemCount = 1,
                status = "Approved"
                //representativeName = emp.name;
            };
            resultList.Add(row1);

            foreach (DisbursementList disburseList in disbursementlist)
            {
                DisbursementDetail disburseDetail = unitOfWork
                   .GetRepository<DisbursementDetail>()
                   .GetAllIncludeIQueryable(filter: x => x.DisbursementListId == disburseList.id).FirstOrDefault();
                
                if (disburseDetail != null)
                {
                    RequisitionDetail requestionDetail = unitOfWork
                   .GetRepository<RequisitionDetail>()
                   .GetAllIncludeIQueryable(filter: x => x.Id == disburseDetail.RequisitionDetailId).FirstOrDefault();
                   
                    Department dept = unitOfWork
                   .GetRepository<Department>()
                   .GetAllIncludeIQueryable(filter: x => x.Id == disburseList.DepartmentId).FirstOrDefault();
                   
                    if (dept != null && requestionDetail!=null)
                    {
                        //Employee emp = findEmployeeByIdAsync(dept.repId);
                        RequesterRow row = new RequesterRow()
                        {
                            date = disburseList.date,
                            departmentId = disburseList.DepartmentId,
                            departmentName = dept.deptName,
                            itemCount = disburseDetail.qty,
                            status = requestionDetail.status
                            //representativeName = emp.name;
                        };
                        resultList.Add(row);
                    }
                }
                
            }//end forEach
            return resultList;
        }
    }
}
