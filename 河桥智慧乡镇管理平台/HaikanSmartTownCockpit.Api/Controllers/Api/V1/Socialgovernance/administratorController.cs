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
    public class administratorController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public administratorController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult List(administratorRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.Administrator
                            select new
                            {
                                p.AdministratorUuid,
                                p.AdministratorName,
                                p.Sex,
                                p.IdentityCard,
                                p.Phone,
                                p.AdminQuanxian,
                                p.AdminAddress,
                                p.AddTime,
                                p.IsDeleted,
                                p.Id,
                                p.AdminVillages,
                                p.GriddingNum,
                                p.SuozaiWangge,
                                p.CunjiZhanghao,
                                p.WanggeZhanghao,
                                p.Wanggeyuan,
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.AdministratorName.Contains(payload.Kw.Trim()));
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
                ToLog.AddLog("查询", "成功:查询:网格员信息数据", _dbContext);
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
        public IActionResult AdminfoGet(Guid guid)
        {
            using (_dbContext)
            {
                var query1 = from c in _dbContext.Administrator
                             where c.AdministratorUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.AdministratorName,
                                 c.Sex,
                                 c.IdentityCard,
                                 c.Phone,
                                 c.AdminQuanxian,
                                 c.AdminAddress,
                                 c.AddTime,
                                 c.AdministratorUuid,
                                 c.AdminVillages,
                                 c.GriddingNum,
                                 c.SuozaiWangge,
                                 c.CunjiZhanghao,
                                 c.WanggeZhanghao,
                                 c.Wanggeyuan,
                             };
                var query = query1.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(administratorViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.Administrator();
                entity.AdministratorUuid = Guid.NewGuid();
                entity.AdministratorName = model.AdministratorName;
                entity.IdentityCard = model.IdentityCard;
                entity.Phone = model.Phone;
                entity.AdminVillages = model.AdminVillages;
                entity.GriddingNum = model.GriddingNum;
                entity.SuozaiWangge = model.SuozaiWangge;
                entity.CunjiZhanghao = model.CunjiZhanghao;
                entity.WanggeZhanghao = model.WanggeZhanghao;
                entity.Wanggeyuan = model.Wanggeyuan;
                entity.IsDeleted = 0;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                _dbContext.Administrator.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:网格员信息一条数据", _dbContext);
                }
                response.SetSuccess("添加成功");
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
                var entity = _dbContext.Administrator.FirstOrDefault(x => x.AdministratorUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Administrator, administratorViewModel>(entity));
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
        public IActionResult Edit(administratorViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.Administrator.FirstOrDefault(x => x.AdministratorUuid.ToString() == model.AdministratorUuid);
                if (entity == null)
                {
                    response.SetFailed("不存在");
                    return Ok(response);
                }
                if (_dbContext.Administrator.Count(x => x.AdministratorName == model.AdministratorName && x.AdministratorUuid.ToString() != model.AdministratorUuid) > 0)
                {
                    response.SetFailed("网格员已存在");
                    return Ok(response);
                }
                entity.AdministratorName = model.AdministratorName;
                entity.IdentityCard = model.IdentityCard;
                entity.Phone = model.Phone;
                entity.AdminVillages = model.AdminVillages;
                entity.GriddingNum = model.GriddingNum;
                entity.SuozaiWangge = model.SuozaiWangge;
                entity.CunjiZhanghao = model.CunjiZhanghao;
                entity.WanggeZhanghao = model.WanggeZhanghao;
                entity.Wanggeyuan = model.Wanggeyuan;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:网格员信息一条数据", _dbContext);
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

                string uploadtitle = " 网格管理员信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.Administrator();
                            entity.AdministratorUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.AdministratorName = dt.Rows[i]["姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            Regex regsfz = new Regex("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$");
                            if (regsfz.IsMatch(dt.Rows[i]["身份证号"].ToString()))
                            {
                                entity.IdentityCard = dt.Rows[i]["身份证号"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证号格式不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["手机号码"].ToString()))
                            {
                                entity.Phone = dt.Rows[i]["手机号码"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行手机号码为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["行政村"].ToString()))
                            {
                                entity.AdminVillages = dt.Rows[i]["行政村"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["所在网格"].ToString()))
                            {
                                entity.SuozaiWangge = dt.Rows[i]["所在网格"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["网格账号"].ToString()))
                            {
                                entity.WanggeZhanghao = dt.Rows[i]["网格账号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["村级账号"].ToString()))
                            {
                                entity.CunjiZhanghao = dt.Rows[i]["村级账号"].ToString();
                            }
                            entity.IsDeleted = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            _dbContext.Administrator.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:网格员信息数据", _dbContext);
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
                    var query1 = _dbContext.Administrator.Where(x => x.IsDeleted != 1 && parameters.Contains(x.AdministratorUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.Administrator
                    {
                        Id = x.Id,
                        AdministratorName = x.AdministratorName,
                        Sex = x.Sex,
                        IdentityCard = x.IdentityCard,
                        Phone = x.Phone,
                        AdminQuanxian = x.AdminQuanxian,
                        AdminAddress = x.AdminAddress,
                        AdminVillages = x.AdminVillages,
                        GriddingNum = x.GriddingNum,
                        SuozaiWangge = x.SuozaiWangge,
                        CunjiZhanghao = x.CunjiZhanghao,
                        WanggeZhanghao = x.WanggeZhanghao,
                        Wanggeyuan = x.Wanggeyuan,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportSocialgoverExcel\\";
                    string uploadtitle = "网格管理员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportSocialgoverExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:网格员信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.Administrator.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.Administrator
                    {
                        Id = x.Id,
                        AdministratorName = x.AdministratorName,
                        Sex = x.Sex,
                        IdentityCard = x.IdentityCard,
                        Phone = x.Phone,
                        AdminQuanxian = x.AdminQuanxian,
                        AdminAddress = x.AdminAddress,
                        AdminVillages = x.AdminVillages,
                        GriddingNum = x.GriddingNum,
                        SuozaiWangge = x.SuozaiWangge,
                        CunjiZhanghao = x.CunjiZhanghao,
                        WanggeZhanghao = x.WanggeZhanghao,
                        Wanggeyuan = x.Wanggeyuan,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportSocialgoverExcel\\";
                    string uploadtitle = "网格管理员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportSocialgoverExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:网格员信息数据", _dbContext);
                    return Ok(response);
                }
            }
        }

        private bool TablesToExcel(List<Administrator> query, string filename)
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

        private void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.Administrator> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "姓名","身份证号","手机号码","行政村","所在网格","网格账号","村级账号"
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
                dataRow.CreateCell(0).SetCellValue(row.AdministratorName);
                dataRow.CreateCell(1).SetCellValue(row.IdentityCard);
                dataRow.CreateCell(2).SetCellValue(row.Phone);
                dataRow.CreateCell(3).SetCellValue(row.AdminVillages);
                dataRow.CreateCell(4).SetCellValue(row.SuozaiWangge);
                dataRow.CreateCell(5).SetCellValue(row.WanggeZhanghao);
                dataRow.CreateCell(6).SetCellValue(row.CunjiZhanghao);
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
                var sql = string.Format("UPDATE Administrator SET IsDeleted=@IsDel WHERE AdministratorUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion

    }
}
