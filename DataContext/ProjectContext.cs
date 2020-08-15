using Microsoft.EntityFrameworkCore;
using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.DataContext
{
    public class ProjectContext:DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options): base(options)
        {

        }

        public DbSet<Employee> Employee_Table { get; set; }
        public DbSet<Department> Department_Table { get; set; }
        //Below data tables just for testing 
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<Supplier> Supplier_Table { get; set; }

    }
}
 