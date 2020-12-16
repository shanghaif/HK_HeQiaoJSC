using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Environmental;
using HaikanSmartTownCockpit.Api.ViewModels.Environmental;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Environmental
{
    [Route("api/v1/Environmental/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class farmHouseController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public farmHouseController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult List(farmHouseRequestpayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.BuildHouse.Where(x=>1==1);
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Household.Contains(payload.Kw.Trim()));
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
                ToLog.AddLog("查询", "成功:查询:建房监控信息数据", _dbContext);
                return Ok(response);
            }
        }

        [HttpGet]
        public IActionResult TownList()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                var query = _dbContext.Town.Where(x => x.IsDeleted == 0).ToList();
                response.SetData(query);
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
        public IActionResult Create(farmHouseViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                //if (_dbContext.BuildHouse.Count(x => x.Household == model.Household) > 0)
                //{
                //    response.SetFailed("户主已存在");
                //    return Ok(response);
                //}

                var entity = _mapper.Map<farmHouseViewModel, BuildHouse>(model);
                entity.BuildHouseUuid = Guid.NewGuid();
                entity.Town = model.Town;
                entity.Household = model.Household;
                entity.HouseAddress = model.HouseAddress;
                entity.Lon = model.Lon;
                entity.Lat = model.Lat;
                entity.IdentityCard = model.IdentityCard;
                entity.Phone = model.Phone;
                entity.MonitorHouseId = model.MonitorHouseId;
                entity.AdministratorUuid = model.AdministratorUuid;
                entity.ProjectCred = model.ProjectCred;
                entity.LandNum = model.LandNum;
                entity.BuildArea = model.BuildArea;
                entity.OccupyArea = model.OccupyArea;
                entity.Way = model.Way;
                entity.ArtisanCred = model.ArtisanCred;
                entity.ApproveTime = model.ApproveTime;
                entity.IsDeleted = 0;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;

                _dbContext.BuildHouse.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:建房监控信息一条数据", _dbContext);
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
                var entity = _dbContext.BuildHouse.FirstOrDefault(x => x.BuildHouseUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<BuildHouse, farmHouseViewModel>(entity));
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
        public IActionResult Edit(farmHouseViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.BuildHouse.FirstOrDefault(x => x.BuildHouseUuid == model.BuildHouseUuid);
                if (entity == null)
                {
                    response.SetFailed("不存在");
                    return Ok(response);
                }
                //if (_dbContext.BuildHouse.Count(x => x.Household == model.Household && x.BuildHouseUuid != model.BuildHouseUuid) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}
                entity.Town = model.Town;
                entity.Household = model.Household;
                entity.HouseAddress = model.HouseAddress;
                entity.Lon = model.Lon;
                entity.Lat = model.Lat;
                entity.IdentityCard = model.IdentityCard;
                entity.Phone = model.Phone;
                entity.MonitorHouseId = model.MonitorHouseId;
                entity.AdministratorUuid = model.AdministratorUuid;
                entity.ProjectCred = model.ProjectCred;
                entity.LandNum = model.LandNum;
                entity.BuildArea = model.BuildArea;
                entity.OccupyArea = model.OccupyArea;
                entity.Way = model.Way;
                entity.ArtisanCred = model.ArtisanCred;
                entity.ApproveTime = model.ApproveTime;

                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:建房监控信息一条数据", _dbContext);
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }
        #endregion

        #region 导入信息
        /// <summary>
        /// 导入信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult FarmHouseImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel";

                string uploadtitle = " 农民建房信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        if (!dt.Columns.Contains("姓名"))
                        {
                            response.SetFailed("无‘姓名’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("行政村"))
                        {
                            response.SetFailed("无‘行政村’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.BuildHouse();
                            entity.BuildHouseUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.Household = dt.Rows[i]["姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["行政村"].ToString()))
                            {
                                var query1 = _dbContext.Town.FirstOrDefault(x=>x.TownName== dt.Rows[i]["行政村"].ToString());
                                if (query1!=null)
                                {
                                    entity.Town = dt.Rows[i]["行政村"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行行政村不存在" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行行政村为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["联系号码"].ToString()))
                            {
                                entity.Phone = dt.Rows[i]["联系号码"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行联系号码为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["身份证号"].ToString()))
                            {
                                entity.IdentityCard = dt.Rows[i]["身份证号"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证号为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["规划许可证号"].ToString()))
                            {
                                entity.ProjectCred = dt.Rows[i]["规划许可证号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["用地呈报表编号"].ToString()))
                            {
                                entity.LandNum = dt.Rows[i]["用地呈报表编号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["建筑面积"].ToString()))
                            {
                                entity.BuildArea = dt.Rows[i]["建筑面积"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["占地面积"].ToString()))
                            {
                                entity.OccupyArea = dt.Rows[i]["占地面积"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["方式"].ToString()))
                            {
                                entity.Way = dt.Rows[i]["方式"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["工匠证书"].ToString()))
                            {
                                entity.ArtisanCred = dt.Rows[i]["工匠证书"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["审批时间"].ToString()))
                            {
                                entity.ApproveTime = dt.Rows[i]["审批时间"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["位置"].ToString()))
                            {
                                entity.HouseAddress = dt.Rows[i]["位置"].ToString();
                            }
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行位置为空" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["经度"].ToString()))
                            {
                                entity.Lon = dt.Rows[i]["经度"].ToString();
                            }
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行经度为空" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["纬度"].ToString()))
                            {
                                entity.Lat = dt.Rows[i]["纬度"].ToString();
                            }
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行纬度为空" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["监控编号"].ToString()))
                            {
                                entity.MonitorHouseId = dt.Rows[i]["监控编号"].ToString();
                            }
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行监控编号为空" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["网格员"].ToString()))
                            {
                                entity.AdministratorUuid = dt.Rows[i]["网格员"].ToString();
                            }
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行网络员为空" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}


                            entity.IsDeleted = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            _dbContext.BuildHouse.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:建房监控信息数据", _dbContext);
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
        #endregion

        #region 导出
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
                    var query1 = _dbContext.BuildHouse.Where(x => x.IsDeleted != 1 && parameters.Contains(x.BuildHouseUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.BuildHouse
                    {
                        Id = x.Id,
                        Household = x.Household,
                        HouseAddress = x.HouseAddress,
                        Lon = x.Lon,
                        Lat = x.Lat,
                        IdentityCard = x.IdentityCard,
                        Phone = x.Phone,
                        MonitorHouseId = x.MonitorHouseId,
                        AdministratorUuid = x.AdministratorUuid,
                        Town =x.Town,
                        ProjectCred=x.ProjectCred,
                        LandNum=x.LandNum,
                        BuildArea=x.BuildArea,
                        OccupyArea=x.OccupyArea,
                        Way=x.Way,
                        ArtisanCred=x.ArtisanCred,
                        ApproveTime=x.ApproveTime

                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "农民建房信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:建房监控信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.BuildHouse.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.BuildHouse
                    {
                        Id = x.Id,
                        Household = x.Household,
                        HouseAddress = x.HouseAddress,
                        Lon = x.Lon,
                        Lat = x.Lat,
                        IdentityCard = x.IdentityCard,
                        Phone = x.Phone,
                        MonitorHouseId = x.MonitorHouseId,
                        AdministratorUuid = x.AdministratorUuid,
                        Town = x.Town,
                        ProjectCred = x.ProjectCred,
                        LandNum = x.LandNum,
                        BuildArea = x.BuildArea,
                        OccupyArea = x.OccupyArea,
                        Way = x.Way,
                        ArtisanCred = x.ArtisanCred,
                        ApproveTime = x.ApproveTime
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "农民建房信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:建房监控信息数据", _dbContext);
                    return Ok(response);
                }
            }
        }

        private bool TablesToExcel(List<BuildHouse> query, string filename)
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

        private void CreatSheet(ISheet sheet, List<BuildHouse> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "行政村","姓名","联系电话","身份证号码","规划许可证号","用地呈报表编号","建筑面积","占地面积","方式","工匠证书","审批时间","经度","纬度","位置","监控编号","网格员"
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
                dataRow.CreateCell(0).SetCellValue(row.Town);
                dataRow.CreateCell(1).SetCellValue(row.Household);
                dataRow.CreateCell(2).SetCellValue(row.Phone);
                dataRow.CreateCell(3).SetCellValue(row.IdentityCard);
                dataRow.CreateCell(4).SetCellValue(row.ProjectCred);
                dataRow.CreateCell(5).SetCellValue(row.LandNum);
                dataRow.CreateCell(6).SetCellValue(row.BuildArea);
                dataRow.CreateCell(7).SetCellValue(row.OccupyArea);
                dataRow.CreateCell(8).SetCellValue(row.Way);
                dataRow.CreateCell(9).SetCellValue(row.ArtisanCred);
                dataRow.CreateCell(10).SetCellValue(row.ApproveTime);
                dataRow.CreateCell(11).SetCellValue(row.Lon);
                dataRow.CreateCell(12).SetCellValue(row.Lat);
                dataRow.CreateCell(13).SetCellValue(row.HouseAddress);
                dataRow.CreateCell(14).SetCellValue(row.MonitorHouseId);
                dataRow.CreateCell(15).SetCellValue(row.AdministratorUuid);
                rowIndex++;
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
                var sql = string.Format("UPDATE BuildHouse SET IsDeleted=@IsDel WHERE BuildHouseUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:建房监控信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion
    }
}
