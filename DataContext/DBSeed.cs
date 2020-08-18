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
            //Done yirui W/O clerkId
            #region CollectionInfo
            CollectionInfo ci1 = new CollectionInfo()
            {
                //clerkId = 111,
                collectionTime = new DateTime(2020, 8, 1, 9, 30, 0),
                collectionPoint = "Stationery Store - Administration Building",
                lat = "xxx",
                longi = "yyy",

            };

            CollectionInfo ci2 = new CollectionInfo()
            {
                //clerkId = 111,
                collectionTime = new DateTime(2020, 8, 1, 11, 00, 0),
                collectionPoint = "Management School",
                lat = "xxx",
                longi = "yyy",

            };

            CollectionInfo ci3 = new CollectionInfo()
            {
                //clerkId = 222,
                collectionTime = new DateTime(2020, 8, 2, 9, 30, 0),
                collectionPoint = "Medical School",
                lat = "xxx",
                longi = "yyy",

            };

            CollectionInfo ci4 = new CollectionInfo()
            {
                //clerkId = 222,
                collectionTime = new DateTime(2020, 8, 2, 11, 00, 0),
                collectionPoint = "Engineering School",
                lat = "xxx",
                longi = "yyy",

            };

            CollectionInfo ci5 = new CollectionInfo()
            {
                //clerkId = 333,
                collectionTime = new DateTime(2020, 8, 3, 9, 30, 0),
                collectionPoint = "Science School",
                lat = "xxx",
                longi = "yyy",

            };

            CollectionInfo ci6 = new CollectionInfo()
            {
                //clerkId = 333,
                collectionTime = new DateTime(2020, 8, 3, 11, 00, 0),
                collectionPoint = "University Hospital",
                lat = "xxx",
                longi = "yyy",

            };
            CollectionInfo[] collectionInfoArr = { ci1, ci2, ci3, ci4, ci5, ci6 };
            for (int d = 0; d < collectionInfoArr.Length; d++)
            {
                unitOfWork.GetRepository<CollectionInfo>().Insert(collectionInfoArr[d]);
            }
            unitOfWork.SaveChanges();
            #endregion

            //done by Yirui w/o FK repId headId delegateId
            #region Department
            //dept with store
            Department store = new Department()
            {
                deptName = "Store",
                //headId =31
            };
            Department dENGL = new Department()
            {
                deptName = "EngLish Dept",
                deptCode = "ENGL",
                //headId = 1,
                //repId = 6,
                //delegaterId = 2,
                delgtStartDate = new DateTime(2020, 7, 31),
                delgtEndDate = new DateTime(2020, 8, 9),
                CollectionId = 1

            };
            

            Department dCPSC = new Department()
            {
                deptName = "Computer Science",
                deptCode = "CPSC",
                //headId = 2,
                //repId = 7,
                //delegaterId = null,
                delgtStartDate = new DateTime(2020, 6, 31),
                delgtEndDate = new DateTime(2020, 7, 10),
                CollectionId = 2
            };
            

            Department dCOMM = new Department()
            {
                deptName = "Commerce Dept",
                deptCode = "COMM",
                //headId = 3,
                //repId = 8,
                //delegaterId = 12,
                delgtStartDate = new DateTime(2020, 10, 1),
                delgtEndDate = new DateTime(2020, 10, 5),
                CollectionId = 3
            };
            

            Department dREGR = new Department()
            {
                deptName = "Registrar Dept",
                deptCode = "REGR",
                //headId = 4,
                //repId = 9,
                //delegaterId = 13,
                delgtStartDate = new DateTime(2020, 5, 1),
                delgtEndDate = new DateTime(2020, 5, 5),
                CollectionId = 4
            };

            Department dZOOL = new Department()
            {
                deptName = "Zoology Dept",
                deptCode = "ZOOL",
                //headId = 5,
                //repId = 10,
                //delegaterId = 14,
                delgtStartDate = new DateTime(2019, 12, 20),
                delgtEndDate = new DateTime(2020, 1, 3),
                CollectionId = 5
            };
            Department[] DeptArr = { store,dENGL, dCPSC, dCOMM, dREGR, dZOOL };
            for (int i = 0; i < DeptArr.Length; i++)
            {
                unitOfWork.GetRepository<Department>().Insert(DeptArr[i]);
            }
            unitOfWork.SaveChanges();
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

            //pending
            #region stationery pending change
            Stationery i1 = new Stationery()
            {
                desc = "pen_black",
                inventoryQty = 10
            };
            Stationery i2 = new Stationery()
            {
                desc = "pen_blue",
                inventoryQty = 10
            };
            Stationery i3 = new Stationery()
            {
                desc = "pencil",
                inventoryQty = 30
            };
            unitOfWork.GetRepository<Stationery>().Insert(i1);
            #endregion

            //done Yirui
            #region Employee
            Employee e1 = new Employee()
            {
                name = "Mary",
                password = "123",
                Department = dENGL,
                email = "mary@gmail.com",
                role = "HEAD",
                phoneNum = "91234567",
                DepartmentId = 1
            };

            Employee e2 = new Employee()
            {
                name = "John",
                password = "123",
                Department = dCPSC,
                email = "john@gmail.com",
                role = "HEAD",
                phoneNum = "92234567",
                DepartmentId = 2
            };

            Employee e3 = new Employee()
            {
                name = "Joe",
                password = "123",
                Department = dCOMM,
                email = "joe@gmail.com",
                role = "HEAD",
                phoneNum = "81234567",
                DepartmentId = 3
            };

            Employee e4 = new Employee()
            {
                name = "Peter",
                password = "123",
                Department = dREGR,
                email = "peter@gmail.com",
                role = "HEAD",
                phoneNum = "82234567",
                DepartmentId = 4
            };

            Employee e5 = new Employee()
            {
                name = "Bob",
                password = "123",
                Department = dZOOL,
                email = "bob@gmail.com",
                role = "HEAD",
                phoneNum = "82334567",
                DepartmentId = 5
            };

            Employee e6 = new Employee()
            {
                name = "Mary1",
                password = "123",
                Department = dENGL,
                email = "mary1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "82354567",
                DepartmentId = 1
            };

            Employee e7 = new Employee()
            {
                name = "John1",
                password = "123",
                Department = dCPSC,
                email = "john1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "93234567",
                DepartmentId = 2
            };

            Employee e8 = new Employee()
            {
                name = "Joe1",
                password = "123",
                Department = dCOMM,
                email = "joe1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "91134567",
                DepartmentId = 3
            };

            Employee e9 = new Employee()
            {
                name = "Peter1",
                password = "123",
                Department = dREGR,
                email = "Peter1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "92224567",
                DepartmentId = 4
            };

            Employee e10 = new Employee()
            {
                name = "Bob1",
                password = "123",
                Department = dZOOL,
                email = "Bob1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "91114567",
                DepartmentId = 5
            };

            Employee e11 = new Employee()
            {
                name = "Mary2",
                password = "123",
                Department = dENGL,
                email = "Mary2@gmail.com",
                role = "DELEGATE",
                phoneNum = "92222567",
                DepartmentId = 1
            };

            Employee e12 = new Employee()
            {
                name = "Joe2",
                password = "123",
                Department = dCOMM,
                email = "Joe2@gmail.com",
                role = "DELEGATE",
                phoneNum = "91222567",
                DepartmentId = 3
            };

            Employee e13 = new Employee()
            {
                name = "Peter2",
                password = "123",
                Department = dREGR,
                email = "Peter2@gmail.com",
                role = "DELEGATE",
                phoneNum = "92222267",
                DepartmentId = 4
            };

            Employee e14 = new Employee()
            {
                name = "Bob2",
                password = "123",
                Department = dZOOL,
                email = "BOB2@gmail.com",
                role = "DELEGATE",
                phoneNum = "91122267",
                DepartmentId = 5
            };

            Employee e15 = new Employee()
            {
                name = "Mary3",
                password = "123",
                Department = dENGL,
                email = "Mary3@gmail.com",
                role = "STAFF",
                phoneNum = "92222267",
                DepartmentId = 1
            };

            Employee e16 = new Employee()
            {
                name = "Mary4",
                password = "123",
                Department = dENGL,
                email = "Mary4@gmail.com",
                role = "STAFF",
                phoneNum = "94222267",
                DepartmentId = 1
            };

            Employee e17 = new Employee()
            {
                name = "Mary5",
                password = "123",
                Department = dENGL,
                email = "Mary5@gmail.com",
                role = "STAFF",
                phoneNum = "95222267",
                DepartmentId = 1
            };

            Employee e18 = new Employee()
            {
                name = "John2",
                password = "123",
                Department = dCPSC,
                email = "John2@gmail.com",
                role = "STAFF",
                phoneNum = "82222267",
                DepartmentId = 2
            };

            Employee e19 = new Employee()
            {
                name = "John3",
                password = "123",
                Department = dCPSC,
                email = "John3@gmail.com",
                role = "STAFF",
                phoneNum = "83222267",
                DepartmentId = 2
            };

            Employee e20 = new Employee()
            {
                name = "John4",
                password = "123",
                Department = dCPSC,
                email = "John4@gmail.com",
                role = "STAFF",
                phoneNum = "84222267",
                DepartmentId = 2
            };

            Employee e21 = new Employee()
            {
                name = "John5",
                password = "123",
                Department = dCPSC,
                email = "John5@gmail.com",
                role = "STAFF",
                phoneNum = "85222267",
                DepartmentId = 2
            };

            Employee e22 = new Employee()
            {
                name = "Joe3",
                password = "123",
                Department = dCOMM,
                email = "Joe3@gmail.com",
                role = "STAFF",
                phoneNum = "83322267",
                DepartmentId = 3
            };

            Employee e23 = new Employee()
            {
                name = "Joe4",
                password = "123",
                Department = dCOMM,
                email = "Joe4@gmail.com",
                role = "STAFF",
                phoneNum = "84422267",
                DepartmentId = 3
            };

            Employee e24 = new Employee()
            {
                name = "Joe5",
                password = "123",
                Department = dCOMM,
                email = "Joe5@gmail.com",
                role = "STAFF",
                phoneNum = "85522267",
                DepartmentId = 3
            };

            Employee e25 = new Employee()
            {
                name = "Peter3",
                password = "123",
                Department = dREGR,
                email = "Peter3@gmail.com",
                role = "STAFF",
                phoneNum = "85522263",
                DepartmentId = 4
            };

            Employee e26 = new Employee()
            {
                name = "Peter4",
                password = "123",
                Department = dREGR,
                email = "Peter4@gmail.com",
                role = "STAFF",
                phoneNum = "85522264",
                DepartmentId = 4
            };

            Employee e27 = new Employee()
            {
                name = "Peter5",
                password = "123",
                Department = dREGR,
                email = "Peter5@gmail.com",
                role = "STAFF",
                phoneNum = "85522265",
                DepartmentId = 4
            };

            Employee e28 = new Employee()
            {
                name = "Bob3",
                password = "123",
                Department = dREGR,
                email = "bob3@gmail.com",
                role = "STAFF",
                phoneNum = "83522265",
                DepartmentId = 5
            };

            Employee e29 = new Employee()
            {
                name = "Bob4",
                password = "123",
                Department = dREGR,
                email = "bob4@gmail.com",
                role = "STAFF",
                phoneNum = "84522265",
                DepartmentId = 5
            };

            Employee e30 = new Employee()
            {
                name = "Bob5",
                password = "123",
                Department = dREGR,
                email = "bob5@gmail.com",
                role = "STAFF",
                phoneNum = "85522265",
                DepartmentId = 5
            };

            Employee e31 = new Employee()
            {
                name = "Martin",
                password = "123",
                Department = store,
                email = "martin@gmail.com",
                role = "STRMGR",
                phoneNum = "85522261",
                //DepartmentId = 
            };

            Employee e32 = new Employee()
            {
                name = "Tin",
                password = "123",
                Department = store,
                email = "tin@gmail.com",
                role = "STRSUPV",
                phoneNum = "85522262",
                //DepartmentId = 
            };

            Employee e33 = new Employee()
            {
                name = "Esther",
                password = "123",
                Department = store,
                email = "esther@gmail.com",
                role = "CLERK",
                phoneNum = "85522263",
                //DepartmentId = 
            };

            Employee e34 = new Employee()
            {
                name = "Esther1",
                password = "123",
                Department = store,
                email = "esther1@gmail.com",
                role = "CLERK",
                phoneNum = "85522264",
                //DepartmentId = 
            };

            Employee e35 = new Employee()
            {
                name = "Esther2",
                password = "123",
                Department = store,
                email = "esther2@gmail.com",
                role = "CLERK",
                phoneNum = "85522265",
                //DepartmentId = 
            };

            Employee[] empArr = { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19, e20, e21, e22, e23, e24, e25, e26, e27, e28, e29, e30, e31, e32, e33, e34, e35 };
            for (int c = 0; c < empArr.Length; c++)
            {
                unitOfWork.GetRepository<Employee>().Insert(empArr[c]);
            }
            #endregion

            //May
            #region requisition +detail lists
            Requisition r1 = new Requisition()
            {
                EmployeeId = e6.id,
                dateOfRequest = new DateTime(2020, 8, 17, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 8, 50, 00),
                AuthorizerId = e1.id,
                status = "Approved",
                comment = null
            };
            RequisitionDetail r1rd1 = new RequisitionDetail() { RequisitionId = r1.Id, StationeryId = i1.Id, reqQty = 10, rcvQty = 0, status = "Approved" };
            RequisitionDetail r1rd2 = new RequisitionDetail() { RequisitionId = r1.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 0, status = "Approved" };
            RequisitionDetail r1rd3 = new RequisitionDetail() { RequisitionId = r1.Id, StationeryId = i3.Id, reqQty = 10, rcvQty = 0, status = "Approved" };

            Requisition r2 = new Requisition()
            {
                EmployeeId = e15.id,
                dateOfRequest = new DateTime(2020, 8, 18, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 18, 8, 50, 00),
                AuthorizerId = e1.id,
                status = "Approved",
                comment = null
            };
            RequisitionDetail r2rd1 = new RequisitionDetail() { RequisitionId = r2.Id, StationeryId = i2.Id, reqQty = 3, rcvQty = 0, status = "Approved" };
            RequisitionDetail r2rd2 = new RequisitionDetail() { RequisitionId = r2.Id, StationeryId = i3.Id, reqQty = 15, rcvQty = 0, status = "Approved" };

            Requisition r3 = new Requisition()
            {
                EmployeeId = e16.id,
                dateOfRequest = new DateTime(2020, 8, 17, 8, 00, 00),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 8, 30, 00),
                AuthorizerId = e1.id,
                status = "Partially_Delivered",
                comment = null
            };
            RequisitionDetail r3rd1 = new RequisitionDetail() { RequisitionId = r3.Id, StationeryId = i1.Id, reqQty = 8, rcvQty = 5, status = "Partially_Delivered" };
            RequisitionDetail r3rd2 = new RequisitionDetail() { RequisitionId = r3.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r4 = new Requisition()
            {
                EmployeeId = e18.id,
                dateOfRequest = new DateTime(2020, 8, 18, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 18, 8, 50, 00),
                AuthorizerId = e2.id,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r4rd1 = new RequisitionDetail() { RequisitionId = r4.Id, StationeryId = i1.Id, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r4rd2 = new RequisitionDetail() { RequisitionId = r4.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r5 = new Requisition()
            {
                EmployeeId = e20.id,
                dateOfRequest = new DateTime(2020, 8, 18, 9, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 18, 9, 50, 00),
                AuthorizerId = e2.id,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r5rd1 = new RequisitionDetail() { RequisitionId = r5.Id, StationeryId = i3.Id, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r5rd2 = new RequisitionDetail() { RequisitionId = r5.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r6 = new Requisition()
            {
                EmployeeId = e22.id,
                dateOfRequest = new DateTime(2020, 8, 18, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 18, 8, 50, 00),
                AuthorizerId = e3.id,
                status = "Declined",
                comment = "Too many quantities"
            };
            RequisitionDetail r6rd1 = new RequisitionDetail() { RequisitionId = r6.Id, StationeryId = i1.Id, reqQty = 50, rcvQty = 0, status = "Declined" };

            Requisition r7 = new Requisition()
            {
                EmployeeId = e23.id,
                dateOfRequest = new DateTime(2020, 8, 17, 9, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 9, 45, 00),
                AuthorizerId = e3.id,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r7rd1 = new RequisitionDetail() { RequisitionId = r7.Id, StationeryId = i1.Id, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r7rd2 = new RequisitionDetail() { RequisitionId = r7.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r8 = new Requisition()
            {
                EmployeeId = e8.id,
                dateOfRequest = new DateTime(2020, 8, 18, 9, 30, 52),
                dateOfAuthorizing = null,
                AuthorizerId = e3.id,
                status = "Applied",
                comment = null
            };
            RequisitionDetail r8rd1 = new RequisitionDetail() { RequisitionId = r8.Id, StationeryId = i3.Id, reqQty = 8, rcvQty = 0, status = "Applied" };
            RequisitionDetail r8rd2 = new RequisitionDetail() { RequisitionId = r8.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 0, status = "Applied" };

            Requisition r9 = new Requisition()
            {
                EmployeeId = e24.id,
                dateOfRequest = new DateTime(2020, 8, 18, 9, 30, 00),
                dateOfAuthorizing = null,
                AuthorizerId = e4.id,
                status = "Applied",
                comment = null
            };
            RequisitionDetail r9rd1 = new RequisitionDetail() { RequisitionId = r9.Id, StationeryId = i1.Id, reqQty = 8, rcvQty = 0, status = "Applied" };
            RequisitionDetail r9rd2 = new RequisitionDetail() { RequisitionId = r9.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 0, status = "Applied" };

            Requisition r10 = new Requisition()
            {
                EmployeeId = e25.id,
                dateOfRequest = new DateTime(2020, 8, 17, 9, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 9, 45, 00),
                AuthorizerId = e4.id,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r10rd1 = new RequisitionDetail() { RequisitionId = r10.Id, StationeryId = i2.Id, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r10rd2 = new RequisitionDetail() { RequisitionId = r10.Id, StationeryId = i3.Id, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r11 = new Requisition()
            {
                EmployeeId = e26.id,
                dateOfRequest = new DateTime(2020, 8, 17, 8, 00, 00),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 8, 30, 00),
                AuthorizerId = e4.id,
                status = "Partially_Delivered",
                comment = null
            };
            RequisitionDetail r11rd1 = new RequisitionDetail() { RequisitionId = r11.Id, StationeryId = i1.Id, reqQty = 7, rcvQty = 5, status = "Partially_Delivered" };
            RequisitionDetail r11rd2 = new RequisitionDetail() { RequisitionId = r11.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r12 = new Requisition()
            {
                EmployeeId = e27.id,
                dateOfRequest = new DateTime(2020, 8, 18, 10, 30, 00),
                dateOfAuthorizing = null,
                AuthorizerId = e5.id,
                status = "Applied",
                comment = null
            };
            RequisitionDetail r12rd1 = new RequisitionDetail() { RequisitionId = r12.Id, StationeryId = i1.Id, reqQty = 8, rcvQty = 0, status = "Applied" };
            RequisitionDetail r12rd2 = new RequisitionDetail() { RequisitionId = r12.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 0, status = "Applied" };

            Requisition r13 = new Requisition()
            {
                EmployeeId = e28.id,
                dateOfRequest = new DateTime(2020, 8, 17, 9, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 9, 45, 00),
                AuthorizerId = e5.id,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r13rd1 = new RequisitionDetail() { RequisitionId = r13.Id, StationeryId = i2.Id, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r13rd2 = new RequisitionDetail() { RequisitionId = r13.Id, StationeryId = i3.Id, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r14 = new Requisition()
            {
                EmployeeId = e29.id,
                dateOfRequest = new DateTime(2020, 8, 17, 8, 00, 00),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 8, 30, 00),
                AuthorizerId = e5.id,
                status = "Partially_Delivered",
                comment = null
            };
            RequisitionDetail r14rd1 = new RequisitionDetail() { RequisitionId = r14.Id, StationeryId = i1.Id, reqQty = 7, rcvQty = 5, status = "Partially_Delivered" };
            RequisitionDetail r14rd2 = new RequisitionDetail() { RequisitionId = r14.Id, StationeryId = i2.Id, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition[] reqArr = { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14 };
            for (int j = 0; j < reqArr.Length; j++)
            {
                unitOfWork.GetRepository<Requisition>().Insert(reqArr[j]);
            }

            RequisitionDetail[] reqDetailArr = { r1rd1, r1rd2, r1rd3, r2rd1, r2rd2, r3rd1, r3rd2, r4rd2, r4rd2, r5rd1, r5rd2, r6rd1, r7rd1, r7rd2, r8rd1, r8rd2, r9rd1, r9rd2, r10rd1, r10rd2, r11rd1, r11rd2, r12rd1, r12rd2, r13rd1, r13rd2, r14rd1, r14rd2 };
            for (int a = 0; a < reqDetailArr.Length; a++)
            {
                unitOfWork.GetRepository<RequisitionDetail>().Insert(reqDetailArr[a]);
            }
            unitOfWork.SaveChanges();
            #endregion


            //Done yirui
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

            Supplier s4 = new Supplier()
            {
                supplierCode = "OMEG",
                name = "OMEGA Stationary Supplier",
                contactPerson = " Mr Ronnie Ho",
                phoneNum = "7671233",
                gstRegisNo = "MR-8555330-1",
                fax = "7671234",
                address = "Blk 11, Hillview Avenue,#03-04, Singapore 679036",
                priority = "4th",
            };
            Supplier[] supArr = { s1, s2, s3, s4 };
            for (int b = 0; b < supArr.Length; b++)
            {
                unitOfWork.GetRepository<Supplier>().Insert(supArr[b]);
            }
            unitOfWork.SaveChanges();
            #endregion

            
            //Done May only 2 
            #region StockAdjustment and Detail
            /// 1 receiveGood (after PO)
            /// 2 monthly check + 3 get good from ware house find missing or damage
            /// 4 manager revert adjustment after not issue voucher
            StockAdjustment sa1 = new StockAdjustment() { type = "regular inventory check", date = new DateTime(2020, 7, 27), EmployeeId = e33.id};
            StockAdjustmentDetail sa1sad1 = new StockAdjustmentDetail() { StockAdjustmentId = sa1.id, StationeryId = i1.Id, discpQty = -5, comment = "Broken"};
            StockAdjustmentDetail sa1sad2 = new StockAdjustmentDetail() { StockAdjustmentId = sa1.id, StationeryId = i2.Id, discpQty = 1, comment = null };
            StockAdjustmentDetail sa1sad3 = new StockAdjustmentDetail() { StockAdjustmentId = sa1.id, StationeryId = i3.Id, discpQty = -2, comment = "Missing" };

            StockAdjustment sa2 = new StockAdjustment() { type = "missing when collecting goods", date = new DateTime(2020, 8, 27), EmployeeId = e34.id };
            StockAdjustmentDetail sa2sad1 = new StockAdjustmentDetail() { StockAdjustmentId = sa2.id, StationeryId = i1.Id, discpQty = -10, comment = null };
            StockAdjustmentDetail sa2sad2 = new StockAdjustmentDetail() { StockAdjustmentId = sa2.id, StationeryId = i2.Id, discpQty = -50, comment = null };
            StockAdjustmentDetail sa2sad3 = new StockAdjustmentDetail() { StockAdjustmentId = sa2.id, StationeryId = i3.Id, discpQty = -2, comment = "Broken" };

            StockAdjustment[] saArr = { sa1, sa2 };
            for (int e = 0; e < saArr.Length; e++)
            {
                unitOfWork.GetRepository<StockAdjustment>().Insert(saArr[e]);
            }
            StockAdjustmentDetail[] sadArr = { sa1sad1, sa1sad2, sa1sad3, sa2sad1, sa2sad2, sa2sad3 };
            for (int f = 0; f < sadArr.Length; f++)
            {
                unitOfWork.GetRepository<StockAdjustmentDetail>().Insert(sadArr[f]);
            }

            #endregion

            //So far empty
            #region AdjustmentVoucher and Detail
            AdjustmentVoucher av1 = new AdjustmentVoucher() { };
            AdjustmentVoucherDetail av1avd1 = new AdjustmentVoucherDetail() { AdjustmentVoucherId = av1.id };

            AdjustmentVoucher av2 = new AdjustmentVoucher() { };
            AdjustmentVoucherDetail av2avd1 = new AdjustmentVoucherDetail() { AdjustmentVoucherId = av2.id };

            AdjustmentVoucher[] avArr = { av1, av2 };
            for (int g = 0; g < avArr.Length; g++)
            {
                unitOfWork.GetRepository<AdjustmentVoucher>().Insert(avArr[g]);
            }
            AdjustmentVoucherDetail[] avdArr = { av1avd1, av2avd1 };
            for (int h = 0; h < avArr.Length; h++)
            {
                unitOfWork.GetRepository<AdjustmentVoucherDetail>().Insert(avdArr[h]);
            }
            //I got blurred and don't know how to seed this part. Sry. -May
            #endregion

            #region Disbursement and Detail
            DisbursementList dl1 = new DisbursementList() { DepartmentId = dENGL.Id, date = new DateTime(2020, 8, 27) };
            DisbursementList dl2 = new DisbursementList() { DepartmentId = dCPSC.Id, date = new DateTime(2020, 8, 27) };
            //I got blurred and don't know how to seed this parts. Sry. -May
            #endregion

            // sry Pending seed 
            #region Purchase order and Detail

            #endregion

        }

        return isCreateDb;
    }


}