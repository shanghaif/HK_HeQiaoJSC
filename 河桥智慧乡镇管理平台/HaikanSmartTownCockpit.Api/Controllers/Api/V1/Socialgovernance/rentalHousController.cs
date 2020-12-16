using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Socialgovernance;
using HaikanSmartTownCockpit.Api.ViewModels.Socialgovernance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Socialgovernance
{
    [Route("api/v1/Socialgovernance/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class rentalHousController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public rentalHousController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult List(rentalHousRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.RentoutRoom
                            select new
                            {
                                p.RentoutRoomUuid,
                                p.RentoutInfo,
                                p.RentoutYezhu,
                                p.RentoutZuhu,
                                p.RentoutTime,
                                p.DaoqiTime,
                                p.RentoutMoney,
                                p.RentoutStaues,
                                p.AddTime,
                                p.IsDeleted,
                                p.Id
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.RentoutInfo.Contains(payload.Kw.Trim()));
                }

                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == Convert.ToInt32(((CommonEnum.IsDeleted)payload.IsDeleted)));
                }

                query = query.OrderByDescending(x => x.Id);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:出租房信息数据", _dbContext);
                return Ok(response);
            }
        }
        #endregion

        /// <summary>
        /// 获取当前行信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult rentalHousfoGet(Guid guid)
        {
            using (_dbContext)
            {
                var query1 = from c in _dbContext.RentoutRoom
                             where c.RentoutRoomUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.RentoutInfo,
                                 c.RentoutYezhu,
                                 c.RentoutZuhu,
                                 c.RentoutTime,
                                 c.DaoqiTime,
                                 c.RentoutMoney,
                                 c.RentoutStaues,
                                 c.AddTime,
                             };
                var query = query1.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        #region 创建
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(rentalHousViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.RentoutRoom();
                entity.RentoutRoomUuid = Guid.NewGuid();
                entity.RentoutInfo = model.RentoutInfo;
                entity.RentoutYezhu = model.RentoutYezhu;
                entity.RentoutZuhu = model.RentoutZuhu;
                entity.RentoutMoney = model.RentoutMoney;
                entity.RentoutStaues = model.RentoutStaues;
                if (model.RentoutTime != "")
                {
                    entity.RentoutTime = DateTime.Parse(model.RentoutTime).ToString("yyyy-MM-dd");
                }
                if (model.DaoqiTime != "")
                {
                    entity.DaoqiTime = DateTime.Parse(model.DaoqiTime).ToString("yyyy-MM-dd");
                }
                entity.IsDeleted = 0;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                _dbContext.RentoutRoom.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加::出租房信息一条数据", _dbContext);
                }
                response.SetSuccess();
                return Ok(response);
            }
        }
        #endregion

        #region 编辑
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
                var entity = _dbContext.RentoutRoom.FirstOrDefault(x => x.RentoutRoomUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<RentoutRoom, rentalHousViewModel>(entity));
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
        public IActionResult Edit(rentalHousViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = _dbContext.RentoutRoom.FirstOrDefault(x => x.RentoutRoomUuid == model.RentoutRoomUuid);
                entity.RentoutInfo = model.RentoutInfo;
                entity.RentoutYezhu = model.RentoutYezhu;
                entity.RentoutZuhu = model.RentoutZuhu;
                entity.RentoutMoney = model.RentoutMoney;
                entity.RentoutStaues = model.RentoutStaues;
                if (model.RentoutTime != null)
                {
                    entity.RentoutTime = DateTime.Parse(model.RentoutTime).ToString("yyyy-MM-dd");
                }
                if (model.DaoqiTime != null)
                {
                    entity.DaoqiTime = DateTime.Parse(model.DaoqiTime).ToString("yyyy-MM-dd");
                }
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑::出租房信息一条数据", _dbContext);
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }
        #endregion

        /// <summary>
        /// 导入信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult HqCommunaImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportSocialgoverExcel";

                string uploadtitle = " 出租房信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sFileName = $"{uploadtitle}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                string responsemsgsuccess = "";
                string responsemsgrepeat = "";
                string responsemsgdefault = "";
                int successcount = 0;
                int repeatcount = 0;
                int defaultcount = 0;
                string today = DateTime.Now.ToString("yyyy-MM-dd");
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
                        if (!dt.Columns.Contains("出租房信息"))
                        {
                            response.SetFailed("无‘出租房信息’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.RentoutRoom();
                            entity.RentoutRoomUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["出租房信息"].ToString()))
                            {
                                entity.RentoutInfo = dt.Rows[i]["出租房信息"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行出租房信息为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["业主"].ToString()))
                            {
                                entity.RentoutYezhu = dt.Rows[i]["业主"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行业主为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["租户"].ToString()))
                            {
                                entity.RentoutZuhu = dt.Rows[i]["租户"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行租户为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            Regex regbr = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                            if (regbr.IsMatch(dt.Rows[i]["租房时间"].ToString()))
                            { 
                                entity.RentoutTime = dt.Rows[i]["租房时间"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行租房时间为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (regbr.IsMatch(dt.Rows[i]["到期时间"].ToString()))
                            {
                                
                                entity.DaoqiTime = dt.Rows[i]["到期时间"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行到期时间为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["价格"].ToString()))
                            {
                                entity.RentoutMoney = dt.Rows[i]["价格"].ToString();
                            }
                            Regex regxb = new Regex("^(已出租|闲置中|待出租)$");
                            if (regxb.IsMatch(dt.Rows[i]["出租状态"].ToString()))
                            {
                                entity.RentoutStaues = dt.Rows[i]["出租状态"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行出租状态不正确,应为:已出租|闲置中|待出租" + "</p></br>";
                                defaultcount++;
                                continue;
                            }

                                entity.IsDeleted = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            _dbContext.RentoutRoom.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入::出租房信息数据", _dbContext);
                    DateTime endTime = DateTime.Now;
                    TimeSpan useTime = endTime - beginTime;
                    string taketime = "导入时间" + useTime.TotalSeconds.ToString() + "秒  ";
                    response.SetData(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new
                    {
                        time = taketime,
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
        public IActionResult ExportPass(string ids)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                if (ids != null)
                {
                    var parameters = ids.Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query1 = _dbContext.RentoutRoom.Where(x => x.IsDeleted != 1 && parameters.Contains(x.RentoutRoomUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.RentoutRoom
                    {
                        Id = x.Id,
                        RentoutInfo = x.RentoutInfo,
                        RentoutYezhu = x.RentoutYezhu,
                        RentoutZuhu = x.RentoutZuhu,
                        RentoutTime = x.RentoutTime,
                        DaoqiTime = x.DaoqiTime,
                        RentoutMoney = x.RentoutMoney,
                        RentoutStaues = x.RentoutStaues,
                        AddTime = x.AddTime,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportSocialgoverExcel\\";
                    string uploadtitle = "出租房信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportSocialgoverExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出::出租房信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.RentoutRoom.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.RentoutRoom
                    {
                        Id = x.Id,
                        RentoutInfo = x.RentoutInfo,
                        RentoutYezhu = x.RentoutYezhu,
                        RentoutZuhu = x.RentoutZuhu,
                        RentoutTime = x.RentoutTime,
                        DaoqiTime = x.DaoqiTime,
                        RentoutMoney = x.RentoutMoney,
                        RentoutStaues = x.RentoutStaues,
                        AddTime = x.AddTime,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportSocialgoverExcel\\";
                    string uploadtitle = "出租房信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportSocialgoverExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出::出租房信息数据", _dbContext);
                    return Ok(response);
                }
            }
        }

        private bool TablesToExcel(List<RentoutRoom> query, string filename)
        {
            MemoryStream ms = new MemoryStream();

            IWorkbook workBook;
            string suffix = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - filename.LastIndexOf(".") - 1);
            if (suffix == "xls")
            {
                workBook = new HSSFWorkbook();
            }
            else
                workBook = new XSSFWorkbook();

            ISheet sheet = workBook.CreateSheet(" 表");

            CreatSheet(sheet, query);


            workBook.Write(ms);
            try
            {
                SaveToFile(ms, filename);
                ms.Flush();
                return true;
            }
            catch
            {
                ms.Flush();
                throw;
            }
        }

        private void SaveToFile(MemoryStream ms, string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();         //转为字节数组 
                fs.Write(data, 0, data.Length);     //保存为Excel文件
                fs.Flush();
                data = null;
            }
        }

        private void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.RentoutRoom> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "出租房信息","业主","租户","租房时间","到期时间","价格","出租状态","注册时间"
            };

            //表头
            for (int i = 0; i < list.Count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(list[i]);
            }

            int rowIndex = 1;
            foreach (var row in query)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(row.RentoutInfo);
                dataRow.CreateCell(1).SetCellValue(row.RentoutYezhu);
                dataRow.CreateCell(2).SetCellValue(row.RentoutZuhu);
                dataRow.CreateCell(3).SetCellValue(row.RentoutTime);
                dataRow.CreateCell(4).SetCellValue(row.DaoqiTime);
                dataRow.CreateCell(5).SetCellValue(row.RentoutMoney);
                dataRow.CreateCell(6).SetCellValue(row.RentoutStaues);
                dataRow.CreateCell(7).SetCellValue(row.AddTime);
                rowIndex++;
            }
        }

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
                var sql = string.Format("UPDATE RentoutRoom SET IsDeleted=@IsDel WHERE RentoutRoomUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除::出租房信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion
    }
}
