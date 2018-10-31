using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Services.Models
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class Staff : BaseModel
    {
        public string StaffCode { get; set; }
        public string ChName { get; set; }
        public string EnName { get; set; }
        public string Telephone { get; set; }
        public string PhoneNumber { get; set; }
        public string CALLNumber { get; set; }
        public string DepartmentNumber { get; set; }
        public string JobTitle { get; set; }
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime EmploymentDate { get; set; }
        public DateTime ResignationDate { get; set; }
        /// <summary>
        /// 上级id
        /// </summary>
        public long? ParentId { get; set; }
        public virtual Staff ParentStaff { get; set; }


        /// <summary>
        /// 子节点
        /// </summary>
         public virtual ICollection<Staff> Childs { get; set; }
    }
}
