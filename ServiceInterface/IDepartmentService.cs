using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.TempService;

namespace BackEndAD.ServiceInterface
{
    public interface IDepartmentService
    {

        public Task<IList<Department>> findAllDepartmentsAsync();
        public IList<Department> findAllDepartmentsAsyncEager();   
        public Task<Department> findDepartmentByIdAsync(int id);
        public Task<IList<Requisition>> findAllRequsitionsAsync();
        public Task<IList<Employee>> findAllEmployeesAsync();
        public Task<Employee> findEmployeeByIdAsync(int empid);
        public Task<IList<CollectionInfo>> findAllCollectionPointAsync();

        public Task<IList<RequisitionDetail>> findAllRequsitionDetailAsync();
        public Task<IList<Stationery>> findAllStationeryAsync();
        public Task<IList<RequisitionDetailsList>> findAllRequisitionDetailsItemListById(Requisition req);
        public Task<IList<Requisition>> applyRequisition(List<RequisitionDetailsApply> requisition,int empId);
        public void updateRequisition(int requisitionId, DateTime? reqDateOfAuthorizing, string reqStatus,
	        string reqComment);
        public void updateRequisitionDetail(int requisitionId, string reqDetailStatus);
        public Task<Stationery> getItemByDesc(String desc);
        public void updateDeptCollectionPt(int DeptId, int CollectionId);
        public void updateDeptDelegate(Department departmentToUpdate);
        public void updateDeptEmp(int oldId, string oldRole, int newId, string newRole);
        public void updateDeptEmpRevoke(int oldId, string oldRole);
    }
}
