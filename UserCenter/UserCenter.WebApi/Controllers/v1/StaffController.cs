using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserCenter.DTO;
using UserCenter.DTO.Staff;
using UserCenter.IServices;
using UserCenter.WebApi.Models.Common;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace UserCenter.WebApi.Controllers.v1
{
    public class StaffController : ApiController
    {
        public IStaffService StaffService { get; set; }

        [HttpGet]
        public async Task<JsonResult<ResultModel>> GetPageAsync(int page, int page_size)
        {
            if (page<=0)
            {
                var res = new ResultModel() { status = 200, data = new { }, msg = "error" };
                return Json(res);
            }
            else
            {
                //获取总数
               var dataCount = await StaffService.GetCountAsync();
                if ((page - 1) * page_size > dataCount) {
                    var res = new ResultModel() { status = 200, data = new { }, msg = "error" };
                    return Json(res);
                }
                else
                {
                   var list =await StaffService.GetPageListAsync(page, page_size);
                    var res = new ResultModel() { status = 200, data = list, msg = "ok" };
                    return Json(res);
                }
            }
        }

        [HttpGet]
        public async Task<JsonResult<ResultModel>> AddNewAsync([FromUri]StaffDTO addDto)
        {
            var id = await StaffService.AddNewAsync(addDto);
            var res = new ResultModel() { status = 200, data = new{ Id=id}, msg = "ok" };
            return Json(res);
        }

        // POST: api/Staff
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Staff/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Staff/5
        public void Delete(int id)
        {
        }
    }
}
