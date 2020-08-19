using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
