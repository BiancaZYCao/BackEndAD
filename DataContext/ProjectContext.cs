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
        public DbSet<AdjustmentVoucher> AdjustmentVoucher_Table { get; set; }
        public DbSet<AdjustmentVoucherDetail> AdjustmentVoucherDetail_Table { get; set; }
        public DbSet<CollectionInfo> CollectionInfo_Table { get; set; }
        public DbSet<Department> Department_Table { get; set; }
        public DbSet<DisbursementDetail> DisbursementDetail_Table { get; set; }
        public DbSet<DisbursementList> DisbursementList_Table { get; set; }
        public DbSet<Employee> Employee_Table { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder_Table { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetail_Table { get; set; }
        public DbSet<Requisition> Requisition_Table { get; set; }
        public DbSet<RequisitionDetail> RequisitionDetail_Table { get; set; }
        public DbSet<Stationery> Stationery_Table { get; set; }
        public DbSet<StockAdjustment> StockAdjustment_Table { get; set; }
        public DbSet<StockAdjustmentDetail> StockAdjustmentDetail_Table { get; set; }
        public DbSet<Supplier> Supplier_Table { get; set; }
        

        //Below data tables just for testing 
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("test");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
            /*does not work
            //modelBuilder.Entity<Department>()
            .HasOne(d => d.Collection)
            .WithMany(ci => ci.Departments)
            .HasForeignKey(d => d.CollectionId);*/
            modelBuilder.Entity<Employee>()
            .HasOne(r => r.department)
            .WithMany(e => e.employees)
            .HasForeignKey(p => p.departmentId);
            
            /*got prob
            modelBuilder.Entity<CollectionInfo>()
            .HasOne(c => c.clerk)
            .WithOne()
            .HasForeignKey(c => c.clerkId);*/
            // This is reference for advanced constraints setting.
            /*modelBuilder.Entity<Requisition>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.Requisitions)
                .HasForeignKey(p => p.EmployeeId);
            modelBuilder.Entity<RequisitionDetail>()
                .HasOne(rd => rd.Requisition)
                .WithMany(r => r.RequisitionDetails)
                .HasForeignKey(p => p.RequisitionId);
            //above 2 cannot work together u can only choose one navigation relationship,
            // so I give up . -Bianca


            modelBuilder.Entity<Employee>()
            .HasOne(r => r.department)
            .WithMany(e => e.employees)
            .HasForeignKey(p => p.departmentId);*/
            //.HasForeignKey(p => p.headId);
            /*modelBuilder.Entity<Department>()
                 .Property(d => d.head)
                 .HasColumnName("headId");*/

        }



    }
}
 