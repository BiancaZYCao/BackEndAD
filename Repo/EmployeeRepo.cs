using BackEndAD.DataContext;
using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Repo
{
    public interface IEmployeeRepo
    {
        public List<Employee> findAllEmp();
        public Employee findEmployeeById(int id);
        public List<Employee> findEmployeeByDeptName(string dname);
        public bool saveNewEmpToDB(Employee employee);
    }
    public class EmployeeRepo : IEmployeeRepo
    {
        private ProjectContext _context;

        public EmployeeRepo(ProjectContext dbcontext)
        {
            _context = dbcontext;
        }

        public List<Employee> findAllEmp()
        {
            List<Employee> emplist = _context.Employee_Table.ToList();
            return emplist;
        }

        public List<Employee> findEmployeeByDeptName(string dname)
        {
            string departmentName = dname;
            Department dept = _context.Department_Table.FirstOrDefault(x => x.deptName == departmentName);
            int deptId = dept.Id;

            //Linq method
            var elist = from employee in _context.Employee_Table
                                   where employee.DepartmentId == deptId
                                   select employee;
            List<Employee> elist2 = elist.ToList();
            return elist2;
        }

        public Employee findEmployeeById(int id)
        {
            Employee emp = _context.Employee_Table.FirstOrDefault(x => x.Id == id);
            return emp;
        }

        public bool saveNewEmpToDB(Employee employee)
        {
            _context.Employee_Table.Add(employee);
            _context.SaveChanges();
            return true;
        }
    }
}
