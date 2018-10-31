using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
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
        Task<IEnumerable<StaffQueryDTO>> GetPageListAsync(int page, int page_size);

        Task<int> GetCountAsync();



    }
}
