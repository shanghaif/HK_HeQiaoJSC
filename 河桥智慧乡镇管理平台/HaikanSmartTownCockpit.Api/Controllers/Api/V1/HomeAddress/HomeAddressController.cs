using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Models.Response;
using System.IO;
using System.Data;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using HaikanSmartTownCockpit.Api.ViewModels.HomeAddress;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.HomeAddress
{
    [Route("api/v1/HomeAddress/[controller]/[action]")]
    [ApiController]
    public class HomeAddressController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public HomeAddressController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        ///  列表显示
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult HomeAddressList(UserInfoRequestpayload payload)
        {
            var query = from c in _dbContext.HomeAddress
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.Address,          //地址
                            c.Addresscode,     //地址编码
                            c.Village,      //自然村
                            c.Town,         //街道
                            c.Door,         //门牌
                            c.Unit,         //单元
                            c.Resregion,       //小区
                            c.HomeAddressUuid,
                        };

            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.Address.Contains(payload.Kw.Trim()));
            }

            if (!string.IsNullOrEmpty(payload.Kw1))
            {
                query = query.Where(x => x.Addresscode.Contains(payload.Kw1.Trim()));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:地址库信息数据", _dbContext);
            return Ok(response);
        }


        /// <summary>
        /// 获取当前行信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HomeAddressfoGet(Guid guid)
        {
            using (_dbContext)
            {
                var query1 = from c in _dbContext.HomeAddress
                             where c.HomeAddressUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.Address,          //地址
                                 c.Addresscode,     //地址编码
                                 c.Village,      //自然村
                                 c.Town,         //街道
                                 c.Door,         //门牌
                                 c.Unit,         //单元
                                 c.Resregion,       //小区
                                 c.HomeAddressUuid,
                             };
                var query = query1.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult HomeAddressCreate(HomeAddressViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.HomeAddress();
                entity.HomeAddressUuid = Guid.NewGuid();
                entity.Address = model.Address;
                entity.Addresscode = model.Addresscode;
                entity.Village = model.Village;
                entity.Town = model.Town;
                entity.Door = model.Door;
                entity.Unit = model.Unit;
                entity.Resregion = model.Resregion;
                //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                //entity.AddPeople = model.addPeople;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                entity.IsDeleted = 0;
                _dbContext.HomeAddress.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:地址库信息一条数据", _dbContext);
                }
                response.SetSuccess("添加成功");
                return Ok(response);
            }
        }




        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult HomeAddressEdit(HomeAddressViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            string guid = model.HomeAddressUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.HomeAddress.FirstOrDefault(x => x.HomeAddressUuid == Guid.Parse(guid));
                entity.Address = model.Address;
                entity.Addresscode = model.Addresscode;
                entity.Village = model.Village;
                entity.Town = model.Town;
                entity.Door = model.Door;
                entity.Unit = model.Unit;
                entity.Resregion = model.Resregion;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:地址库信息一条数据", _dbContext);
                }
                response.SetSuccess("修改成功");
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除单个角色
        /// </summary>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
            return Ok(response);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE HomeAddress SET IsDeleted=@isDeleted WHERE HomeAddressUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:地址库信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        /// <summary>
        /// 删除多条批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    if (ConfigurationManager.AppSettings.IsTrialVersion)
                    {
                        response.SetIsTrial();
                        return Ok(response);
                    }
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

        /// <summary>
        /// 导入信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult HomeAddressImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = " 地址库信息信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sFileName = $"{uploadtitle}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                //string conStr = ConnectionStrings.DefaultConnection;
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
                        if (!dt.Columns.Contains("地址"))
                        {
                            response.SetFailed("无‘地址’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var entity = new HaikanSmartTownCockpit.Api.Entities.HomeAddress();
                            entity.HomeAddressUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["地址"].ToString()))
                            {
                                entity.Address = dt.Rows[i]["地址"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行地址为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["地址编码"].ToString()))
                            {
                                entity.Addresscode = dt.Rows[i]["地址编码"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["自然村"].ToString()))
                            {
                                entity.Village = dt.Rows[i]["自然村"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["街道"].ToString()))
                            {
                                entity.Town = dt.Rows[i]["街道"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["门牌"].ToString()))
                            {
                                entity.Door = dt.Rows[i]["门牌"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["单元"].ToString()))
                            {
                                entity.Unit = dt.Rows[i]["单元"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["小区"].ToString()))
                            {
                                entity.Resregion = dt.Rows[i]["小区"].ToString();
                            }
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行价格为空" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            //entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.HomeAddress.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:地址库信息数据", _dbContext);
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
                    var query1 = _dbContext.HomeAddress.Where(x => x.IsDeleted != 1 && parameters.Contains(x.HomeAddressUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.HomeAddress
                    {
                        Id = x.Id,
                        Address =  x.Address,          //地址
                        Addresscode =x.Addresscode,     //地址编码
                        Village = x.Village,      //自然村
                        Town = x.Town,         //街道
                        Door = x.Door,         //门牌
                        Unit = x.Unit,         //单元
                        Resregion = x.Resregion,       //小区
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "地址库信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:地址库信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.HomeAddress.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.HomeAddress
                    {
                        Id = x.Id,
                        Address = x.Address,          //地址
                        Addresscode = x.Addresscode,     //地址编码
                        Village = x.Village,      //自然村
                        Town = x.Town,         //街道
                        Door = x.Door,         //门牌
                        Unit = x.Unit,         //单元
                        Resregion = x.Resregion,       //小区
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "地址库信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:地址库信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.HomeAddress> query, string filename)
        {
            MemoryStream ms = new MemoryStream();

            IWorkbook workBook;
            //IWorkbook workBook=WorkbookFactory.Create(filename);
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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.HomeAddress> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "地址","地址编码","自然村","街道","门牌","单元","小区"
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
                dataRow.CreateCell(0).SetCellValue(row.Address);
                dataRow.CreateCell(1).SetCellValue(row.Addresscode);
                dataRow.CreateCell(2).SetCellValue(row.Village);
                dataRow.CreateCell(3).SetCellValue(row.Town);
                dataRow.CreateCell(4).SetCellValue(row.Door);
                dataRow.CreateCell(5).SetCellValue(row.Unit);
                dataRow.CreateCell(6).SetCellValue(row.Resregion);
                rowIndex++;
            }
        }
        private static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();         //转为字节数组 
                fs.Write(data, 0, data.Length);     //保存为Excel文件
                fs.Flush();
                data = null;
            }
        }

        [HttpPost]
        public IActionResult backtoken() {
            var response = ResponseModelFactory.CreateResultInstance;
            var code = "0";
            try
            {
                var timeStamp = GetTimeStamp();
                var timepass = timeStamp.ToString() + "123";
                string valPassword = GetMD5Hash(timepass);
                string serviceUrl = "http://10.33.188.56:8090/addrMatch/auth/getToken?" + "user=abc&time=" + timeStamp + "&secret=" + valPassword;
                //response.SetData(serviceUrl);
                //return Ok(response);
                //创建Web访问对象
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
                //把用户传过来的数据转成“UTF-8”的字节流
                //byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

                //myRequest.Method = "GET";
                //myRequest.ContentLength = buf.Length;
                myRequest.ContentType = "application/json";
                //myRequest.MaximumAutomaticRedirections = 1;
                //myRequest.AllowAutoRedirect = true;
                //发送请求
                //Stream stream = myRequest.GetRequestStream();
                //stream.Write(buf, 0, buf.Length);
                //stream.Close();

                //获取接口返回值
                //通过Web访问对象获取响应内容
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
                string returnJson = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                code = myResponse.StatusCode.ToString();
                reader.Close();
                myResponse.Close();
                var datatoken = JsonConvert.DeserializeObject<ViewModels.ToTkenInfo.Token>(returnJson);
                string token = "";
                if (datatoken != null)
                {
                    token = datatoken.data.token;
                    using (_dbContext)
                    {

                        var sql = string.Format("TRUNCATE TABLE HomeAddress");
                        _dbContext.Database.ExecuteSqlRaw(sql);
                        var sno = Guid.NewGuid();
                        List<HaikanSmartTownCockpit.Api.Entities.HomeAddress> addresses = new List<HaikanSmartTownCockpit.Api.Entities.HomeAddress>();
                        string infoUrl = "http://10.33.188.56:8090/addrMatch/addrApi/searchAddr?token=" + token + "&addr=河桥&page=100&limit=9999&fuzzy=false&sno" + sno;
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(infoUrl);
                        myRequest1.ContentType = "application/json";
                        HttpWebResponse myResponse1 = (HttpWebResponse)myRequest1.GetResponse();
                        StreamReader reader1 = new StreamReader(myResponse1.GetResponseStream(), Encoding.UTF8);
                        string returnJson1 = reader1.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                        code = myResponse1.StatusCode.ToString();
                        reader1.Close();
                        myResponse1.Close();
                        var data1 = JsonConvert.DeserializeObject<ViewModels.AddresInfo.AddressInfo>(returnJson1);
                        //response.SetData(data1);
                        //return Ok(response);
                        for (int j = 0; j < data1.data.addrList.Count; j++)
                        {
                            //var entity = new HaikanSmartTownCockpit.Api.Entities.HomeAddress();
                            HaikanSmartTownCockpit.Api.Entities.HomeAddress entity = new HaikanSmartTownCockpit.Api.Entities.HomeAddress();
                            entity.Address = data1.data.addrList[j].addr;
                            entity.Addresscode = data1.data.addrList[j].code;
                            entity.Town = data1.data.addrList[j].town;
                            entity.Ccmmunity = data1.data.addrList[j].community;
                            entity.Squad = data1.data.addrList[j].squad;
                            entity.Village = data1.data.addrList[j].village;
                            entity.Szone = data1.data.addrList[j].szone;
                            entity.Street = data1.data.addrList[j].street;
                            entity.Door = data1.data.addrList[j].door;
                            entity.Resregion = data1.data.addrList[j].resregion;
                            entity.Building = data1.data.addrList[j].building;
                            entity.BuildingNum = data1.data.addrList[j].building_num;
                            entity.Unit = data1.data.addrList[j].unit;
                            entity.Floor = data1.data.addrList[j].floor;
                            //entity.Room = data1.data.addrList[j].room;
                            //entity.RoomFloor = data1.data.addrList[j].room_floor;
                            entity.HomeAddressUuid = Guid.NewGuid();
                            entity.IsDeleted = 0;
                            addresses.Add(entity);
                        }
                        _dbContext.AddRange(addresses);
                        _dbContext.SaveChanges();
                    }

                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetFailed("获取失败");
                response.SetData(ex.Message);
                return Ok(response);
            }
        }






        /// <summary>
        /// 使用 MD5  对字符串  进行加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 
            public static string GetMD5Hash(string str)
        {
            StringBuilder result = new StringBuilder();
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {

                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                int length = data.Length;
                for (int i = 0; i < length; i++)
                    result.Append(data[i].ToString("x2"));

            }
            return result.ToString();
        }

        //验证
        public static bool VerifyMD5Hash(string str, string hash)
        {
            string hashOfInput = GetMD5Hash(str);

            if (hashOfInput.CompareTo(hash) == 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

    }
}
