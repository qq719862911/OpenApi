using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.DTO.Common;
using UserCenter.DTO.Staff;

namespace UserCenter.IServices
{
    public interface IStaffService : IServiceTag
    {
       
        Task<long> AddNewAsync(StaffDTO addDto);
        Task<bool> StaffExistsAsync(string id);
        Task<StaffQueryDTO> GetByIdAsync(long id);
        Task DeleteByIdAsync(long id);
        Task DeleteByCodeAsync(string code);
        Task<IEnumerable<StaffQueryDTO>> GetPageListAsync(StaffQueryCondModel queryCond);

        Task<int> GetCountAsync();
        Task BatchDeleteAsync(string ids);
        Task UpdateSaveAsync(StaffDTO dto);
        /// <summary>
        /// 获取到员工上级下拉数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListResultModel>> GetStaffParentSelectItem();
    }
}
