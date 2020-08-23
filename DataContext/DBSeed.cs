using System;
using BackEndAD.DataContext;
using BackEndAD.Models;
using BackEndAD.Repo;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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
                Collection = ci1

            };


            Department dCPSC = new Department()
            {
                deptName = "Computer Science",
                deptCode = "CPSC",
                //headId = 2,
                //repId = 7,
                //delegaterId = null,
                delgtStartDate = new DateTime(2020, 6, 13),
                delgtEndDate = new DateTime(2020, 7, 10),
                Collection = ci2
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
                Collection = ci3
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
                Collection = ci4
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
                Collection = ci5
            };
            Department[] DeptArr = { store, dENGL, dCPSC, dCOMM, dREGR, dZOOL };
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
            unitOfWork.SaveChanges();
            #endregion

            //Bianca Transfer into C# code - pending testing & seed
            #region stationery pending testing & seed
            Stationery i1 = new Stationery() { category = "Clip", desc = "Clips Double 1\"", reOrderLevel = 50, reOrderQty = 30, inventoryQty = 59, unit = "Dozen" };
            Stationery i2 = new Stationery() { category = "Clip", desc = "Clips Double 2\"", reOrderLevel = 50, reOrderQty = 30, inventoryQty = 212, unit = "Dozen" };
            Stationery i3 = new Stationery() { category = "Clip", desc = "Clips Double 3/4\"", reOrderLevel = 50, reOrderQty = 30, inventoryQty = 169, unit = "Dozen" };
            Stationery i4 = new Stationery() { category = "Clip", desc = "Clips Paper Large", reOrderLevel = 50, reOrderQty = 30, inventoryQty = 259, unit = "Box" };
            Stationery i5 = new Stationery() { category = "Clip", desc = "Clips Paper Medium", reOrderLevel = 50, reOrderQty = 30, inventoryQty = 218, unit = "Box" };
            Stationery i6 = new Stationery() { category = "Clip", desc = "Clips Paper Small", reOrderLevel = 50, reOrderQty = 30, inventoryQty = 226, unit = "Box" };
            Stationery i7 = new Stationery() { category = "Envelope", desc = "Envelope Brown (3\"x6\")", reOrderLevel = 600, reOrderQty = 400, inventoryQty = 174, unit = "Each" };
            Stationery i8 = new Stationery() { category = "Envelope", desc = "Envelope Brown (3\"x6\") w/ Window", reOrderLevel = 600, reOrderQty = 400, inventoryQty = 290, unit = "Each" };
            Stationery i9 = new Stationery() { category = "Envelope", desc = "Envelope Brown (5\"x7\")", reOrderLevel = 600, reOrderQty = 400, inventoryQty = 418, unit = "Each" };
            Stationery i10 = new Stationery() { category = "Envelope", desc = "Envelope Brown (5\"x7\") w/ Window", reOrderLevel = 600, reOrderQty = 400, inventoryQty = 332, unit = "Each" };
            Stationery i11 = new Stationery() { category = "Envelope", desc = "Envelope White (3\"x6\")", reOrderLevel = 600, reOrderQty = 400, inventoryQty = 326, unit = "Each" };
            Stationery i12 = new Stationery() { category = "Envelope", desc = "Envelope White (3\"x6\") w/ Window", reOrderLevel = 600, reOrderQty = 400, inventoryQty = 153, unit = "Each" };
            Stationery i13 = new Stationery() { category = "Envelope", desc = "Envelope White (5\"x7\")", reOrderLevel = 600, reOrderQty = 400, inventoryQty = 74, unit = "Each" };
            Stationery i14 = new Stationery() { category = "Envelope", desc = "Envelope White (5\"x7\") w/ Window", reOrderLevel = 600, reOrderQty = 400, inventoryQty = 285, unit = "Each" };
            Stationery i15 = new Stationery() { category = "Eraser", desc = "Eraser (hard)", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 65, unit = "Each" };
            Stationery i16 = new Stationery() { category = "Eraser", desc = "Eraser (soft)", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 115, unit = "Each" };
            Stationery i17 = new Stationery() { category = "Exercise", desc = "Exercise Book (100 pg)", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 45, unit = "Each" };
            Stationery i18 = new Stationery() { category = "Exercise", desc = "Exercise Book (120 pg)", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 48, unit = "Each" };
            Stationery i19 = new Stationery() { category = "Exercise", desc = "Exercise Book A4 Hardcover (100 pg)", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 324, unit = "Each" };
            Stationery i20 = new Stationery() { category = "Exercise", desc = "Exercise Book A4 Hardcover (120 pg)", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 320, unit = "Each" };
            Stationery i21 = new Stationery() { category = "Exercise", desc = "Exercise Book A4 Hardcover (200 pg)", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 338, unit = "Each" };
            Stationery i22 = new Stationery() { category = "Exercise", desc = "Exercise Book Hardcover (100 pg)", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 463, unit = "Each" };
            Stationery i23 = new Stationery() { category = "Exercise", desc = "Exercise Book Hardcover (120 pg)", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 37, unit = "Each" };
            Stationery i24 = new Stationery() { category = "File", desc = "File Separator", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 270, unit = "Set" };
            Stationery i25 = new Stationery() { category = "File", desc = "File-Blue Plain", reOrderLevel = 200, reOrderQty = 100, inventoryQty = 49, unit = "Each" };
            Stationery i26 = new Stationery() { category = "File", desc = "File-Blue with Logo", reOrderLevel = 200, reOrderQty = 100, inventoryQty = 442, unit = "Each" };
            Stationery i27 = new Stationery() { category = "File", desc = "File-Brown w/o Logo", reOrderLevel = 200, reOrderQty = 150, inventoryQty = 91, unit = "Each" };
            Stationery i28 = new Stationery() { category = "File", desc = "File-Brown with Logo", reOrderLevel = 200, reOrderQty = 150, inventoryQty = 440, unit = "Each" };
            Stationery i29 = new Stationery() { category = "File", desc = "Folder Plastic Blue", reOrderLevel = 200, reOrderQty = 150, inventoryQty = 398, unit = "Each" };
            Stationery i30 = new Stationery() { category = "File", desc = "Folder Plastic Clear", reOrderLevel = 200, reOrderQty = 150, inventoryQty = 224, unit = "Each" };
            Stationery i31 = new Stationery() { category = "File", desc = "Folder Plastic Green", reOrderLevel = 200, reOrderQty = 150, inventoryQty = 253, unit = "Each" };
            Stationery i32 = new Stationery() { category = "File", desc = "Folder Plastic Pink", reOrderLevel = 200, reOrderQty = 150, inventoryQty = 21, unit = "Each" };
            Stationery i33 = new Stationery() { category = "File", desc = "Folder Plastic Yellow", reOrderLevel = 200, reOrderQty = 150, inventoryQty = 275, unit = "Each" };
            Stationery i34 = new Stationery() { category = "Pen", desc = "Highlighter Blue", reOrderLevel = 100, reOrderQty = 80, inventoryQty = 197, unit = "Box" };
            Stationery i35 = new Stationery() { category = "Pen", desc = "Highlighter Green", reOrderLevel = 100, reOrderQty = 80, inventoryQty = 270, unit = "Box" };
            Stationery i36 = new Stationery() { category = "Pen", desc = "Highlighter Pink", reOrderLevel = 100, reOrderQty = 80, inventoryQty = 176, unit = "Box" };
            Stationery i37 = new Stationery() { category = "Pen", desc = "Highlighter Yellow", reOrderLevel = 100, reOrderQty = 80, inventoryQty = 371, unit = "Box" };
            Stationery i38 = new Stationery() { category = "Puncher", desc = "Hole Puncher 2 holes", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 422, unit = "Each" };
            Stationery i39 = new Stationery() { category = "Puncher", desc = "Hole Puncher 3 holes", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 210, unit = "Each" };
            Stationery i40 = new Stationery() { category = "Puncher", desc = "Hole Puncher Adjustable", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 488, unit = "Each" };
            Stationery i41 = new Stationery() { category = "Pad", desc = "Pad Postit Memo 1\"x2\"", reOrderLevel = 100, reOrderQty = 60, inventoryQty = 120, unit = "Packet" };
            Stationery i42 = new Stationery() { category = "Pad", desc = "Pad Postit Memo 1/2\"x1\"", reOrderLevel = 100, reOrderQty = 60, inventoryQty = 292, unit = "Packet" };
            Stationery i43 = new Stationery() { category = "Pad", desc = "Pad Postit Memo 1/2\"x2\"", reOrderLevel = 100, reOrderQty = 60, inventoryQty = 155, unit = "Packet" };
            Stationery i44 = new Stationery() { category = "Pad", desc = "Pad Postit Memo 2\"x3\"", reOrderLevel = 100, reOrderQty = 60, inventoryQty = 311, unit = "Packet" };
            Stationery i45 = new Stationery() { category = "Pad", desc = "Pad Postit Memo 2\"x4\"", reOrderLevel = 100, reOrderQty = 60, inventoryQty = 312, unit = "Packet" };
            Stationery i46 = new Stationery() { category = "Pad", desc = "Pad Postit Memo 2\"x4\"", reOrderLevel = 100, reOrderQty = 60, inventoryQty = 294, unit = "Packet" };
            Stationery i47 = new Stationery() { category = "Pad", desc = "Pad Postit Memo 3/4\"x2\"", reOrderLevel = 100, reOrderQty = 60, inventoryQty = 99, unit = "Packet" };
            Stationery i48 = new Stationery() { category = "Paper", desc = "Paper Photostat A3", reOrderLevel = 500, reOrderQty = 500, inventoryQty = 311, unit = "Box" };
            Stationery i49 = new Stationery() { category = "Paper", desc = "Paper Photostat A4", reOrderLevel = 500, reOrderQty = 500, inventoryQty = 394, unit = "Box" };
            Stationery i50 = new Stationery() { category = "Pen", desc = "Pen Ballpoint Black", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 361, unit = "Dozen" };
            Stationery i51 = new Stationery() { category = "Pen", desc = "Pen Ballpoint Blue", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 181, unit = "Dozen" };
            Stationery i52 = new Stationery() { category = "Pen", desc = "Pen Ballpoint Red", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 266, unit = "Dozen" };
            Stationery i53 = new Stationery() { category = "Pen", desc = "Pen Felt Tip Black", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 208, unit = "Dozen" };
            Stationery i54 = new Stationery() { category = "Pen", desc = "Pen Felt Tip Blue", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 480, unit = "Dozen" };
            Stationery i55 = new Stationery() { category = "Pen", desc = "Pen Felt Tip Red", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 386, unit = "Dozen" };
            Stationery i56 = new Stationery() { category = "Pen", desc = "Pen Transparency Permanent", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 90, unit = "Packet" };
            Stationery i57 = new Stationery() { category = "Pen", desc = "Pen Transparency Soluble", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 60, unit = "Packet" };
            Stationery i58 = new Stationery() { category = "Pen", desc = "Pen Whiteboard Marker Black", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 123, unit = "Box" };
            Stationery i59 = new Stationery() { category = "Pen", desc = "Pen Whiteboard Marker Blue", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 333, unit = "Box" };
            Stationery i60 = new Stationery() { category = "Pen", desc = "Pen Whiteboard Marker Green", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 375, unit = "Box" };
            Stationery i61 = new Stationery() { category = "Pen", desc = "Pen Whiteboard Marker Red", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 410, unit = "Box" };
            Stationery i62 = new Stationery() { category = "Pen", desc = "Pencil 2B", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 292, unit = "Dozen" };
            Stationery i63 = new Stationery() { category = "Pen", desc = "Pencil 2B with Eraser End", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 276, unit = "Dozen" };
            Stationery i64 = new Stationery() { category = "Pen", desc = "Pencil 4H", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 352, unit = "Dozen" };
            Stationery i65 = new Stationery() { category = "Pen", desc = "Pencil B", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 173, unit = "Dozen" };
            Stationery i66 = new Stationery() { category = "Pen", desc = "Pencil B with Eraser End", reOrderLevel = 100, reOrderQty = 50, inventoryQty = 360, unit = "Dozen" };
            Stationery i67 = new Stationery() { category = "Ruler", desc = "Ruler 12\"", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 383, unit = "Dozen" };
            Stationery i68 = new Stationery() { category = "Ruler", desc = "Ruler 6\"", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 389, unit = "Each" };
            Stationery i69 = new Stationery() { category = "Scissors", desc = "Scissors", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 69, unit = "Each" };
            Stationery i70 = new Stationery() { category = "Tape", desc = "Scotch Tape", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 419, unit = "Each" };
            Stationery i71 = new Stationery() { category = "Tape", desc = "Scotch Tape Dispenser", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 400, unit = "Each" };
            Stationery i72 = new Stationery() { category = "Sharpener", desc = "Sharpener", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 190, unit = "Each" };
            Stationery i73 = new Stationery() { category = "Shorthand", desc = "Shorthand Book (100 pg)", reOrderLevel = 100, reOrderQty = 80, inventoryQty = 325, unit = "Each" };
            Stationery i74 = new Stationery() { category = "Shorthand", desc = "Shorthand Book (120 pg)", reOrderLevel = 100, reOrderQty = 80, inventoryQty = 464, unit = "Each" };
            Stationery i75 = new Stationery() { category = "Shorthand", desc = "Shorthand Book (80 pg)", reOrderLevel = 100, reOrderQty = 80, inventoryQty = 122, unit = "Each" };
            Stationery i76 = new Stationery() { category = "Stapler", desc = "Stapler No. 28 (Each)", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 95, unit = "Each" };
            Stationery i77 = new Stationery() { category = "Stapler", desc = "Stapler No. 36 (Each)", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 394, unit = "Each" };
            Stationery i78 = new Stationery() { category = "Stapler", desc = "Stapler No. 28 (Box)", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 254, unit = "Box" };
            Stationery i79 = new Stationery() { category = "Stapler", desc = "Stapler No. 36 (Box)", reOrderLevel = 50, reOrderQty = 20, inventoryQty = 364, unit = "Box" };
            Stationery i80 = new Stationery() { category = "Tacks", desc = "Thumb Tacks Large", reOrderLevel = 10, reOrderQty = 10, inventoryQty = 157, unit = "Box" };
            Stationery i81 = new Stationery() { category = "Tacks", desc = "Thumb Tacks Medium", reOrderLevel = 10, reOrderQty = 10, inventoryQty = 308, unit = "Box" };
            Stationery i82 = new Stationery() { category = "Tacks", desc = "Thumb Tacks Small", reOrderLevel = 10, reOrderQty = 10, inventoryQty = 416, unit = "Box" };
            Stationery i83 = new Stationery() { category = "Tparency", desc = "Transparency Blue", reOrderLevel = 100, reOrderQty = 200, inventoryQty = 351, unit = "Box" };
            Stationery i84 = new Stationery() { category = "Tparency", desc = "Transparency Clear", reOrderLevel = 500, reOrderQty = 400, inventoryQty = 9, unit = "Box" };
            Stationery i85 = new Stationery() { category = "Tparency", desc = "Transparency Green", reOrderLevel = 100, reOrderQty = 200, inventoryQty = 249, unit = "Box" };
            Stationery i86 = new Stationery() { category = "Tparency", desc = "Transparency Red", reOrderLevel = 100, reOrderQty = 200, inventoryQty = 487, unit = "Box" };
            Stationery i87 = new Stationery() { category = "Tparency", desc = "Transparency Reverse Blue", reOrderLevel = 100, reOrderQty = 200, inventoryQty = 170, unit = "Box" };
            Stationery i88 = new Stationery() { category = "Tparency", desc = "Transparency Cover 3M", reOrderLevel = 500, reOrderQty = 400, inventoryQty = 362, unit = "Box" };
            Stationery i89 = new Stationery() { category = "Tray", desc = "Trays In/Out", reOrderLevel = 20, reOrderQty = 10, inventoryQty = 15, unit = "Set" };
            Stationery[] stationeryArr = { i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, i13, i14, i15, i16, i17, i18, i19, i20, i21, i22, i23, i24, i25, i26, i27, i28, i29, i30,
        i31, i32, i33, i34, i35, i36, i37, i38, i39, i40, i41, i42, i43, i44, i45, i46, i47, i48, i49, i50, i51, i52, i53, i54, i55, i56, i57, i58, i59,
        i60, i61, i62, i63, i64, i65, i66, i67, i68, i69, i70, i71, i72, i73, i74, i75, i76, i77, i78, i79, i80, i81, i82, i83, i84, i85, i86, i87, i88, i89};
            for (int d = 0; d < stationeryArr.Length; d++)
            {
                unitOfWork.GetRepository<Stationery>().Insert(stationeryArr[d]);
                unitOfWork.SaveChanges();
            }
            
            #endregion

            //done Yirui
            #region Employee
            Employee e1 = new Employee()
            {
                name = "Mary",
                password = "123",
                department = dENGL,
                email = "mary@gmail.com",
                role = "HEAD",
                phoneNum = "91234567"
                
            };

            Employee e2 = new Employee()
            {
                name = "John",
                password = "123",
                department = dCPSC,
                email = "john@gmail.com",
                role = "HEAD",
                phoneNum = "92234567"
               
            };

            Employee e3 = new Employee()
            {
                name = "Joe",
                password = "123",
                department = dCOMM,
                email = "joe@gmail.com",
                role = "HEAD",
                phoneNum = "81234567"
                
            };

            Employee e4 = new Employee()
            {
                name = "Peter",
                password = "123",
                department = dREGR,
                email = "peter@gmail.com",
                role = "HEAD",
                phoneNum = "82234567"
                
            };

            Employee e5 = new Employee()
            {
                name = "Bob",
                password = "123",
                department = dZOOL,
                email = "bob@gmail.com",
                role = "HEAD",
                phoneNum = "82334567"
                
            };

            Employee e6 = new Employee()
            {
                name = "Mary1",
                password = "123",
                department = dENGL,
                email = "mary1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "82354567"
            };

            Employee e7 = new Employee()
            {
                name = "John1",
                password = "123",
                department = dCPSC,
                email = "john1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "93234567"
                
            };

            Employee e8 = new Employee()
            {
                name = "Joe1",
                password = "123",
                department = dCOMM,
                email = "joe1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "91134567"
                
            };

            Employee e9 = new Employee()
            {
                name = "Peter1",
                password = "123",
                department = dREGR,
                email = "Peter1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "92224567"
                
            };

            Employee e10 = new Employee()
            {
                name = "Bob1",
                password = "123",
                department = dZOOL,
                email = "Bob1@gmail.com",
                role = "REPRESENTATIVE",
                phoneNum = "91114567"
            };

            Employee e11 = new Employee()
            {
                name = "Mary2",
                password = "123",
                department = dENGL,
                email = "Mary2@gmail.com",
                role = "DELEGATE",
                phoneNum = "92222567"
            };

            Employee e12 = new Employee()
            {
                name = "Joe2",
                password = "123",
                department = dCOMM,
                email = "Joe2@gmail.com",
                role = "DELEGATE",
                phoneNum = "91222567",
                
            };

            Employee e13 = new Employee()
            {
                name = "Peter2",
                password = "123",
                department = dREGR,
                email = "Peter2@gmail.com",
                role = "DELEGATE",
                phoneNum = "92222267",
                
            };

            Employee e14 = new Employee()
            {
                name = "Bob2",
                password = "123",
                department = dZOOL,
                email = "BOB2@gmail.com",
                role = "DELEGATE",
                phoneNum = "91122267"
            };

            Employee e15 = new Employee()
            {
                name = "Mary3",
                password = "123",
                department = dENGL,
                email = "Mary3@gmail.com",
                role = "STAFF",
                phoneNum = "92222267"
            };

            Employee e16 = new Employee()
            {
                name = "Mary4",
                password = "123",
                department = dENGL,
                email = "Mary4@gmail.com",
                role = "STAFF",
                phoneNum = "94222267"
            };

            Employee e17 = new Employee()
            {
                name = "Mary5",
                password = "123",
                department = dENGL,
                email = "Mary5@gmail.com",
                role = "STAFF",
                phoneNum = "95222267"
            };

            Employee e18 = new Employee()
            {
                name = "John2",
                password = "123",
                department = dCPSC,
                email = "John2@gmail.com",
                role = "STAFF",
                phoneNum = "82222267"
                
            };

            Employee e19 = new Employee()
            {
                name = "John3",
                password = "123",
                department = dCPSC,
                email = "John3@gmail.com",
                role = "STAFF",
                phoneNum = "83222267"
                
            };

            Employee e20 = new Employee()
            {
                name = "John4",
                password = "123",
                department = dCPSC,
                email = "John4@gmail.com",
                role = "STAFF",
                phoneNum = "84222267"
                
            };

            Employee e21 = new Employee()
            {
                name = "John5",
                password = "123",
                department = dCPSC,
                email = "John5@gmail.com",
                role = "STAFF",
                phoneNum = "85222267"
                
            };

            Employee e22 = new Employee()
            {
                name = "Joe3",
                password = "123",
                department = dCOMM,
                email = "Joe3@gmail.com",
                role = "STAFF",
                phoneNum = "83322267"
                
            };

            Employee e23 = new Employee()
            {
                name = "Joe4",
                password = "123",
                department = dCOMM,
                email = "Joe4@gmail.com",
                role = "STAFF",
                phoneNum = "84422267"
                
            };

            Employee e24 = new Employee()
            {
                name = "Joe5",
                password = "123",
                department = dCOMM,
                email = "Joe5@gmail.com",
                role = "STAFF",
                phoneNum = "85522267"
                
            };

            Employee e25 = new Employee()
            {
                name = "Peter3",
                password = "123",
                department = dREGR,
                email = "Peter3@gmail.com",
                role = "STAFF",
                phoneNum = "85522263"
                
            };

            Employee e26 = new Employee()
            {
                name = "Peter4",
                password = "123",
                department = dREGR,
                email = "Peter4@gmail.com",
                role = "STAFF",
                phoneNum = "85522264"
                
            };

            Employee e27 = new Employee()
            {
                name = "Peter5",
                password = "123",
                department = dREGR,
                email = "Peter5@gmail.com",
                role = "STAFF",
                phoneNum = "85522265"
            };

            Employee e28 = new Employee()
            {
                name = "Bob3",
                password = "123",
                department = dREGR,
                email = "bob3@gmail.com",
                role = "STAFF",
                phoneNum = "83522265"
            };

            Employee e29 = new Employee()
            {
                name = "Bob4",
                password = "123",
                department = dREGR,
                email = "bob4@gmail.com",
                role = "STAFF",
                phoneNum = "84522265"
            };

            Employee e30 = new Employee()
            {
                name = "Bob5",
                password = "123",
                department = dREGR,
                email = "bob5@gmail.com",
                role = "STAFF",
                phoneNum = "85522265"
            };

            Employee e31 = new Employee()
            {
                name = "Martin",
                password = "123",
                department = store,
                email = "martin@gmail.com",
                role = "STRMGR",
                phoneNum = "85522261",           
            };

            Employee e32 = new Employee()
            {
                name = "Tin",
                password = "123",
                department = store,
                email = "tin@gmail.com",
                role = "STRSUPV",
                phoneNum = "85522262"
            };

            Employee e33 = new Employee()
            {
                name = "Esther",
                password = "123",
                department = store,
                email = "esther@gmail.com",
                role = "CLERK",
                phoneNum = "85522263"
            };

            Employee e34 = new Employee()
            {
                name = "Esther1",
                password = "123",
                department = store,
                email = "esther1@gmail.com",
                role = "CLERK",
                phoneNum = "85522264"
            };

            Employee e35 = new Employee()
            {
                name = "Esther2",
                password = "123",
                department = store,
                email = "esther2@gmail.com",
                role = "CLERK",
                phoneNum = "85522265"
            };

            Employee[] empArr = { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19, e20, e21, e22, e23, e24, e25, e26, e27, e28, e29, e30, e31, e32, e33, e34, e35 };
            for (int c = 0; c < empArr.Length; c++)
            {
                unitOfWork.GetRepository<Employee>().Insert(empArr[c]);
            }
            unitOfWork.SaveChanges();
            #endregion

            //Part 2 this is still need 
            ci1.clerk = e33;
            ci2.clerk = e33;
            ci3.clerk = e34;
            ci4.clerk = e34;
            ci5.clerk = e35;
            ci6.clerk = e35;
            for (int i = 0; i < collectionInfoArr.Length; i++)
            {
                unitOfWork.GetRepository<CollectionInfo>().Update(collectionInfoArr[i]);
            }
            unitOfWork.SaveChanges();

            //May
            #region requisition +detail lists
            Requisition r1 = new Requisition()
            {
                Employee = e6,
                dateOfRequest = new DateTime(2020, 8, 17, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 8, 50, 00),
                Authorizer = e1,
                status = "Approved",
                comment = null
            };
            RequisitionDetail r1rd1 = new RequisitionDetail() { Requisition = r1, Stationery = i1, reqQty = 10, rcvQty = 0, status = "Approved" };
            RequisitionDetail r1rd2 = new RequisitionDetail() { Requisition = r1, Stationery = i2, reqQty = 5, rcvQty = 0, status = "Approved" };
            RequisitionDetail r1rd3 = new RequisitionDetail() { Requisition = r1, Stationery = i3, reqQty = 10, rcvQty = 0, status = "Approved" };

            Requisition r2 = new Requisition()
            {
                Employee = e15,
                dateOfRequest = new DateTime(2020, 8, 18, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 18, 8, 50, 00),
                Authorizer = e1,
                status = "Approved",
                comment = null
            };
            RequisitionDetail r2rd1 = new RequisitionDetail() { Requisition = r2, Stationery = i2, reqQty = 3, rcvQty = 0, status = "Approved" };
            RequisitionDetail r2rd2 = new RequisitionDetail() { Requisition = r2, Stationery = i3, reqQty = 15, rcvQty = 0, status = "Approved" };

            Requisition r3 = new Requisition()
            {
                Employee = e16,
                dateOfRequest = new DateTime(2020, 8, 17, 8, 00, 00),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 8, 30, 00),
                Authorizer = e1,
                status = "Partially_Delivered",
                comment = null
            };
            RequisitionDetail r3rd1 = new RequisitionDetail() { Requisition = r3, Stationery = i1, reqQty = 8, rcvQty = 5, status = "Partially_Delivered" };
            RequisitionDetail r3rd2 = new RequisitionDetail() { Requisition = r3, Stationery = i2, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r4 = new Requisition()
            {
                Employee = e18,
                dateOfRequest = new DateTime(2020, 8, 18, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 18, 8, 50, 00),
                Authorizer = e2,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r4rd1 = new RequisitionDetail() { Requisition = r4, Stationery = i1, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r4rd2 = new RequisitionDetail() { Requisition = r4, Stationery = i2, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r5 = new Requisition()
            {
                Employee = e20,
                dateOfRequest = new DateTime(2020, 8, 18, 9, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 18, 9, 50, 00),
                Authorizer = e2,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r5rd1 = new RequisitionDetail() { Requisition = r5, Stationery = i3, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r5rd2 = new RequisitionDetail() { Requisition = r5, Stationery = i2, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r6 = new Requisition()
            {
                Employee = e22,
                dateOfRequest = new DateTime(2020, 8, 18, 8, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 18, 8, 50, 00),
                Authorizer = e3,
                status = "Declined",
                comment = "Too many quantities"
            };
            RequisitionDetail r6rd1 = new RequisitionDetail() { Requisition = r6, Stationery = i1, reqQty = 50, rcvQty = 0, status = "Declined" };

            Requisition r7 = new Requisition()
            {
                Employee = e23,
                dateOfRequest = new DateTime(2020, 8, 17, 9, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 9, 45, 00),
                Authorizer = e3,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r7rd1 = new RequisitionDetail() { Requisition = r7, Stationery = i1, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r7rd2 = new RequisitionDetail() { Requisition = r7, Stationery = i2, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r8 = new Requisition()
            {
                Employee = e8,
                dateOfRequest = new DateTime(2020, 8, 18, 9, 30, 52),
                dateOfAuthorizing = null,
                Authorizer = e3,
                status = "Applied",
                comment = null
            };
            RequisitionDetail r8rd1 = new RequisitionDetail() { Requisition = r8, Stationery = i3, reqQty = 8, rcvQty = 0, status = "Applied" };
            RequisitionDetail r8rd2 = new RequisitionDetail() { Requisition = r8, Stationery = i2, reqQty = 5, rcvQty = 0, status = "Applied" };

            Requisition r9 = new Requisition()
            {
                Employee = e24,
                dateOfRequest = new DateTime(2020, 8, 18, 9, 30, 00),
                dateOfAuthorizing = null,
                Authorizer = e4,
                status = "Applied",
                comment = null
            };
            RequisitionDetail r9rd1 = new RequisitionDetail() { Requisition = r9, Stationery = i1, reqQty = 8, rcvQty = 0, status = "Applied" };
            RequisitionDetail r9rd2 = new RequisitionDetail() { Requisition = r9, Stationery = i2, reqQty = 5, rcvQty = 0, status = "Applied" };

            Requisition r10 = new Requisition()
            {
                Employee = e25,
                dateOfRequest = new DateTime(2020, 8, 17, 9, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 9, 45, 00),
                Authorizer = e4,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r10rd1 = new RequisitionDetail() { Requisition = r10, Stationery = i2, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r10rd2 = new RequisitionDetail() { Requisition = r10, Stationery = i3, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r11 = new Requisition()
            {
                Employee = e26,
                dateOfRequest = new DateTime(2020, 8, 17, 8, 00, 00),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 8, 30, 00),
                Authorizer = e4,
                status = "Partially_Delivered",
                comment = null
            };
            RequisitionDetail r11rd1 = new RequisitionDetail() { Requisition = r11, Stationery = i1, reqQty = 7, rcvQty = 5, status = "Partially_Delivered" };
            RequisitionDetail r11rd2 = new RequisitionDetail() { Requisition = r11, Stationery = i2, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r12 = new Requisition()
            {
                Employee = e27,
                dateOfRequest = new DateTime(2020, 8, 18, 10, 30, 00),
                dateOfAuthorizing = null,
                Authorizer = e5,
                status = "Applied",
                comment = null
            };
            RequisitionDetail r12rd1 = new RequisitionDetail() { Requisition = r12, Stationery = i1, reqQty = 8, rcvQty = 0, status = "Applied" };
            RequisitionDetail r12rd2 = new RequisitionDetail() { Requisition = r12, Stationery = i2, reqQty = 5, rcvQty = 0, status = "Applied" };

            Requisition r13 = new Requisition()
            {
                Employee = e28,
                dateOfRequest = new DateTime(2020, 8, 17, 9, 30, 52),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 9, 45, 00),
                Authorizer = e5,
                status = "Delivered",
                comment = null
            };
            RequisitionDetail r13rd1 = new RequisitionDetail() { Requisition = r13, Stationery = i2, reqQty = 8, rcvQty = 8, status = "Delivered" };
            RequisitionDetail r13rd2 = new RequisitionDetail() { Requisition = r13, Stationery = i3, reqQty = 5, rcvQty = 5, status = "Delivered" };

            Requisition r14 = new Requisition()
            {
                Employee = e29,
                dateOfRequest = new DateTime(2020, 8, 17, 8, 00, 00),
                dateOfAuthorizing = new DateTime(2020, 8, 17, 8, 30, 00),
                Authorizer = e5,
                status = "Partially_Delivered",
                comment = null
            };
            RequisitionDetail r14rd1 = new RequisitionDetail() { Requisition = r14, Stationery = i1, reqQty = 7, rcvQty = 5, status = "Partially_Delivered" };
            RequisitionDetail r14rd2 = new RequisitionDetail() { Requisition = r14, Stationery = i2, reqQty = 5, rcvQty = 5, status = "Delivered" };

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
                priority = 1,
            };
            Supplier s2 = new Supplier()
            {
                supplierCode = "CHEP",
                name = " Cheap Stationer",
                contactPerson = "Mr Soh Kway Koh",
                phoneNum = "3543234",
                fax = " 4742434",
                address = "Blk 34, Clementi Road,#07-02 Ban Ban Soh Building,Singapore 110525",
                priority = 2,
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
                priority = 3,
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
                priority = 4,
            };
            Supplier[] supArr = { s1, s2, s3, s4 };
            for (int b = 0; b < supArr.Length; b++)
            {
                unitOfWork.GetRepository<Supplier>().Insert(supArr[b]);
            }
            unitOfWork.SaveChanges();
            #endregion

            #region SupplierItem 
            //1-80 May

            SupplierItem si1 = new SupplierItem() { SupplierId = 1, StationeryId = i2.Id, price = 1.2f};
            SupplierItem si2 = new SupplierItem() { SupplierId = 1, StationeryId = i3.Id, price = 1};
            SupplierItem si3 = new SupplierItem() { SupplierId = 1, StationeryId = i4.Id, price = 5 };
            SupplierItem si4 = new SupplierItem() { SupplierId = 1, StationeryId = i5.Id, price = 7};
            SupplierItem si5 = new SupplierItem() { SupplierId = 1, StationeryId = i6.Id, price = 7.5f };
            SupplierItem si6 = new SupplierItem() { SupplierId = 1, StationeryId = i7.Id, price = 0.4f};
            SupplierItem si7 = new SupplierItem() { SupplierId = 1, StationeryId = i8.Id, price = 0.56f};
            SupplierItem si8 = new SupplierItem() { SupplierId = 1, StationeryId = i9.Id, price = 0.3f};
            SupplierItem si9 = new SupplierItem() { SupplierId = 1, StationeryId = i10.Id, price = 0.35f  };
            SupplierItem si10= new SupplierItem() { SupplierId = 1, StationeryId = i12.Id, price = 0.4f  };
            SupplierItem si11= new SupplierItem() { SupplierId = 1, StationeryId = i13.Id, price = 0.38f  };
            SupplierItem si12= new SupplierItem() { SupplierId = 1, StationeryId = i14.Id, price = 0.22f };
            SupplierItem si13= new SupplierItem() { SupplierId = 1, StationeryId = i15.Id, price = 0.38f  };
            SupplierItem si14= new SupplierItem() { SupplierId = 1, StationeryId = i16.Id, price = 0.35f  };
            SupplierItem si15= new SupplierItem() { SupplierId = 1, StationeryId = i17.Id, price = 0.5f  };
            SupplierItem si16= new SupplierItem() { SupplierId = 1, StationeryId = i18.Id, price = 0.4f  };
            SupplierItem si17= new SupplierItem() { SupplierId = 1, StationeryId = i19.Id, price = 0.3f  };
            SupplierItem si18= new SupplierItem() { SupplierId = 1, StationeryId = i20.Id, price = 0.46f  };
            SupplierItem si19= new SupplierItem() { SupplierId = 1, StationeryId = i22.Id, price = 0.3f  };
            SupplierItem si20= new SupplierItem() { SupplierId = 1, StationeryId = i23.Id, price = 0.39f  };
            SupplierItem si21 = new SupplierItem() { SupplierId = 1, StationeryId = i24.Id, price = 8 };
            SupplierItem si22 = new SupplierItem() { SupplierId = 1, StationeryId = i25.Id, price = 16  };
            SupplierItem si23 = new SupplierItem() { SupplierId = 1, StationeryId = i26.Id, price = 18  };
            SupplierItem si24 = new SupplierItem() { SupplierId = 1, StationeryId = i27.Id, price = 16  };
            SupplierItem si25 = new SupplierItem() { SupplierId = 1, StationeryId = i28.Id, price = 18  };
            SupplierItem si26 = new SupplierItem() { SupplierId = 1, StationeryId = i29.Id, price = 0.2f  };
            SupplierItem si27 = new SupplierItem() { SupplierId = 1, StationeryId = i30.Id, price = 0.2f  };
            SupplierItem si28 = new SupplierItem() { SupplierId = 1, StationeryId = i32.Id, price = 0.2f  };
            SupplierItem si29 = new SupplierItem() { SupplierId = 1, StationeryId = i33.Id, price = 0.2f  };
            SupplierItem si30 = new SupplierItem() { SupplierId = 1, StationeryId = i34.Id, price = 8.99f };
            SupplierItem si31 = new SupplierItem() { SupplierId = 1, StationeryId = i35.Id, price = 8.99f };
            SupplierItem si32 = new SupplierItem() { SupplierId = 1, StationeryId = i36.Id, price = 8.99f };
            SupplierItem si33 = new SupplierItem() { SupplierId = 1, StationeryId = i37.Id, price = 8.99f };
            SupplierItem si34 = new SupplierItem() { SupplierId = 1, StationeryId = i38.Id, price = 6  };
            SupplierItem si35 = new SupplierItem() { SupplierId = 1, StationeryId = i39.Id, price = 7  };
            SupplierItem si36 = new SupplierItem() { SupplierId = 1, StationeryId= i40.Id, price = 20  };
            SupplierItem si37 = new SupplierItem() { SupplierId = 1, StationeryId = i42.Id, price = 12.99f };
            SupplierItem si38 = new SupplierItem() { SupplierId = 1, StationeryId = i43.Id, price = 12.99f };
            SupplierItem si39 = new SupplierItem() { SupplierId = 1, StationeryId = i44.Id, price = 16.99f };
            SupplierItem si40 = new SupplierItem() { SupplierId = 1, StationeryId = i45.Id, price = 19.99f };
            SupplierItem si41 = new SupplierItem() { SupplierId = 1, StationeryId = i46.Id, price = 19.99f };
            SupplierItem si42 = new SupplierItem() { SupplierId = 1, StationeryId = i47.Id, price = 10.99f };
            SupplierItem si43 = new SupplierItem() { SupplierId = 1, StationeryId = i48.Id, price = 10 };
            SupplierItem si44 = new SupplierItem() { SupplierId = 1, StationeryId = i49.Id, price = 4 };
            SupplierItem si45 = new SupplierItem() { SupplierId = 1, StationeryId = i50.Id, price = 5.99f };
            SupplierItem si46 = new SupplierItem() { SupplierId = 1, StationeryId = i52.Id, price = 5.99f };
            SupplierItem si47 = new SupplierItem() { SupplierId = 1, StationeryId = i53.Id, price = 13.99f };
            SupplierItem si48 = new SupplierItem() { SupplierId = 1, StationeryId = i54.Id, price = 13.99f };
            SupplierItem si49 = new SupplierItem() { SupplierId = 1, StationeryId = i55.Id, price = 13.99f };
            SupplierItem si50 = new SupplierItem() { SupplierId = 1, StationeryId = i56.Id, price = 5.99f };
            SupplierItem si51 = new SupplierItem() { SupplierId = 1, StationeryId = i57.Id, price = 8.99f };
            SupplierItem si52 = new SupplierItem() { SupplierId = 1, StationeryId = i58.Id, price = 6 };
            SupplierItem si53 = new SupplierItem() { SupplierId = 1, StationeryId = i59.Id, price = 8.5f };
            SupplierItem si54 = new SupplierItem() { SupplierId = 1, StationeryId = i60.Id, price = 8.5f };
            SupplierItem si55 = new SupplierItem() { SupplierId = 1, StationeryId = i62.Id, price = 2.8f };
            SupplierItem si56 = new SupplierItem() { SupplierId = 1, StationeryId = i63.Id, price = 3.6f };
            SupplierItem si57 = new SupplierItem() { SupplierId = 1, StationeryId = i64.Id, price = 2.4f };
            SupplierItem si58 = new SupplierItem() { SupplierId = 1, StationeryId = i65.Id, price = 3.6f };
            SupplierItem si59 = new SupplierItem() { SupplierId = 1, StationeryId = i66.Id, price = 4 };
            SupplierItem si60 = new SupplierItem() { SupplierId = 1, StationeryId = i67.Id, price = 3.9f };
            SupplierItem si61 = new SupplierItem() { SupplierId = 1, StationeryId = i68.Id, price = 2.2f };
            SupplierItem si62 = new SupplierItem() { SupplierId = 1, StationeryId = i69.Id, price = 4.9f };
            SupplierItem si63 = new SupplierItem() { SupplierId = 1, StationeryId = i70.Id, price = 2.5f };
            SupplierItem si64 = new SupplierItem() { SupplierId = 1, StationeryId = i72.Id, price = 5.9f };
            SupplierItem si65 = new SupplierItem() { SupplierId = 1, StationeryId = i73.Id, price = 16.2f };
            SupplierItem si66 = new SupplierItem() { SupplierId = 1, StationeryId = i74.Id, price = 20 };
            SupplierItem si67 = new SupplierItem() { SupplierId = 1, StationeryId = i75.Id, price = 12.2f };
            SupplierItem si68 = new SupplierItem() { SupplierId = 1, StationeryId = i76.Id, price = 2.5f };
            SupplierItem si69 = new SupplierItem() { SupplierId = 1, StationeryId = i77.Id, price = 3.5f };
            SupplierItem si70 = new SupplierItem() { SupplierId = 1, StationeryId = i78.Id, price = 10.5f };
            SupplierItem si71 = new SupplierItem() { SupplierId = 1, StationeryId = i79.Id, price = 12.5f };
            SupplierItem si72 = new SupplierItem() { SupplierId = 1, StationeryId = i80.Id, price = 7.2f };
            SupplierItem si73 = new SupplierItem() { SupplierId = 1, StationeryId = i82.Id, price = 3.4f };
            SupplierItem si74 = new SupplierItem() { SupplierId = 1, StationeryId = i83.Id, price = 12 };
            SupplierItem si75 = new SupplierItem() { SupplierId = 1, StationeryId = i84.Id, price = 12 };
            SupplierItem si76 = new SupplierItem() { SupplierId = 1, StationeryId = i85.Id, price = 12 };
            SupplierItem si77 = new SupplierItem() { SupplierId = 1, StationeryId = i86.Id, price = 12 };
            SupplierItem si78 = new SupplierItem() { SupplierId = 1, StationeryId = i87.Id, price = 12 };
            SupplierItem si79 = new SupplierItem() { SupplierId = 1, StationeryId = i88.Id, price = 10 };
            SupplierItem si80 = new SupplierItem() { SupplierId = 1, StationeryId = i89.Id, price = 12 };
           
            SupplierItem si81 = new SupplierItem() {SupplierId = 2, StationeryId = i11.Id, price = 0.2f};
            SupplierItem si82 = new SupplierItem() { SupplierId = 2, StationeryId = i21.Id, price = 0.62f };
            SupplierItem si83 = new SupplierItem() { SupplierId = 2, StationeryId = i31.Id, price = 0.2f };
            SupplierItem si84 = new SupplierItem() { SupplierId = 2, StationeryId = i41.Id, price = 13.99f };
            SupplierItem si85 = new SupplierItem() { SupplierId = 2, StationeryId = i51.Id, price = 5.99f };
            SupplierItem si86 = new SupplierItem() { SupplierId = 2, StationeryId = i61.Id, price = 8.5f };
            SupplierItem si87 = new SupplierItem() { SupplierId = 2, StationeryId = i71.Id, price = 2.2f };
            SupplierItem si88 = new SupplierItem() { SupplierId = 2, StationeryId = i81.Id, price = 5.2f };
            SupplierItem si89 = new SupplierItem() { SupplierId = 2, StationeryId = i9.Id, price = 0.5f };
            SupplierItem si90 = new SupplierItem() { SupplierId = 2, StationeryId = i10.Id, price = 0.55f };

            SupplierItem si91 = new SupplierItem() {SupplierId = 3, StationeryId = i15.Id, price = 0.45f};
            SupplierItem si92 = new SupplierItem() { SupplierId = 3, StationeryId = i25.Id, price = 16.8f };
            SupplierItem si93 = new SupplierItem() { SupplierId = 3, StationeryId = i35.Id, price = 9.99f };
            SupplierItem si94 = new SupplierItem() { SupplierId = 3, StationeryId = i45.Id, price = 21.8f };
            SupplierItem si95 = new SupplierItem() { SupplierId = 3, StationeryId = i55.Id, price = 15.99f };
            SupplierItem si96 = new SupplierItem() { SupplierId = 3, StationeryId = i1.Id, price = 5.99f };
            SupplierItem[] supItemArr = { si1,si2,si3,si4,si5,si6,si7,si8,si9,si10, si11, si12, si13, si14, si15, si16, si17, si18, si19, si20, si21, si22, si23, si24, si25, si26, si27, si28, si29, si30, si31, si32, si33, si34, si35, si36, si37, si38, si39, si40, si41, si42, si43, si44, si45, si46, si47, si48, si49, si50, si51, si52, si53, si54, si55, si56, si57, si58, si59, si60, si61, si62, si63, si64, si65, si66, si67, si68, si69, si70, si71, si72, si73, si74, si75, si76, si77, si78, si79, si80, si81, si82, si83, si84, si85, si86, si87, si88, si89, si90, si91, si92, si93, si94, si95};
            for (int z = 0; z < supItemArr.Length; z++)
            {
                unitOfWork.GetRepository<SupplierItem>().Insert(supItemArr[z]);
                unitOfWork.SaveChanges();
            }
            #endregion

            //only 2 Done May 
            #region StockAdjustment and Detail
            /// 1 receiveGood (after PO)
            /// 2 monthly check + 3 get good from ware house find missing or damage
            /// 4 manager revert adjustment after not issue voucher
            StockAdjustment sa1 = new StockAdjustment() { type = "regular inventory check", date = new DateTime(2020, 7, 27), EmployeeId = e33.Id};
            StockAdjustmentDetail sa1sad1 = new StockAdjustmentDetail() { stockAdjustment = sa1, StationeryId = i1.Id, discpQty = -5, comment = "Broken", Status="Pending Approval"};
            StockAdjustmentDetail sa1sad2 = new StockAdjustmentDetail() { stockAdjustment = sa1, StationeryId = i2.Id, discpQty = 1, comment = null, Status = "Approved" };
            StockAdjustmentDetail sa1sad3 = new StockAdjustmentDetail() { stockAdjustment = sa1, StationeryId = i3.Id, discpQty = -2, comment = "Missing", Status = "Rejected" };

            StockAdjustment sa2 = new StockAdjustment() { type = "missing when collecting goods", date = new DateTime(2020, 8, 27), EmployeeId = e34.Id };
            StockAdjustmentDetail sa2sad1 = new StockAdjustmentDetail() { stockAdjustment = sa2, StationeryId = i1.Id, discpQty = -10, comment = null, Status = "Approved" };
            StockAdjustmentDetail sa2sad2 = new StockAdjustmentDetail() { stockAdjustment = sa2, StationeryId = i2.Id, discpQty = -50, comment = null, Status = "Pending Approval" };
            StockAdjustmentDetail sa2sad3 = new StockAdjustmentDetail() { stockAdjustment = sa2, StationeryId = i3.Id, discpQty = -2, comment = "Broken" = "Approved" };

            StockAdjustment sa3 = new StockAdjustment() { type = "Warehouse flooding", date = new DateTime(2020, 6, 2), EmployeeId = e35.Id};
            StockAdjustmentDetail sa3sad1 = new StockAdjustmentDetail() { stockAdjustment = sa3, StationeryId = i4.Id, discpQty = -20, comment = "Damaged", Status = "Approved"};
            StockAdjustmentDetail sa3sad2 = new StockAdjustmentDetail() { stockAdjustment = sa3, StationeryId = i5.Id, discpQty = 1, comment = null, Status = "Approved" };
            StockAdjustmentDetail sa3sad3 = new StockAdjustmentDetail() { stockAdjustment = sa3, StationeryId = i6.Id, discpQty = -15, comment = "Soaked", Status = "Approved" };
            StockAdjustmentDetail sa3sad4 = new StockAdjustmentDetail() { stockAdjustment = sa3, StationeryId = i7.Id, discpQty = -18, comment = "Soaked", Status = "Approved"};
            StockAdjustmentDetail sa3sad5 = new StockAdjustmentDetail() { stockAdjustment = sa3, StationeryId = i8.Id, discpQty = 3, comment = null, Status = "Approved" };

            StockAdjustment sa4 = new StockAdjustment() { type = "regular inventory check", date = new DateTime(2020, 4, 24), EmployeeId = e33.Id};
            StockAdjustmentDetail sa4sad1 = new StockAdjustmentDetail() { stockAdjustment = sa4, StationeryId = i9.Id, discpQty = -5, comment = "Expired", Status="Pending Approval"};
            StockAdjustmentDetail sa4sad2 = new StockAdjustmentDetail() { stockAdjustment = sa4, StationeryId = i10.Id, discpQty = 1, comment = null, Status = "Approved" };
            StockAdjustmentDetail sa4sad3 = new StockAdjustmentDetail() { stockAdjustment = sa4, StationeryId = i11.Id, discpQty = -2, comment = "Missing", Status = "Approved" };

            StockAdjustment sa5 = new StockAdjustment() { type = "extra found after school fair", date = new DateTime(2020, 7, 7), EmployeeId = e34.Id };
            StockAdjustmentDetail sa5sad1 = new StockAdjustmentDetail() { stockAdjustment = sa5, StationeryId = i12.Id, discpQty = 3, comment = null, Status = "Approved" };
            StockAdjustmentDetail sa5sad2 = new StockAdjustmentDetail() { stockAdjustment = sa5, StationeryId = i13.Id, discpQty = 7, comment = null, Status = "Approved" };
            StockAdjustmentDetail sa5sad3 = new StockAdjustmentDetail() { stockAdjustment = sa5, StationeryId = i14.Id, discpQty = 11, comment = null, Status = "Approved" };

            StockAdjustment sa6 = new StockAdjustment() { type = "Warehouse Break in", date = new DateTime(2020, 5, 2), EmployeeId = e35.Id};
            StockAdjustmentDetail sa6sad1 = new StockAdjustmentDetail() { stockAdjustment = sa6, StationeryId = i15.Id, discpQty = -20, comment = "Stolen", Status = "Approved"};
            StockAdjustmentDetail sa6sad2 = new StockAdjustmentDetail() { stockAdjustment = sa6, StationeryId = i16.Id, discpQty = -19, comment = "Stolen", Status = "Approved" };
            StockAdjustmentDetail sa6sad3 = new StockAdjustmentDetail() { stockAdjustment = sa6, StationeryId = i17.Id, discpQty = -15, comment = "Stolen", Status = "Approved" };
            StockAdjustmentDetail sa6sad4 = new StockAdjustmentDetail() { stockAdjustment = sa6, StationeryId = i18.Id, discpQty = -18, comment = "Stolen", Status = "Approved"};
            StockAdjustmentDetail sa6sad5 = new StockAdjustmentDetail() { stockAdjustment = sa6, StationeryId = i19.Id, discpQty = -30, comment = "Stolen", Status = "Approved" };

            StockAdjustment sa7 = new StockAdjustment() { type = "regular inventory check", date = new DateTime(2020, 6, 26), EmployeeId = e33.Id};
            StockAdjustmentDetail sa7sad1 = new StockAdjustmentDetail() { stockAdjustment = sa7, StationeryId = i20.Id, discpQty = 5, comment = null, Status="Approved"};
            StockAdjustmentDetail sa7sad2 = new StockAdjustmentDetail() { stockAdjustment = sa7, StationeryId = i21.Id, discpQty = 13, comment = "Leftover", Status = "Pending Approval" };
            StockAdjustmentDetail sa7sad3 = new StockAdjustmentDetail() { stockAdjustment = sa7, StationeryId = i22.Id, discpQty = 20, comment = "New Box found", Status = "Rejected" };

            StockAdjustment sa8 = new StockAdjustment() { type = "missing when collecting goods", date = new DateTime(2020, 8, 17), EmployeeId = e34.Id };
            StockAdjustmentDetail sa8sad1 = new StockAdjustmentDetail() { stockAdjustment = sa8, StationeryId = i23.Id, discpQty = -7, comment = "Damaged", Status = "Approved" };
            StockAdjustmentDetail sa8sad2 = new StockAdjustmentDetail() { stockAdjustment = sa8, StationeryId = i24.Id, discpQty = -6, comment = "Missing", Status = "Pending Approval" };
            StockAdjustmentDetail sa8sad3 = new StockAdjustmentDetail() { stockAdjustment = sa8, StationeryId = i25.Id, discpQty = -1, comment = "Broken", Status = "Approved" };

            StockAdjustment sa9 = new StockAdjustment() { type = "Moving to New Warehouse Location", date = new DateTime(2020, 6, 12), EmployeeId = e35.Id};
            StockAdjustmentDetail sa9sad1 = new StockAdjustmentDetail() { stockAdjustment = sa9, StationeryId = i26.Id, discpQty = -20, comment = "Missing", Status = "Approved"};
            StockAdjustmentDetail sa9sad2 = new StockAdjustmentDetail() { stockAdjustment = sa9, StationeryId = i27.Id, discpQty = -21, comment = "Missing", Status = "Approved" };
            StockAdjustmentDetail sa9sad3 = new StockAdjustmentDetail() { stockAdjustment = sa9, StationeryId = i28.Id, discpQty = -15, comment = "Damaged during move", Status = "Rejected" };
            StockAdjustmentDetail sa9sad4 = new StockAdjustmentDetail() { stockAdjustment = sa9, StationeryId = i29.Id, discpQty = -18, comment = "Missing", Status = "Approved"};
            StockAdjustmentDetail sa9sad5 = new StockAdjustmentDetail() { stockAdjustment = sa9, StationeryId = i30.Id, discpQty = -33, comment = "Damaged during move", Status = "Pending Approval" };

            StockAdjustment sa10 = new StockAdjustment() { type = "regular inventory check", date = new DateTime(2020, 7, 27), EmployeeId = e33.Id};
            StockAdjustmentDetail sa10sad1 = new StockAdjustmentDetail() { stockAdjustment = sa10, StationeryId = i31.Id, discpQty = -25, comment = "Broken", Status = "Rejected"};
            StockAdjustmentDetail sa10sad2 = new StockAdjustmentDetail() { stockAdjustment = sa10, StationeryId = i32.Id, discpQty = -14, comment = "Missing", Status = "Rejected" };
            StockAdjustmentDetail sa10sad3 = new StockAdjustmentDetail() { stockAdjustment = sa10, StationeryId = i33.Id, discpQty = -28, comment = "Missing", Status = "Rejected" };

            StockAdjustment[] saArr = { sa1, sa2, sa3, sa4, sa5, sa6, sa7, sa8, sa9, sa10 };
            for (int e = 0; e < saArr.Length; e++)
            {
                unitOfWork.GetRepository<StockAdjustment>().Insert(saArr[e]);
                unitOfWork.SaveChanges();
            }
            StockAdjustmentDetail[] sadArr = { sa1sad1, sa1sad2, sa1sad3, sa2sad1, sa2sad2, sa2sad3, sa3sad1, sa3sad2, sa3sad3, sa3sad4, sa3sad5, sa4sad1, sa4sad2, sa4sad3, sa5sad1, sa5sad2, sa5sad3, 
                sa6sad1, sa6sad2, sa6sad3,sa6sad3, sa6sad4, sa6sad5, sa7sad1, sa7sad2, sa7sad3, sa8sad1, sa8sad2, sa8sad3, sa9sad1, sa9sad2, sa9sad3, sa9sad4, sa9sad5, sa10sad1, sa10sad2, sa10sad3 };
            for (int f = 0; f < sadArr.Length; f++)
            {
                unitOfWork.GetRepository<StockAdjustmentDetail>().Insert(sadArr[f]);
                unitOfWork.SaveChanges();
            }
            #endregion
            
            #region AdjustmentVoucher and Detail
            AdjustmentVoucher av1 = new AdjustmentVoucher() { StockAdjustmentId=sa1.Id, date=new DateTime(2020, 7, 31), EmployeeId=e32.Id, reason="" };
            AdjustmentVoucherDetail av1avd1 = new AdjustmentVoucherDetail() { adjustmentVoucher = av1, StockAdjustmentDetailId=sa1sad1.Id, price=29.95 };
            AdjustmentVoucherDetail av1avd2 = new AdjustmentVoucherDetail() { adjustmentVoucher = av1, StockAdjustmentDetailId=sa1sad2.Id, price=1.2 };
            AdjustmentVoucherDetail av1avd3 = new AdjustmentVoucherDetail() { adjustmentVoucher= av1, StockAdjustmentDetailId=sa1sad3.Id, price=2 };

            AdjustmentVoucher av2 = new AdjustmentVoucher() { StockAdjustmentId=sa2.Id, date=new DateTime(2020, 8, 31), EmployeeId=e32.Id, reason=""};
            AdjustmentVoucherDetail av2avd1 = new AdjustmentVoucherDetail() { adjustmentVoucher = av2, StockAdjustmentDetailId=sa2sad1.Id, price=59.9 };
            AdjustmentVoucherDetail av2avd2 = new AdjustmentVoucherDetail() { adjustmentVoucher = av2, StockAdjustmentDetailId=sa2sad2.Id, price= 60};
            AdjustmentVoucherDetail av2avd3 = new AdjustmentVoucherDetail() { adjustmentVoucher = av2, StockAdjustmentDetailId=sa2sad3.Id, price=2 };

            AdjustmentVoucher[] avArr = { av1, av2 };
            for (int g = 0; g < avArr.Length; g++)
            {
                unitOfWork.GetRepository<AdjustmentVoucher>().Insert(avArr[g]);
                unitOfWork.SaveChanges();
            }
            AdjustmentVoucherDetail[] avdArr = { av1avd1, av1avd2,av1avd3,av2avd1,av2avd2,av2avd3 };
            for (int h = 0; h < avdArr.Length; h++)
            {
                unitOfWork.GetRepository<AdjustmentVoucherDetail>().Insert(avdArr[h]);
                unitOfWork.SaveChanges();
            }
            
            #endregion
            /*
            #region Disbursement and Detail
            DisbursementList dl1 = new DisbursementList() { DepartmentId = dENGL, date = new DateTime(2020, 8, 27) };
            DisbursementList dl2 = new DisbursementList() { DepartmentId = dCPSC, date = new DateTime(2020, 8, 27) };
            #endregion

            // sry Pending seed 
            #region Purchase order and Detail

            #endregion*/
            unitOfWork.SaveChanges();


        }

        return isCreateDb;
    }


}