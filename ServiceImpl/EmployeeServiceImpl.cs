using BackEndAD.Models;
using BackEndAD.Repo;
using BackEndAD.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceImpl
{
    public class EmployeeServiceImpl : IEmployeeService
    {
        public IEmployeeRepo erepo;
        public EmployeeServiceImpl(IEmployeeRepo erepo)
        {
            this.erepo = erepo;
        }
        public List<Employee> findAllEmployees()
        {
            //interface with persistence layer to give output
            //manage the information from persistance layer and apply business logic to be returned to the controller
            //TODO: business logic
            List<Employee> elist = erepo.findAllEmp();
            return elist;
        }

        public List<Employee> findEmployeeByDeptName(string dname)
        {
            return erepo.findEmployeeByDeptName(dname);
        }

        public Employee findEmployeeById(int empid)
        {
            return erepo.findEmployeeById(empid);
        }

        public bool saveEmp(Employee employee)
        {
            return erepo.saveNewEmpToDB(employee);
            
           
        }
    }
}
