using System;
using BackEndAD.DataContext;
using BackEndAD.Models;
using BackEndAD.Repo;


public static class DBSeed
{
    // database initializer
    public static bool Initialize(IUnitOfWork<ProjectContext> unitOfWork)
    {
        bool isCreateDb = false;
        unitOfWork.DbContext.Database.EnsureDeleted();
        if (unitOfWork.DbContext.Database.EnsureCreated())
        {
            isCreateDb = true;

            #region Department
            //Here is an example for seeding data.

            Department dENGL = new Department()
            {
                deptName = "EngLish Dept",
                deptCode = "ENGL"
            };
            unitOfWork.GetRepository<Department>().Insert(dENGL);

            Department dCPCS = new Department()
            {
                deptName = "Computer Science",
                deptCode = "CPCS"
            };
            unitOfWork.GetRepository<Department>().Insert(dCPCS);

            Department dCOMM = new Department()
            {
                deptName = "Commerce Dept",
                deptCode = "COMM"
            };
            unitOfWork.GetRepository<Department>().Insert(dCOMM);

            #endregion
            #region Test TodoItem
            TodoItem tdi = new TodoItem()
            {
                Name = "Cook dinner",
                IsComplete = false,
                Secret = "Dangerous Secret info"
            };
            unitOfWork.GetRepository<TodoItem>().Insert(tdi);
            #endregion
            #region stationery
            Stationery i1 = new Stationery()
            {
                name = "pen_black",
                quantity = 10
            };
            Stationery i2 = new Stationery()
            {
                name = "pen_blue",
                quantity = 10
            };
            Stationery i3 = new Stationery()
            {
                name = "pencil",
                quantity = 30
            };
            unitOfWork.GetRepository<Stationery>().Insert(i1);
            unitOfWork.GetRepository<Stationery>().Insert(i2);
            unitOfWork.GetRepository<Stationery>().Insert(i3);
            #endregion
            #region Employee
            Employee e1 = new Employee()
            {
                name = "Mary",
                password = "123",
                //DepartmentId = 1,
                Department = dENGL,
                email = "mary@gmail.com"
            };

            Employee e2 = new Employee()
            {
                name = "John",
                password = "123",
                //DepartmentId = 1,
                Department = dENGL,
                email = "john@gmail.com"
            };

            Employee e3 = new Employee()
            {
                name = "Peter",
                password = "123",
                //DepartmentId = 3,
                Department = dCOMM,
                email = "peter@gmail.com"
            };

            unitOfWork.GetRepository<Employee>().Insert(e1);
            unitOfWork.GetRepository<Employee>().Insert(e2);
            unitOfWork.GetRepository<Employee>().Insert(e3);
            #endregion
            #region requisition +detail lists
            Requisition r1 = new Requisition()
            {
                Employee = e1,
                dateOfRequest = new DateTime(2017, 9, 9, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2017, 9, 10, 8, 30, 52),
                Authorizer = e2,
                status = "Approved",
                comment = null,
            };
            unitOfWork.GetRepository<Requisition>().Insert(r1);

            RequisitionDetail rd1 = new RequisitionDetail()
            {
                Requisition = r1,
                Stationery = i1,
                reqQty = 10,
                status = "approved",

            };
            RequisitionDetail rd2 = new RequisitionDetail()
            {
                Requisition = r1,
                Stationery = i2,
                reqQty = 1,
                status = "approved",

            };
            RequisitionDetail rd3 = new RequisitionDetail()
            {
                Requisition = r1,
                Stationery = i3,
                reqQty = 1,
                status = "approved",

            };
            unitOfWork.GetRepository<RequisitionDetail>().Insert(rd1);
            unitOfWork.GetRepository<RequisitionDetail>().Insert(rd2);
            unitOfWork.GetRepository<RequisitionDetail>().Insert(rd3);
            unitOfWork.SaveChanges();
            #endregion

            #region Supplier
            Supplier s1 = new Supplier()
            {
                supplierCode = "ALPA",
                name = " ALPHA Office Supplies",
                contactPerson = "Ms Irene Tan",
                phoneNum = "4619928",
                gstRegisNo = "MR-8500440-2",
                fax = " 4612238",
                address = "Blk 1128, Ang Mo Kio Industrial Park #02-1108 Ang Mo Kio Street 62 ,Singapore 622262",
                priority = "1st",
            };
            Supplier s2 = new Supplier()
            {
                supplierCode = "CHEP",
                name = " Cheap Stationer",
                contactPerson = "Mr Soh Kway Koh",
                phoneNum = "3543234",
                fax = " 4742434",
                address = "Blk 34, Clementi Road,#07-02 Ban Ban Soh Building,Singapore 110525",
                priority = "2nd",
            };

            Supplier s3 = new Supplier()
            {
                supplierCode = "BANE",
                name = "BANES Shop",
                contactPerson = " Mr Loh Ah Pek",
                phoneNum = "4781234",
                gstRegisNo = "MR-8200420-2",
                fax = "4792434",
                address = "Blk 124, Alexandra Road,#03-04 Banes Building,Singapore 550315",
                priority = "3rd",
            };
            unitOfWork.GetRepository<Supplier>().Insert(s1);
            unitOfWork.GetRepository<Supplier>().Insert(s2);
            unitOfWork.GetRepository<Supplier>().Insert(s3);
            unitOfWork.SaveChanges();
            #endregion
        }
        return isCreateDb;
    }


}