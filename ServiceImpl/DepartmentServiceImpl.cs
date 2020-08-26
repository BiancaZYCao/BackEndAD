using BackEndAD.DataContext;
using BackEndAD.Models;
using BackEndAD.Repo;
using BackEndAD.ServiceInterface;
using BackEndAD.TempService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceImpl
{
    public class DepartmentServiceImpl : IDepartmentService
    {
        public IUnitOfWork<ProjectContext> unitOfWork;
        

        public DepartmentServiceImpl(IUnitOfWork<ProjectContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region department
        public async Task<IList<Department>> findAllDepartmentsAsync()
        {
            IList<Department> deptlist = await unitOfWork.GetRepository<Department>().GetAllAsync();
            return deptlist;
        }

        public async Task<Department> findDepartmentByIdAsync(int deptId)
        {
            //for repo
            //Department dept = await deptrepo.findDepartmentByIdAsync(id);
            Department dept = await unitOfWork.GetRepository<Department>().FindAsync(deptId);
            return dept;
        }
        public IList<Department> findAllDepartmentsAsyncEager()
        {
            IList<Department> deptlist = 
                unitOfWork.GetRepository<Department>()
                .GetAllIncludeIQueryable(null, null,"Collection").ToList();
            /*
            IList<Department> deptlist = await
                unitOfWork.GetRepository<Department>().GetAllAsync(null,null,
                    s => s.Include(de => de.Collection).ThenInclude(coll => coll.Id)
                    );*/
            return deptlist;
        }

        public void updateDeptCollectionPt(int DeptId, int CollectionId)
        {
	        var resultDepartment = unitOfWork.GetRepository<Department>().GetById(DeptId);
	        if (resultDepartment != null)
	        {
		        resultDepartment.CollectionId = CollectionId;
		        unitOfWork.GetRepository<Department>().Update(resultDepartment);
		        unitOfWork.SaveChanges();
	        }
        }

        public void updateDeptDelegate(Department departmentToUpdate)
        {
	        var resultDepartment = unitOfWork.GetRepository<Department>().GetById(departmentToUpdate.Id);
	        if (resultDepartment != null)
	        {
		        resultDepartment.delgtStartDate = departmentToUpdate.delgtStartDate;
		        resultDepartment.delgtEndDate = departmentToUpdate.delgtEndDate;
		        unitOfWork.GetRepository<Department>().Update(resultDepartment);
		        unitOfWork.SaveChanges();
	        }
        }

        public void updateDeptEmp(int oldId, string oldRole, int newId, string newRole)
        {
	        Employee oldEmp = unitOfWork.GetRepository<Employee>().GetById(oldId);
	        if (oldEmp != null)
	        {
		        oldEmp.role = oldRole;
		        unitOfWork.GetRepository<Employee>().Update(oldEmp);
		        unitOfWork.SaveChanges();
	        }

	        Employee newEmp = unitOfWork.GetRepository<Employee>().GetById(newId);
	        if (newEmp != null)
	        {
		        newEmp.role = newRole;
		        unitOfWork.GetRepository<Employee>().Update(newEmp);
		        unitOfWork.SaveChanges();
	        }
        }

        public void updateDeptEmpRevoke(int oldId, string oldRole)
        {
	        Employee oldEmp = unitOfWork.GetRepository<Employee>().GetById(oldId);
	        if (oldEmp != null)
	        {
		        oldEmp.role = oldRole;
		        unitOfWork.GetRepository<Employee>().Update(oldEmp);
		        unitOfWork.SaveChanges();
	        }
        }
        #endregion

        #region requsition
        public async Task<IList<Requisition>> findAllRequsitionsAsync()
        {
            IList<Requisition> reqlist = await unitOfWork.GetRepository<Requisition>().GetAllAsync();
            return reqlist;
        }

        public async Task<IList<Requisition>> findAllRequsitionsByEmpIdAsync(int empId)
        {
            IList<Requisition> reqlist = unitOfWork
                       .GetRepository<Requisition>()
                       .GetAllIncludeIQueryable(filter: x => x.EmployeeId == empId).ToList();
            return reqlist;

        }

        public void updateRequisition(int requisitionId, DateTime? reqDateOfAuthorizing, string reqStatus, string reqComment)
        {
	        var resultRequisition = unitOfWork.GetRepository<Requisition>().GetById(requisitionId);
	        if (resultRequisition != null)
	        {
		        resultRequisition.dateOfAuthorizing = reqDateOfAuthorizing;
		        resultRequisition.status = reqStatus;
		        resultRequisition.comment = reqComment;

		        unitOfWork.GetRepository<Requisition>().Update(resultRequisition);
		        unitOfWork.SaveChanges();
	        }
        }
        #endregion

        #region requisition details
        public async Task<IList<RequisitionDetail>> findAllRequsitionDetailAsync()
        {
            IList<RequisitionDetail> detailsLists = await unitOfWork.GetRepository<RequisitionDetail>().GetAllAsync();
            return detailsLists;
        }

        public async Task<IList<RequisitionDetailsList>> findAllRequisitionDetailsItemListById(Requisition req)
        {
            IList<RequisitionDetailsList> reqDList = new List<RequisitionDetailsList>();
            // IList<RequisitionDetail> reqDetail = await findAllRequsitionDetailAsync();
            IList<Stationery> stationery = await findAllStationeryAsync();

            //retrieve list of req detail with equal id
            IList<RequisitionDetail> rList = unitOfWork
                .GetRepository<RequisitionDetail>()
                .GetAllIncludeIQueryable(filter: x => x.RequisitionId == req.Id).ToList();

            Employee employee = unitOfWork
                .GetRepository<Employee>()
                .GetAllIncludeIQueryable(filter: x => x.Id == req.EmployeeId).FirstOrDefault();

            foreach (RequisitionDetail reqDetailRecord in rList)
            {
                foreach (Stationery sItem in stationery)
                {
                    if(reqDetailRecord.StationeryId == sItem.Id && req.status == "Applied")
                    {
                        RequisitionDetailsList requisition = new RequisitionDetailsList()
                        {
                            requisitionDetailsId = reqDetailRecord.Id,
                            requisitionId = reqDetailRecord.RequisitionId,
                            description = sItem.desc,
                            quantity = reqDetailRecord.reqQty,
                            unit = sItem.unit,
                            status = reqDetailRecord.status
                        };
                        reqDList.Add(requisition);
                    }
                    else if (reqDetailRecord.StationeryId == sItem.Id)
                    {
                        RequisitionDetailsList requisition = new RequisitionDetailsList()
                        {
                            authorizer = employee.name,
                            authorizedDate = req.dateOfAuthorizing,
                            requisitionDetailsId = reqDetailRecord.Id,
                            requisitionId = reqDetailRecord.RequisitionId,
                            description = sItem.desc,
                            quantity = reqDetailRecord.reqQty,
                            unit = sItem.unit,
                            status = reqDetailRecord.status
                        };
                        reqDList.Add(requisition);
                    }
                }
            }
            return reqDList;
        }

        public void updateRequisitionDetail(int requisitionId, string reqDetailStatus)
        {
	        var allReqDetailList = unitOfWork.GetRepository<RequisitionDetail>().GetAll();
	        List<RequisitionDetail> resultReqDetailList = new List<RequisitionDetail>();

	        foreach (RequisitionDetail reqDetail in allReqDetailList)
	        {
		        if (reqDetail.RequisitionId == requisitionId)
		        {
			        resultReqDetailList.Add(reqDetail);
		        }
	        }

	        if (resultReqDetailList != null)
	        {
		        foreach (RequisitionDetail reqDetail in resultReqDetailList)
		        {
			        reqDetail.status = reqDetailStatus;

			        unitOfWork.GetRepository<RequisitionDetail>().Update(reqDetail);
			        unitOfWork.SaveChanges();
		        }
	        }
        }
        #endregion

        #region requisition apply
        public async Task<IList<Requisition>> applyRequisition(List<RequisitionDetailsApply> reqList,int empId)
        {
            Requisition requisition = new Requisition()
            {//@WuttYee here hardcore empID,AuthorizerId?,dateOfAuthorizing? should change 
                EmployeeId = empId,
                dateOfRequest = DateTime.Now,
                //dateOfAuthorizing = DateTime.Now,//can leave null
                AuthorizerId = 2,//if must not null ,pass headID ; delegate need to be updated when approved.
                status = "Applied",
            };
            unitOfWork.GetRepository<Requisition>().Insert(requisition);
            unitOfWork.SaveChanges();

            foreach (RequisitionDetailsApply reqDetails in reqList)
            {

                Stationery stationeries = unitOfWork
                    .GetRepository<Stationery>()
                    .GetAllIncludeIQueryable(filter: x => x.desc == reqDetails.desc).FirstOrDefault();

                RequisitionDetail reqDetail1 = new RequisitionDetail()
                {
                    RequisitionId = requisition.Id,
                    StationeryId = stationeries.Id,
                    reqQty = reqDetails.reqQty,
                    status = "Applied",
                };
                unitOfWork.GetRepository<RequisitionDetail>().Insert(reqDetail1);
                unitOfWork.SaveChanges();
            }

            return await findAllRequsitionsAsync();
        }

        public async Task<Stationery> getItemByDesc(String desc)
        {
            Stationery sta = unitOfWork
             .GetRepository<Stationery>()
             .GetAllIncludeIQueryable(filter: x => x.desc == desc).FirstOrDefault();

            return sta;
        }
        #endregion

        #region stationery
        public async Task<IList<Stationery>> findAllStationeryAsync()
        {
            IList<Stationery> stationeryList = await unitOfWork.GetRepository<Stationery>().GetAllAsync();
            return stationeryList;
        }
        #endregion

        #region Employee
        public async Task<Employee> findEmployeeByIdAsync(int empid)
        {
            Employee emp = await unitOfWork.GetRepository<Employee>().FindAsync(empid);
            return emp;
        }
        public async Task<IList<Employee>> findAllEmployeesAsync()
        {
            IList<Employee> emplist = await unitOfWork.GetRepository<Employee>().GetAllAsync();
            //IIncludableQueryable<TEntity, object>> include = null,
            return emplist;
        }

        public async Task<IList<CollectionInfo>> findAllCollectionPointAsync()
        {
            IList<CollectionInfo> collectionpts = await unitOfWork.GetRepository<CollectionInfo>().GetAllAsync();

            return collectionpts;
        }
        #endregion


    }
}
