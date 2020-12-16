using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.Api.ViewModels.JingdianInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.JingdianInfo
{
    [Route("api/v1/JingdianInfo/[controller]/[action]")]
    [ApiController]
    public class lvyoutongjiController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        public lvyoutongjiController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        ///  列表显示
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetList(UserInfoRequestpayload payload)
        {
            List<GetYears> getYears = new List<GetYears>();
            var q1 = _dbContext.Tongji.ToList().GroupBy(x => x.TjYear);
            foreach (var item in q1)
            {
                var query = from c in _dbContext.Tongji
                            where c.TjYear == item.Key
                            select new GetYears
                            {
                                gjYear = item.Key,
                                giOne = one(item.Key,"第一季度")+ "万",
                                gjTwo = one(item.Key, "第二季度") + "万",
                                gjThree = one(item.Key, "第三季度") + "万",
                                gjFour = one(item.Key, "第四季度") + "万",
                            };
                var entity = query.FirstOrDefault();
                getYears.Add(entity);
            }
            var getYe = getYears.OrderByDescending(x => x.gjYear);
            var list = getYe.AsQueryable().Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = getYe.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            return Ok(response);
            }

        public static float one(string db,string jb)
        {
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                //var num = cc.Tongji.FirstOrDefault(x => x.TjYear == db && x.TjJidu == jb);
                //double dd = 0;
                //if (num != null)
                //{
                //    dd = Math.Round((double)num.Num * 0.0001,5);
                //}
                //return dd;

                var num = cc.Tongji.FirstOrDefault(x => x.TjYear == db && x.TjJidu == jb);
                float dd = 0;
                if (num != null)
                {
                    int i = (int)(num.Num * 100);
                    dd = (float)(i * 1.0) / 1000000;
                }
                return dd;

            }
        }
        public static float ss(float xx)
        {
            int i = (int)(xx * 100);
            xx = (float)(i * 1.0) / 100;
            return xx;
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
                    List<GetYears> getYears = new List<GetYears>();
                    var q1 = _dbContext.Tongji.ToList().GroupBy(x => x.TjYear);
                    foreach (var item in q1)
                    {
                        var query3 = from c in _dbContext.Tongji
                                    where c.TjYear == item.Key
                                    select new GetYears
                                    {
                                        gjYear = item.Key,
                                        giOne = one(item.Key, "第一季度") + "万",
                                        gjTwo = one(item.Key, "第二季度") + "万",
                                        gjThree = one(item.Key, "第三季度") + "万",
                                        gjFour = one(item.Key, "第四季度") + "万",
                                    };
                        var entity = query3.FirstOrDefault();
                        getYears.Add(entity);
                    }
                //var getYe = getYears.OrderByDescending(x => x.gjYear);
                //var query = getYears.ToList();
                    var query = getYears.OrderByDescending(x => x.gjYear).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "旅游收入年度统计报表" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    return Ok(response);
            }

        }





        public static bool TablesToExcel(List<GetYears> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<GetYears> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "年份","第一季度","第二季度","第三季度","第四季度"
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
                dataRow.CreateCell(0).SetCellValue(row.gjYear);
                dataRow.CreateCell(1).SetCellValue(row.giOne);
                dataRow.CreateCell(2).SetCellValue(row.gjTwo);
                dataRow.CreateCell(3).SetCellValue(row.gjThree);
                dataRow.CreateCell(4).SetCellValue(row.gjFour);
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
    }
}
