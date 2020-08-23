using BackEndAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAD.TempService;

namespace BackEndAD.ServiceInterface
{
    public interface IStoreSupervisorService
    {
        public Task<IList<StockAdjustSumById>> StockAdjustDetailInfo();
        public Task<IList<AdjustmentVocherInfo>> getAllAdjustDetailLineByAdjustId(StockAdjustSumById item);
        public Task<AdjustmentVocherInfo> getEachVoucherDetail(AdjustmentVocherInfo info);
        public Task<SupplierItem> findSupplierItemByIdAsync(int stkAdjId);

        public Task<StockAdjustmentDetail> findAllStockAdjustDetailByIdAsync(int stkAdjId);
        public Task<IList<StockAdjustmentDetail>> findAllStockAdjustDetailAsync();
        public Task<IList<StockAdjustment>> findAllStockAdjustmentAsync();
        public Task<IList<AdjustmentVocherInfo>> issueVoucher(StockAdjustSumById voc);
        public Task<IList<StockAdjustSumById>> rejectRequest(StockAdjustSumById voc);
        public Task<Employee> findEmployeeByIdAsync(int eId);
    }
}
