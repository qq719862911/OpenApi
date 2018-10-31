using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.DTO.Staff
{
   public class StaffQueryDTO: StaffDTO
    {
        /// <summary>
        /// 上级主管代号
        /// </summary>
        public string ParentCode { get; set; }
    }
}
