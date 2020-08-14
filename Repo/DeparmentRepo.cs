using BackEndAD.DataContext;
using BackEndAD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Repo
{
    public interface IDepartmentRepo
    {
        public Task<List<Department>> findAllDepartmentsAsync();
        public Department findDepartmentByName(String _deptName);
        public Task<Department> findDepartmentByIdAsync(int id);
    }
    public class DeparmentRepo : IDepartmentRepo
    {
        private ProjectContext _context;
        public DeparmentRepo(ProjectContext _context)
        {
            this._context = _context;
        }

        //pending changing into async pending test
        public async Task<List<Department>> findAllDepartmentsAsync()
        {
            List<Department> dlist = await _context.Department_Table.ToListAsync();
            return dlist;
        }
        //this method may not be used in deptcontroller because got prob. 
        public Department findDepartmentByName(String _deptName)
        {
            Department dept = _context.Department_Table.FirstOrDefault(x => x.deptName == _deptName);
            return dept;
        }

        //for repo we also need async operation which will be called by serviceImpl 
        //then controller can use via service layer
        //Person person = await DataContext.People.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Department> findDepartmentByIdAsync(int id)
        {
            Department dept = await _context.Department_Table.FirstOrDefaultAsync(x => x.Id == id);
            return dept;
        }

    }
}
