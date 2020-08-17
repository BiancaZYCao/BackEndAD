using BackEndAD.DataContext;
using BackEndAD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Repo
{
    public interface IInventoryRepo
    {
        public Task<List<Inventory>> findAllInventoriesAsync();
        public Task<Inventory> findInventoryByIdAsync(int id);
    }

    public class InventoryRepo : IInventoryRepo
    {
        private ProjectContext _context;
        public InventoryRepo(ProjectContext _context)
        {
            this._context = _context;
        }

        public async Task<List<Inventory>> findAllInventoriesAsync()
        {
            List<Inventory> ilist = await _context.Inventory.ToListAsync();
            return ilist;
        }

        public async Task<Inventory> findInventoryByIdAsync(int id)
        {
            Inventory inv = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == id);
            return inv;
        }

    }
}
