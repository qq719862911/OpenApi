using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserCenter.WebApi.Models.Common
{
    public class ResultModel
    {
        public int status { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
}