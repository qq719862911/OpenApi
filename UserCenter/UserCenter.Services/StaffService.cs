using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.DTO.Staff;
using UserCenter.IServices;
using UserCenter.Services.Models;
using System.Data.Entity;//这里面有异步方法

namespace UserCenter.Services
{
    public class StaffService : IStaffService
    {
        public async Task<long> AddNewAsync(StaffDTO addDto)
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                var entity = ToEntity(addDto);
                var staff = ctx.Staffs.Add(entity);
                await ctx.SaveChangesAsync();
                return staff.Id;
            }
        }

        public Task DeleteByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<StaffQueryDTO> GetByIdAsync(long id)
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                var staff = await ctx.Staffs.Where(s => s.Id == id).SingleOrDefaultAsync();
                return ToDTO(staff);
            }
        }

        public async Task<IEnumerable<StaffQueryDTO>> GetPageListAsync(int page, int page_size)
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                var datas = await ctx.Staffs.OrderBy(s => s.Id).Skip((page - 1) * page_size).Take(page_size).ToListAsync();
                List<StaffQueryDTO> staffDtos = new List<StaffQueryDTO>();
                foreach (var item in datas)
                {
                    staffDtos.Add(ToDTO(item));
                }
                return staffDtos;
            }
        }



        public Task<bool> StaffExistsAsync(string id)
        {
            throw new NotImplementedException();
        }
        //private static StaffDTO ToDTO(Staff staff, Staff parentStaff)
        //{
        //    StaffDTO dto = new StaffDTO();
        //    dto.Id = staff.Id;
        //    dto.ChName = staff.ChName;
        //    dto.PhoneNumber = staff.PhoneNumber;
        //    dto.JobTitle = staff.JobTitle;
        //    dto.ParentCode = parentStaff.StaffCode;
        //    dto.ParentId = staff.ParentId;
        //    dto.ResignationDate = staff.ResignationDate;
        //    dto.Telephone = staff.Telephone;
        //    dto.Birthday = staff.Birthday;
        //    dto.CALLNumber = staff.CALLNumber;
        //    dto.EnName = staff.EnName;
        //    dto.DepartmentNumber = staff.DepartmentNumber;
        //    dto.EmploymentDate = staff.EmploymentDate;
        //    return dto;
        //}
        private static Staff ToEntity(StaffDTO dto)
        {
            Staff entity = new Staff();
            entity.StaffCode = dto.StaffCode;
            entity.ChName = dto.ChName;
            entity.EnName = dto.EnName;
            entity.Telephone = dto.Telephone;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.CALLNumber = dto.CALLNumber;
            entity.DepartmentNumber = dto.DepartmentNumber;
            entity.JobTitle = dto.JobTitle;
            entity.Birthday = dto.Birthday;
            entity.EmploymentDate = dto.EmploymentDate;
            entity.ResignationDate = dto.ResignationDate;
            entity.ParentId = dto.ParentId ?? 1;
            entity.CreateDateTime = dto.CreateDateTime;
            return entity;
        }

        private static StaffQueryDTO ToDTO(Staff entity)
        {
            StaffQueryDTO dto = new StaffQueryDTO();
            dto.Id = entity.Id;
            dto.StaffCode = entity.StaffCode;
            dto.ChName = entity.ChName;
            dto.EnName = entity.EnName;
            dto.Telephone = entity.Telephone;
            dto.PhoneNumber = entity.PhoneNumber;
            dto.CALLNumber = entity.CALLNumber;
            dto.DepartmentNumber = entity.DepartmentNumber;
            dto.JobTitle = entity.JobTitle;
            dto.Birthday = entity.Birthday;
            dto.EmploymentDate = entity.EmploymentDate;
            dto.ResignationDate = entity.ResignationDate;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.ParentCode = entity.ParentStaff?.StaffCode;
            dto.ParentId = entity.ParentId ?? 0;
            return dto;
        }

        public async Task<int> GetCountAsync()
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                return await ctx.Staffs.CountAsync();
            }
        }

        
    }
}
