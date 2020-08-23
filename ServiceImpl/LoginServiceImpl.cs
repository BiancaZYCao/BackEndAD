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
    public class LoginServiceImpl : ILoginService
    {
        public IUnitOfWork<ProjectContext> unitOfWork;

        public LoginServiceImpl(IUnitOfWork<ProjectContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Employee> findEmployee(string email)
        {
            var emp = await unitOfWork.GetRepository<Employee>().GetAllAsync();
            var emp1 = emp.FirstOrDefault<Employee>(e => e.email.Equals(email));
            return emp1;
        }
    }
}
