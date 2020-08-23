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
            Employee emp = await unitOfWork.GetRepository<Employee>().FindAsync(email);
            return emp;
        }
    }
}
