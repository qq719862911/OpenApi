using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Services.Models
{
    public class UserGroup : BaseModel
    {
        public string Name { get; set; }
        /// <summary>
        /// 用户组有多个用户
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
