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
        public string CALLNumber { get; set; }
        public string DepartmentNumber { get; set; }
        public string JobTitle { get; set; }
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime EmploymentDate { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime CreateDateTime { get; set; }
        public long? ParentId { get; set; }
    }
}
