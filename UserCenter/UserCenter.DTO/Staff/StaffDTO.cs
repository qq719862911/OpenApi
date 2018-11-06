using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.DTO.Staff
{
   public class StaffDTO
    {
        public long Id { get; set; }
        public string StaffCode { get; set; }
        public string ChName { get; set; }
        public string EnName { get; set; }
        public string Telephone { get; set; }
        public string PhoneNumber { get; set; }
        public string CALLNumber { get; set; }//前台拿到的会是callNumber  mvc大小写处理问题 
        public long DepartmentId { get; set; }//部门的代号，关联到部门表
        public string DepartmentNumber { get; set; }//部门的代号，关联到部门表
        public string JobTitle { get; set; }
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime? EmploymentDate { get; set; }
        public DateTime? ResignationDate { get; set; }

        public string Email { get; set; }
        public string Remark { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 当前登陆的账号，操作表时被记录
        /// </summary>
        public string UserName { get; set; }
        public bool Status { get; set; }

        public long? ParentId { get; set; }
    }
}
