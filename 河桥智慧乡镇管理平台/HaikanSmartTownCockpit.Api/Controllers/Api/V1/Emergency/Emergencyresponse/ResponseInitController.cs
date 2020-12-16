using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haikan3.Utils;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Emergency.Emergencyresponse;
using HaikanSmartTownCockpit.Api.ViewModels.Emergency.Emergencyresponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Emergency.Emergencyresponse
{
    [Route("api/v1/emergency/emergencyresponse/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class ResponseInitController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public ResponseInitController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }


        #region 后台管理列表
        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(ResponseInitRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.ResponseInit
                            select new ResponseInitList
                            {
                                ResponseInitUuid = p.ResponseInitUuid,
                                Villages = p.Villages,
                                Level = p.Level,
                                Situation = p.Situation,
                                IsDelete = p.IsDelete,
                                ReleaseState = p.ReleaseState,
                                Id = p.Id
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Level.Contains(payload.Kw.Trim()) || x.Situation.Contains(payload.Kw.Trim()));
                }

                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDelete == Convert.ToInt32(((CommonEnum.IsDeleted)payload.IsDeleted)));
                }

                query = query.OrderByDescending(x => x.Id);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                for(int i=0;i<list.Count;i++)
                {
                    if(!string.IsNullOrEmpty(list[i].Villages))
                    {
                        var village = list[i].Villages.Split(',');
                        foreach(var item in village)
                        {
                            var villagesingle = _dbContext.Village.FirstOrDefault(x => x.VillageUuid == Guid.Parse(item));
                            if(villagesingle!=null)
                            {
                                list[i].VillageName += villagesingle.VillageName + ",";
                            }
                        }
                        list[i].VillageName = list[i].VillageName.Trim(',');
                    }
                    
                }

                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:响应发起记录信息数据", _dbContext);
                return Ok(response);
            }
        }
        #endregion
        #region 创建
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(ResponseInitViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                //if (_dbContext.ResponseInit.Count(x => x.MemberName == model.MemberName) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}

                var entity = _mapper.Map<ResponseInitViewModel, ResponseInit>(model);
                entity.ResponseInitUuid = Guid.NewGuid();
                entity.IsDelete = 0;
                entity.ReleaseState = 0;
                entity.Addpeoople = AuthContextService.CurrentUser.DisplayName;

                _dbContext.ResponseInit.Add(entity);
                _dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }
        #endregion
        #region 编辑
        /// <summary>
        /// 发送响应
        /// </summary>
        /// <param name="guid">惟一编码</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult SendInit(Guid guid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                var entity = _dbContext.ResponseInit.FirstOrDefault(x => x.ResponseInitUuid == guid);
                if(entity==null)
                {
                    response.SetFailed("响应发起信息为空");
                    return Ok(response);
                }
                entity.ReleaseState = 1;
                if (!string.IsNullOrEmpty(entity.Villages))
                {
                    var village = entity.Villages.Split(',');
                    foreach (var item in village)
                    {
                        var villagesingle = _dbContext.Village.FirstOrDefault(x => x.VillageUuid == Guid.Parse(item));
                        if (villagesingle != null)
                        {
                            var member = _dbContext.VillageMember.Where(x=>x.VillageUuid== villagesingle.VillageUuid&&x.IsDelete==0).ToList();
                            for(int j=0;j<member.Count;j++)
                            {

                                var infoexit = _dbContext.ResponseInfo.FirstOrDefault(x=>x.VillageMemberUuid==member[j].VillageMemberUuid&&x.ResponseInitUuid == entity.ResponseInitUuid);
                                if(infoexit==null)
                                {
                                    var responseinfo = new ResponseInfo();
                                    responseinfo.ResponseInfoUuid = Guid.NewGuid();
                                    responseinfo.Village = villagesingle.VillageName;
                                    responseinfo.TongzhiRen = member[j].MemberName;
                                    responseinfo.XiangyingDj = entity.Level;
                                    responseinfo.TongzhiQingk = entity.Situation;
                                    responseinfo.Phone = member[j].MemberPhone;
                                    responseinfo.IsDeleted = 0;
                                    responseinfo.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                                    responseinfo.AddPeople = AuthContextService.CurrentUser.DisplayName;
                                    responseinfo.VillageMemberUuid = member[j].VillageMemberUuid;
                                    responseinfo.ResponseInitUuid = entity.ResponseInitUuid;
                                    responseinfo.State = 0;
                                    _dbContext.ResponseInfo.Add(responseinfo);
                                }
                                
                            }
                        }
                    }
                }

                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:响应发起记录信息一条数据", _dbContext);
                }
                response.SetSuccess();
                return Ok(response);
            }
        }




        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="guid">惟一编码</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.ResponseInit.FirstOrDefault(x => x.ResponseInitUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<ResponseInit, ResponseInitViewModel>(entity));
                return Ok(response);
            }
        }
        /// <summary>
        /// 保存编辑后的信息
        /// </summary>
        /// <param name="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(ResponseInitViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.ResponseInit.FirstOrDefault(x => x.ResponseInitUuid == model.ResponseInitUuid);
                if (entity == null)
                {
                    response.SetFailed("不存在");
                    return Ok(response);
                }
                //if (_dbContext.ResponseInit.Count(x => x.TeamName == model.TeamName && x.ResponseInitUuid != model.ResponseInitUuid) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}
                entity.Villages = model.Villages;
                entity.Level = model.Level;
                entity.Situation = model.Situation;
                //entity.ReleaseState = model.ReleaseState;

                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:响应发起记录信息一条数据", _dbContext);
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
            return Ok(response);
        }
        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
                    break;
                case "recover":
                    response = UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
                    break;

                default:
                    break;
            }
            return Ok(response);
        }
        #region 私有方法

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE ResponseInit SET IsDelete=@IsDel WHERE ResponseInitUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion

        #region 导入导出
        /// <summary>
        /// 导入信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ResponseInitImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportEmergencyExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = "响应发起信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sFileName = $"{uploadtitle}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                //string conStr = ConnectionStrings.DefaultConnection;
                string responsemsgsuccess = "";
                string responsemsgrepeat = "";
                string responsemsgdefault = "";
                int successcount = 0;
                int repeatcount = 0;
                int defaultcount = 0;
                //string today = DateTime.Now.ToString("yyyy-MM-dd");
                try
                {
                    //把excelfile中的数据复制到file中
                    using (FileStream fs = new FileStream(file.ToString(), FileMode.Create)) //初始化一个指定路径和创建模式的FileStream
                    {
                        excelfile.CopyTo(fs);
                        fs.Flush();  //清空stream的缓存，并且把缓存中的数据输出到file
                    }
                    DataTable dt = Haikan3.Utils.ExcelTools.ExcelToDataTable(file.ToString(), "Sheet1", true);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        response.SetFailed("表格无数据");
                        return Ok(response);
                    }
                    else
                    {
                        if (!dt.Columns.Contains("通知村庄"))
                        {
                            response.SetFailed("无‘通知村庄’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("等级"))
                        {
                            response.SetFailed("无‘等级’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("情况"))
                        {
                            response.SetFailed("无‘情况’列");
                            return Ok(response);
                        }
                        
                        if (!dt.Columns.Contains("状态"))
                        {
                            response.SetFailed("无‘状态’列");
                            return Ok(response);
                        }

                       
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new ResponseInit();
                            entity.ResponseInitUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["通知村庄"].ToString()))
                            {
                                var village = dt.Rows[i]["通知村庄"].ToString().Split("|");
                                var villageuuid="";
                                foreach(var item in village)
                                {
                                    var villagesingle = _dbContext.Village.FirstOrDefault(x=>x.VillageName==item);
                                    if(villagesingle!=null)
                                    {
                                        villageuuid += villagesingle.VillageUuid + ",";
                                    }
                                }
                                entity.Villages = villageuuid.Trim(',');
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行通知村庄为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["等级"].ToString()))
                            {
                                entity.Level = dt.Rows[i]["等级"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行等级为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["情况"].ToString()))
                            {
                                entity.Situation = dt.Rows[i]["情况"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行情况为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            
                            if (!string.IsNullOrEmpty(dt.Rows[i]["状态"].ToString()))
                            {
                                if(dt.Rows[i]["状态"].ToString()== "已发起")
                                    entity.ReleaseState = 1;
                                else
                                    entity.ReleaseState = 0;
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行状态为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                           

                            entity.IsDelete = 0;
                            entity.Addpeoople = AuthContextService.CurrentUser.DisplayName;

                            _dbContext.ResponseInit.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:响应发起记录信息数据", _dbContext);
                    //DateTime endTime = DateTime.Now;
                    //TimeSpan useTime = endTime - beginTime;
                    //string taketime = "导入时间" + useTime.TotalSeconds.ToString() + "秒  ";
                    response.SetData(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new
                    {
                        //time = taketime,
                        successmsg = responsemsgsuccess
                        ,
                        repeatmsg = responsemsgrepeat,
                        defaultmsg = responsemsgdefault
                    })));
                    return Ok(response);
                }
                catch (Exception ex)
                {

                    response.SetFailed(ex.Message);
                    return Ok(response);
                }
            }
        }

        /// <summary>
        /// 导出信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ResponseInitExport(string ids)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var column = new List<string>()
                    {
                        "通知村庄","等级","情况","状态"
                    };
                var column2 = new List<string>()
                    {
                        "VillagesName","Level","Situation","ReleaseState"
                    };
                if (!string.IsNullOrEmpty(ids))
                {
                    var parameters = ids.Trim().Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query = _dbContext.ResponseInit.Where(x => x.IsDelete != 1 && parameters.Contains(x.ResponseInitUuid.ToString())).Select(x => new
                    {
                        VillagesName="",
                        Villages = x.Villages,
                        Level = x.Level,
                        Situation = x.Situation,
                        ReleaseState = x.ReleaseState==1?"已发起":"未发起"
                    }).ToList();
                    List<ResponseInitExportViewModel> resinit = new List<ResponseInitExportViewModel>();
                    for (int i=0;i< query.Count;i++)
                    {
                        ResponseInitExportViewModel re = new ResponseInitExportViewModel();
                        
                        re.Level = query[i].Level;
                        re.Situation = query[i].Situation;
                        re.ReleaseState = query[i].ReleaseState;

                        if (query[i].Villages!=null)
                        { 
                            var village = query[i].Villages.Split(",");
                            var villagesname = "";
                            foreach (var item in village)
                            {
                                var villagesingle = _dbContext.Village.FirstOrDefault(x => x.VillageUuid.ToString() == item);
                                if (villagesingle != null)
                                {
                                    villagesname += villagesingle.VillageName + ",";
                                }
                            }
                            re.VillagesName = villagesname.Trim(',');
                        }
                        resinit.Add(re);
                    }

                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportEmergencyExcel\\";
                    string uploadtitle = "响应发起信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(resinit, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportEmergencyExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:响应发起记录信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {

                    var query = _dbContext.ResponseInit.Where(x => x.IsDelete != 1).Select(x => new
                    {
                        VillagesName = "",
                        Villages = x.Villages,
                        Level = x.Level,
                        Situation = x.Situation,
                        ReleaseState = x.ReleaseState == 1 ? "已通知" : "未通知"
                    }).ToList();
                    List<ResponseInitExportViewModel> resinit = new List<ResponseInitExportViewModel>();
                    for (int i = 0; i < query.Count; i++)
                    {
                        ResponseInitExportViewModel re = new ResponseInitExportViewModel();

                        re.Level = query[i].Level;
                        re.Situation = query[i].Situation;
                        re.ReleaseState = query[i].ReleaseState;

                        if (query[i].Villages != null)
                        {
                            var village = query[i].Villages.Split(",");
                            var villagesname = "";
                            foreach (var item in village)
                            {
                                var villagesingle = _dbContext.Village.FirstOrDefault(x => x.VillageUuid.ToString() == item);
                                if (villagesingle != null)
                                {
                                    villagesname += villagesingle.VillageName + ",";
                                }
                            }
                            re.VillagesName = villagesname.Trim(',');
                        }
                        resinit.Add(re);
                    }

                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportEmergencyExcel\\";
                    string uploadtitle = "响应发起信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    DataToExcel.TablesToExcel(resinit, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportEmergencyExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:响应发起记录信息数据", _dbContext);
                    return Ok(response);
                }
            }
        }
        #endregion
    }
}
