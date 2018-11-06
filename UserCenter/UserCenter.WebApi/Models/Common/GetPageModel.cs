using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserCenter.WebApi.Models.Common
{
    public class GetPageModel
    {
        public PageBodyModel list { get; set; }
    }
    public class PageBodyModel
    {
        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public object[] data { get; set; }
    }
}