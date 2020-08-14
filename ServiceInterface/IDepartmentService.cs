using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceInterface
{
    public interface IDepartmentService
    {
        public Task<List<Department>> findAllDepartmentsAsync();
        public Department findDepartmentByName(String deptName);
        public Task<Department> findDepartmentByIdAsync(int id);


    }
}
