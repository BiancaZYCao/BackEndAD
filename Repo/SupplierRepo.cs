using BackEndAD.DataContext;
using BackEndAD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Repo
{
    public interface ISupplierRepo
    {
        public Task<List<Supplier>> findAllSuppliersAsync();
        public void SaveSupplier(Supplier s);
    }
    public class SupplierRepo : ISupplierRepo
    {
        private ProjectContext _context;
        public SupplierRepo(ProjectContext _context)
        {
            this._context = _context;
        }
        public async Task<List<Supplier>> findAllSuppliersAsync()
        {
            List<Supplier> slist = await _context.Supplier_Table.ToListAsync();
            return slist;
        }

        public void SaveSupplier(Supplier s)
        {
            Supplier sup = new Supplier()
            {
                supplierCode = s.supplierCode,
                name = s.name,
                contactPerson = s.contactPerson,
                email= s.email,
                phoneNum = s.phoneNum,
                gstRegisNo = s.gstRegisNo,
                fax = s.fax,
                address = s.address,
                priority = s.priority,
            };

            _context.Add(sup);
            _context.SaveChanges();
        }
    }
}
