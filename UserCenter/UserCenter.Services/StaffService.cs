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
using UserCenter.DTO.Common;

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
        public async Task UpdateSaveAsync(StaffDTO dto)
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                var id = dto.Id;
                var entity = await ctx.Staffs.Where(s => s.Id == id).FirstOrDefaultAsync();
                if (entity==null)
                {
                    throw new Exception("编辑的人员不存在");
                }
                entity.JobTitle = dto.JobTitle;
                entity.ParentId = dto.ParentId;
                entity.PhoneNumber = dto.PhoneNumber;
                entity.ResignationDate = dto.ResignationDate;
                entity.StaffCode = dto.StaffCode;
                entity.Telephone = dto.Telephone;
                entity.Birthday = dto.Birthday;
                entity.CALLNumber = dto.CALLNumber;
                entity.ChName = dto.ChName;
                entity.DepartmentId = dto.DepartmentId;
                entity.EmploymentDate = dto.EmploymentDate;
                entity.EnName = dto.EnName;
                entity.Email = dto.Email;
                entity.Email = dto.Remark;

                entity.UpdateTime = dto.UpdateTime;
                entity.UserName = dto.UserName;
                await ctx.SaveChangesAsync();
            }
        }

        public Task DeleteByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task BatchDeleteAsync(string ids)
        {
            var idArr = ids.Split(',').Select(s => { return Convert.ToInt64(s); });
            using (UCDbContext ctx = new UCDbContext())
            {
                var entitys = await ctx.Staffs.Where(s => idArr.Contains(s.Id)).ToListAsync();
                if (entitys != null)
                {
                    ctx.Staffs.RemoveRange(entitys);
                    await ctx.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteByIdAsync(long id)
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                var entity = await ctx.Staffs.Where(s=>s.Id == id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    ctx.Staffs.Remove(entity);
                    await ctx.SaveChangesAsync();
                }
            }
        }

        public async Task<StaffQueryDTO> GetByIdAsync(long id)
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                var staff = await ctx.Staffs.Where(s =>s.Status && s.Id == id).SingleOrDefaultAsync();
                return ToDTO(staff);
            }
        }

        public async Task<IEnumerable<StaffQueryDTO>> GetPageListAsync(StaffQueryCondModel queryCond)
        {
            var page = queryCond.Page;
            var page_size = queryCond.Page_size;
            using (UCDbContext ctx = new UCDbContext())
            {
                var queryAble = ctx.Staffs.AsNoTracking().OrderBy(s => s.Id).Where(s=>s.Status);//用asNotracking可以提升一点性能，不跟踪状态变化
                if (!string.IsNullOrEmpty(queryCond.StaffCode))
                {
                    queryAble= queryAble.Where(s => s.StaffCode.Contains(queryCond.StaffCode));
                }
                if (!string.IsNullOrEmpty(queryCond.ChName))
                {
                    queryAble = queryAble.Where(s => s.ChName.Contains(queryCond.ChName));
                }
                if (!string.IsNullOrEmpty(queryCond.ParentCode))
                {
                    queryAble = queryAble.Where(s => s.ParentStaff.StaffCode.Contains(queryCond.ParentCode));
                }
                if (queryCond.BeginEmploymentDate != DateTime.MinValue&& queryCond.EndEmploymentDate != DateTime.MinValue)
                {
                    queryAble = queryAble.Where(s => (DbFunctions.DiffDays(s.EmploymentDate, queryCond.BeginEmploymentDate) <= 0) && (DbFunctions.DiffDays(s.EmploymentDate, queryCond.EndEmploymentDate) >= 0));//后面减前面 &&翻译成 AND
                }
                else
                {
                    if (queryCond.BeginEmploymentDate != DateTime.MinValue)
                    {
                        queryAble = queryAble.Where(s => DbFunctions.DiffDays(s.EmploymentDate, queryCond.BeginEmploymentDate) <= 0);//后面减前面
                    }
                    if (queryCond.EndEmploymentDate != DateTime.MinValue)
                    {
                        queryAble = queryAble.Where(s => DbFunctions.DiffDays(s.EmploymentDate, queryCond.EndEmploymentDate) >= 0);
                    }
                }
                queryAble = queryAble.Skip((page - 1) * page_size);
                queryAble = queryAble.Take(page_size);
                var datas = await queryAble.ToListAsync();
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
            throw new NotImplementedException();//.Where(s => s.Status)
        }
       
        private static Staff ToEntity(StaffDTO dto)
        {
            Staff entity = new Staff();
            entity.StaffCode = dto.StaffCode;
            entity.ChName = dto.ChName;
            entity.EnName = dto.EnName;
            entity.Telephone = dto.Telephone;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.CALLNumber = dto.CALLNumber;
            entity.DepartmentId = dto.DepartmentId;
            entity.JobTitle = dto.JobTitle;
            entity.Birthday = dto.Birthday;
            entity.EmploymentDate = dto.EmploymentDate;
            entity.ResignationDate = dto.ResignationDate;
            entity.ParentId = dto.ParentId ?? 1;
            entity.Email = dto.Email;
            entity.Remark = dto.Remark;

            entity.CreateDateTime = dto.CreateDateTime;
            entity.UpdateTime = dto.UpdateTime;
            entity.Status = dto.Status;
            entity.UserName = dto.UserName;
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
            dto.DepartmentId = entity.DepartmentId;
            dto.DepartmentNumber = entity.Department?.DepartCode;
            dto.JobTitle = entity.JobTitle;
            dto.Birthday = entity.Birthday;
            dto.EmploymentDate = entity.EmploymentDate;
            dto.ResignationDate = entity.ResignationDate;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.ParentCode = entity.ParentStaff?.StaffCode;
            dto.ParentId = entity.ParentId ?? 0;

            dto.Email = entity.Email;
            dto.Remark = entity.Remark;
         //   dto.UserName = entity.User?.NickName;//报错
            return dto;
        }

        public async Task<int> GetCountAsync()
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                return await ctx.Staffs.Where(s => s.Status).CountAsync();
            }
        }


        /// <summary>
        /// 获取到员工上级下拉数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListResultModel>> GetStaffParentSelectItem()
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                var entitys =  await ctx.Staffs.Where(s => s.Status).ToListAsync();
                List<SelectListResultModel> result = new List<SelectListResultModel>();
                foreach (var item in entitys)
                {
                    result.Add(new SelectListResultModel {text =item.StaffCode,value=item.Id });
                }
                return result;
            }
        }
    }
}
