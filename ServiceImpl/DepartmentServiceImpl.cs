using BackEndAD.Models;
using BackEndAD.Repo;
using BackEndAD.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceImpl
{
    public class DepartmentServiceImpl : IDepartmentService
    {
        public IDepartmentRepo deptrepo;

        public DepartmentServiceImpl(IDepartmentRepo deptrepo)
        {
            this.deptrepo = deptrepo;
        }
        public async Task<List<Department>> findAllDepartmentsAsync()
        {
            List<Department> deptlist = await deptrepo.findAllDepartmentsAsync();
            return deptlist;
        }
        public Department findDepartmentByName(String deptName)
        {
            Department dept = deptrepo.findDepartmentByName(deptName);
            return dept;
        }
        public async Task<Department> findDepartmentByIdAsync(int id)
        {
            //for repo
            //Person randomPerson = await DataContext.People.FirstOrDefaultAsync(x => x.Id == id);
            Department dept = await deptrepo.findDepartmentByIdAsync(id);
            return dept;
        }
    }
}
