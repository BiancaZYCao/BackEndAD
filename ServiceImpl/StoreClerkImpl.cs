using BackEndAD.DataContext;
using BackEndAD.Models;
using BackEndAD.Repo;
using BackEndAD.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.ServiceImpl
{
    public class StoreClerkServiceImpl : IStoreClerkService
    {
        public IUnitOfWork<ProjectContext> unitOfWork;
       
        public StoreClerkServiceImpl(IUnitOfWork<ProjectContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Stationery
        public async Task<IList<Stationery>> findAllStationeriesAsync()
        {
            IList<Stationery> list = await unitOfWork.GetRepository<Stationery>().GetAllAsync();
            return list;
        }

        public async Task<Stationery> findStationeryByIdAsync(int stationeryId)
        {
            Stationery s = await unitOfWork.GetRepository<Stationery>().FindAsync(stationeryId);
            return s;
        }
        #endregion

        #region supplier
        public async Task<IList<Supplier>> findAllSuppliersAsync()
        {
            IList<Supplier> list = await unitOfWork.GetRepository<Supplier>().GetAllAsync();
            return list;
        }

        public async Task<Supplier> findSupplierByIdAsync(int supplierId)
        {
            Supplier sup = await unitOfWork.GetRepository<Supplier>().FindAsync(supplierId);
            return sup;
        }

        public void deleteSupplier(int id)
        {
            Supplier s = unitOfWork.GetRepository<Supplier>().GetById(id);
            unitOfWork.GetRepository<Supplier>().DeleteS(s);
            unitOfWork.SaveChanges();
        }

        public async void saveSupplier(Supplier s)
        {
            unitOfWork.GetRepository<Supplier>().Insert(s);
            unitOfWork.SaveChanges();
            //unitOfWork.GetRepository<Supplier>().Save();
        }
        public async void updateSupplier(int id)
        {
            Supplier s = unitOfWork.GetRepository<Supplier>().GetById(id);
            unitOfWork.GetRepository<Supplier>().Update(s);
            unitOfWork.SaveChanges();
            //unitOfWork.GetRepository<Supplier>().Save();

        }
        #endregion


    }
}
