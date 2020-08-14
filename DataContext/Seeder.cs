using BackEndAD.DataContext;
using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.DataContext
{
    //THIS IS DATABASE SEEDER to initialize data
    //should include all the ENTITIES in our ERD
    public class Seeder
    {
        public Seeder(ProjectContext dbContext)
        {
            //Here is an example for seeding data.
            Department dENGL = new Department()
            {
                deptName = "EngLish Dept",
                deptCode = "ENGL"
            };
            dbContext.Add(dENGL);

            Department dCPCS = new Department()
            {
                deptName = "Computer Science",
                deptCode = "CPCS"
            };
            dbContext.Add(dCPCS);

            Department dCOMM = new Department()
            {
                deptName = "Commerce Dept",
                deptCode = "COMM"
            };
            dbContext.Add(dCOMM);
            dbContext.SaveChanges();


            Employee e1 = new Employee()
            {
                name = "Mary",
                password = "123",
                DepartmentId = 1,
                email = "mary@gmail.com"
            };

            Employee e2 = new Employee()
            {
                name = "John",
                password = "123",
                DepartmentId = 1,
                email = "john@gmail.com"
            };

            Employee e3 = new Employee()
            {
                name = "Peter",
                password = "123",
                DepartmentId = 3,
                email = "peter@gmail.com"
            };

            dbContext.Add(e1);
            dbContext.Add(e2);
            dbContext.Add(e3);
            dbContext.SaveChanges();

            TodoItem tdi = new TodoItem()
            {
                Name = "Cook dinner",
                IsComplete = false,
                Secret = "Dangerous Secret info"
            };
            dbContext.Add(tdi);

            dbContext.SaveChanges();

            Inventory i1 = new Inventory()
            {
                name = "pen_black",
                quantity = 10
            };
            Inventory i2 = new Inventory()
            {
                name = "pen_blue",
                quantity = 10
            };
            Inventory i3 = new Inventory()
            {
                name = "pencil",
                quantity = 30
            };
            dbContext.Add(i1);
            dbContext.Add(i2);
            dbContext.Add(i3);
            dbContext.SaveChanges();

        }
    }
}
