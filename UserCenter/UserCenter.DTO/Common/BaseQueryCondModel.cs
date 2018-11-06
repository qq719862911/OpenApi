using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.DTO.Common
{
    /// <summary>
    /// 分页查询条件父类
    /// </summary>
   public class BaseQueryCondModel
    {
        public int Page { get; set; }
        public int Page_size { get; set; }
    }
}
