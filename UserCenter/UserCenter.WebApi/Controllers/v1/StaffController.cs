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
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using UserCenter.WebApi.Models.Staff;

namespace UserCenter.WebApi.Controllers.v1
{
    public class StaffController : BaseApiController
    {
        public IStaffService StaffService { get; set; }

        [HttpGet]
        public async Task<JsonResult<ResultModel>> GetStaffParentSelectList()
        {
            var dtoList = await StaffService.GetStaffParentSelectItem();
            var res = new ResultModel() { status = 200, data = dtoList, msg = "ok" };
            return Json(res);
        }

        //public HttpResponseMessage Options()
        //{
        //    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        //}

        //[HttpOptions]
        [HttpGet]
        public async Task<JsonResult<ResultModel>> GetPageAsync([FromUri]StaffQueryCondModel queryCond)
        {
            var page = queryCond.Page;
            var page_size = queryCond.Page_size;
            if (page <= 0)
            {
                var res = new ResultModel() { status = 200, data = new { }, msg = "error" };
                return Json(res);
            }
            else
            {
                //获取总数
                var dataCount = await StaffService.GetCountAsync();
                if (dataCount <= 0)
                {

                    var res = new ResultModel() { status = 200, data = new { }, msg = "empty" };
                    return Json(res);
                }
                if ((page - 1) * page_size > dataCount)
                {
                    var res = new ResultModel() { status = 200, data = new { }, msg = "error" };
                    return Json(res);
                }
                else
                {
                    var list = await StaffService.GetPageListAsync(queryCond);
                    var pageBodyModel = new PageBodyModel()
                    {
                        current_page = page,
                        per_page = page_size,
                        data = list.ToArray(),
                        total = dataCount,
                    };
                    var pageModel = new GetPageModel { list = pageBodyModel };
                    var res = new ResultModel() { status = 200, data = pageModel, msg = "ok" };
                    return Json(res);
                }
            }
        }

        [HttpGet]
        public async Task<JsonResult<ResultModel>> GetAsync(long id)
        {
            var staffQueryDTO = await StaffService.GetByIdAsync(id);
            var res = new ResultModel() { status = 200, data = staffQueryDTO, msg = "ok" };
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult<ResultModel>> AddNewAsync([FromBody]StaffDTO dto)
        {
            //设置当前操作用于的username
            dto.UserName = "tom";
            dto.CreateDateTime = DateTime.UtcNow;
            dto.Status = false;
            dto.UpdateTime = DateTime.UtcNow;
            var id = await StaffService.AddNewAsync(dto);
            var res = new ResultModel() { status = 200, data = new { Id = id }, msg = "ok" };
            return Json(res);
        }

        [HttpGet]
        public async Task<JsonResult<ResultModel>> UpdateAsync(long id)
        {
            var dto = await StaffService.GetByIdAsync(id);
            var res = new ResultModel() { status = 200, data = dto, msg = "ok" };
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult<ResultModel>> UpdateSaveAsync([FromBody]StaffDTO dto)
        {
            dto.UserName = "tom";
            dto.UpdateTime = DateTime.UtcNow;
            await StaffService.UpdateSaveAsync(dto);
            var res = new ResultModel() { status = 200, data = { }, msg = "ok" };
            return Json(res);
        }

        [HttpGet]
        public async Task<JsonResult<ResultModel>> DeleteAsync(long id)
        {
            Task t1 = StaffService.DeleteByIdAsync(id);
            ResultModel res = null;
            try
            {
                await t1;
                res = new ResultModel() { status = 200, data = new { }, msg = "ok" };
            }
            catch (Exception ex)
            {
                res = new ResultModel() { status = 500, data = new { }, msg = ex.Message };
            }
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult<ResultModel>> BatchDeleteAsync([FromBody] BatchDelParModel parModel)
        {
            Task t1 = StaffService.BatchDeleteAsync(parModel.Ids);
            ResultModel res = null;
            try
            {
               await t1;
               res = new ResultModel() { status = 200, data = new { }, msg = "ok" };
            }
            catch (Exception ex)
            {
                res = new ResultModel() { status = 500, data = new {}, msg = ex.Message };
            }
            return Json(res);
        }

        [HttpGet]
        public string TestApi()
        {
            return "aaaaaaaaaa";
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
