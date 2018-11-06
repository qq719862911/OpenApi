using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Services.Models
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdateTime { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// 当前登陆的账号，操作表时被记录
        /// </summary>
        public string UserName { get; set; }
        public bool Status { get; set; } = true;
    }
}
