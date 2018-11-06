using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Services.Models
{
    public class Department : BaseModel
    {
        public string DepartCode { get; set; }
        /// <summary>
        /// 部门全称
        /// </summary>
        public string FullName { get; set; }
        public string EnName { get; set; }
    }
}
