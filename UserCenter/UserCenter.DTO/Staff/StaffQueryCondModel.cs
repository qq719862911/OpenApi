using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO.Common;

namespace UserCenter.DTO.Staff
{
    public class StaffQueryCondModel : BaseQueryCondModel
    {
        /// <summary>
        /// 员工代号
        /// </summary>
        public string StaffCode { get; set; }
        public string ChName { get; set; }
        /// <summary>
        /// 用户输入的开始的(入职时间)
        /// </summary>
        public DateTime BeginEmploymentDate { get; set; }
        /// <summary>
        /// 用户输入的结束的(入职时间)
        /// </summary>
        public DateTime EndEmploymentDate { get; set; }
        /// <summary>
        /// 上级主管代号
        /// </summary>

        public string ParentCode { get; set; }
    }
}
