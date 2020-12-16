using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.statistics
{
    [Route("api/v1/statistics/[controller]/[action]")]
    [ApiController]
    public class statisticsController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        public statisticsController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 查询统计数据
        /// </summary>
        /// <param name="userid">用户uuid</param>
        /// <returns></returns>
        [HttpGet("{userid}")]
        public IActionResult selectcount(string userid)
        {
            using (_dbContext)
            {
                int allcount = 0;//所有任务
                int sjjbcount = 0;//上级交办
                int xjsccount = 0;//下级上传
                int ywccount = 0;//已完成的任务
                int blzcount = 0;//办理中的任务
                int jrdqcount = 0;//今日到期任务
                int yyqcount = 0;//已逾期任务
                int yqwccount = 0;//逾期完成任务数量
                int putong = 0;
                int jinji = 0;

                var styuerid = 0;

                //获取当前登录用户id
                if (!string.IsNullOrEmpty(userid))
                {
                    try
                    {
                        var styuer = _dbContext.SystemUser.Where(x => x.SystemUserUuid == Guid.Parse(userid)).FirstOrDefault();
                        if (styuer == null)
                        {

                        }
                        else
                        {
                            styuerid = styuer.Id;
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }


                var query = _dbContext.Mission.Where(x => x.Recycle == "0" && x.Preserve == "1").ToList();//查询所有任务

                allcount = query.Count();//所有任务
                sjjbcount = query.Count(x => x.Missiontype == "0");//上级交办
                xjsccount = query.Count(x => x.Missiontype == "1");//下级上传
                ywccount = query.Count(x => x.Accomplish == "1" && x.Isouttime == "0");//已完成
                blzcount = query.Count(x => x.AuditStatus == "0" || x.AuditStatus == "1");//办理中
                var nowdate = DateTime.Now.ToString("yyyy-MM-dd");//获取当前日期
                jrdqcount = query.Count(x => x.FinishTime.Contains(nowdate));//今日到期任务
                yyqcount = query.Count(x => x.Isouttime == "1");//已逾期任务


                yqwccount = query.Count(x => x.Accomplish == "1" && Convert.ToDateTime(x.Sortord) > Convert.ToDateTime(x.FinishTime));//逾期完成

                putong = query.Count(x => x.Priority == "普通");//优先级为普通的任务数量
                jinji = query.Count(x => x.Priority == "紧急");//优先级为紧急的任务数量


                //定义一个键值对集合保存各类型任务分布情况
                Dictionary<string, int> taskcount = new Dictionary<string, int>();
                taskcount.Add("allcount", allcount);//所有任务
                taskcount.Add("sjjbcount", sjjbcount);//上级交办
                taskcount.Add("xjsccount", xjsccount);//下级上传
                taskcount.Add("ywccount", ywccount);//已完成
                taskcount.Add("blzcount", blzcount);//办理中
                taskcount.Add("jrdqcount", jrdqcount);//今日到期
                taskcount.Add("yyqcount", yyqcount);//已逾期
                taskcount.Add("yqwccount", yqwccount);//逾期完成
                taskcount.Add("putong", putong);//普通任务
                taskcount.Add("jinji", jinji);//紧急任务
                return Ok(taskcount);
            }
        }

        /// <summary>
        /// 村庄任务排行统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectfuzepaihang()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                //默认查询当年的数据
                var nowdate = DateTime.Now.ToString("yyyy");
                var query = (from d in _dbContext.Town
                             where d.IsDeleted == 0
                             select new
                             {
                                 name = d.TownName,
                                 count = _dbContext.Mission.Count(x => x.Recycle == "0" && x.AdministrativeOffice == d.TownUuid.ToString() && x.EstablishTime.Contains(nowdate))
                             }).ToList();
                Dictionary<string, int> taskcount = new Dictionary<string, int>();
                for (int i = 0; i < query.Count; i++)
                {
                    taskcount.Add(query[i].name, query[i].count);
                }
                response.SetData(taskcount);
                return Ok(response);
            }
        }



        /// <summary>
        /// 村庄任务排行统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("{date}")]
        public IActionResult selectfuzepaihangd(string date)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                var query = (from d in _dbContext.Town
                             where d.IsDeleted == 0
                             select new
                             {
                                 name = d.TownName,
                                 count = _dbContext.Mission.Count(x => x.Recycle == "0" && x.AdministrativeOffice == d.TownUuid.ToString() && x.EstablishTime.Contains(date))
                             }).ToList();
                Dictionary<string, int> taskcount = new Dictionary<string, int>();
                for (int i = 0; i < query.Count; i++)
                {
                    taskcount.Add(query[i].name, query[i].count);
                }
                response.SetData(taskcount);
                return Ok(response);
            }
        }


        /// <summary>
        /// 科室任务排行统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("{date}")]
        public IActionResult selectcanyupaihang()
        {

            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                //默认查询当年的数据
                var nowdate = DateTime.Now.ToString("yyyy");
                var query = (from d in _dbContext.Department
                             where d.IsDeleted == 0
                             select new
                             {
                                 name = d.Name,
                                 count = _dbContext.Mission.Count(x => x.Recycle == "0" && x.AdministrativeOffice == d.DepartmentUuid.ToString() && x.EstablishTime.Contains(nowdate))
                             }).ToList();
                Dictionary<string, int> taskcount = new Dictionary<string, int>();
                for (int i = 0; i < query.Count; i++)
                {
                    taskcount.Add(query[i].name, query[i].count);
                }
                response.SetData(taskcount);
                return Ok(response);
            }



        }



        /// <summary>
        /// 科室任务排行统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("{date}")]
        public IActionResult selectcanyupaihangd(string date)
        {
            
                using (_dbContext)
                {
                    var response = ResponseModelFactory.CreateInstance;
                var query = (from d in _dbContext.Department
                                 where d.IsDeleted == 0
                                 select new
                                 {
                                     name = d.Name,
                                     count = _dbContext.Mission.Count(x => x.Recycle == "0" && x.AdministrativeOffice == d.DepartmentUuid.ToString() && x.EstablishTime.Contains(date))
                                 }).ToList();
                    Dictionary<string, int> taskcount = new Dictionary<string, int>();
                    for (int i = 0; i < query.Count; i++)
                    {
                        taskcount.Add(query[i].name.ToString().Replace("办公室",""), query[i].count);
                    }
                    response.SetData(taskcount);
                    return Ok(response);
                }
 


        }





        /// <summary> 
        /// 获取DataTable前几条数据 
        /// </summary> 
        /// <param name="TopItem">前N条数据</param> 
        /// <param name="oDT">源DataTable</param> 
        /// <returns></returns> 
        public static DataTable DtSelectTop(int TopItem, DataTable oDT)
        {
            if (oDT.Rows.Count < TopItem) return oDT;

            DataTable NewTable = oDT.Clone();
            DataRow[] rows = oDT.Select("1=1");
            for (int i = 0; i < TopItem; i++)
            {
                NewTable.ImportRow((DataRow)rows[i]);
            }
            return NewTable;
        }




        /// <summary>
        /// 负责人任务排行统计app
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectfuzepaihangapp()
        {
            using (_dbContext)
            {

                //var query = from m in _dbContext.Mission
                //            join u in _dbContext.SystemUser
                //            on m.Principal equals u.SystemUserUuid.ToString()
                //            where m.Recycle == "0"//没有在回收站
                //            where m.Preserve == "1"//没有在草稿箱中
                //            group m by new
                //            {//group by 分组
                //                m.Principal,
                //                u.RealName
                //            } into gp
                //            orderby gp.Count() descending //按负责的任务数量排序
                //            select new
                //            {
                //                gp.Key,
                //                counts = gp.Count()
                //            };
                var query = from u in _dbContext.SystemUser
                            select new
                            {
                                name = u.RealName,
                                //yuqi = _dbContext.Mission.Count(x => Convert.ToDateTime(x.FinishTime) < DateTime.Now && x.Accomplish == "0" && x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0" && x.Preserve == "1"),//已逾期
                                //wancheng = _dbContext.Mission.Count(x => x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Accomplish == "1" && x.Recycle == "0" && x.Preserve == "1"),//已完成
                                //jinxing = _dbContext.Mission.Count(x => x.AuditStatus == "0" && x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0" && x.Preserve == "1"),//进行中
                                sun = _dbContext.Mission.Count(x => x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0" && x.Preserve == "1")//总数
                            };

                var querydata = query.OrderByDescending(x => x.sun).Take(5).ToList();//取前30条数据

                string categories = "";
                string seriesdata = "";

                for (int i = 0; i < querydata.Count; i++)
                {
                    categories = categories + "'" + querydata[i].name + "',";
                    seriesdata = seriesdata + querydata[i].sun + ",";
                }
                string jsondata = "{categories:[" + categories + "],series:[{'name':'任务个数','data':[" + seriesdata + "]}]}";

                return Ok(JsonConvert.DeserializeObject(jsondata));
            }
        }


        /// <summary>
        /// 任务进展统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("{order}")]
        public IActionResult selectjinzhan(string order)
        {
            using (_dbContext)
            {

                using (_dbContext)
                {
                    var query = from u in _dbContext.SystemUser
                               select new 
                               {
                                   name = u.RealName,
                                   yuqi = _dbContext.Mission.Count(x => x.AuditStatus=="3" && x.Accomplish == "0" && x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0" && x.Preserve == "1"),//已逾期
                                   wancheng = _dbContext.Mission.Count(x => x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Accomplish == "1"  && x.Recycle == "0" && x.Preserve == "1"),//已完成
                                   jinxing = _dbContext.Mission.Count(x => x.AuditStatus == "0" && x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0" && x.Preserve == "1"),//进行中
                                   sun = _dbContext.Mission.Count(x => x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0" && x.Preserve == "1")//总数
                               };

                    //var query = from m in _dbContext.Mission
                    //            join u
                    //            on m.Principal equals u.SystemUserUuid.ToString()
                    //            where m.Recycle == "0" && m.Preserve == "1"


                    //            //没有在回收站且没有在草稿箱中
                    //            select new
                    //            {
                    //                principal = m.Principal,
                    //                name = getname(u.RealName),
                    //                yuqi = _dbContext.Mission.Count(x => Convert.ToDateTime(x.FinishTime) < DateTime.Now && x.Accomplish == "0" && x.Principal == m.Principal && x.Recycle == "0" && x.Preserve == "1"),//已逾期
                    //                wancheng = _dbContext.Mission.Count(x => x.Principal == m.Principal && x.Accomplish == "1" && x.Principal == m.Principal&& x.Recycle == "0" && x.Preserve == "1"),//已完成
                    //                jinxing = _dbContext.Mission.Count(x => x.AuditStatus == "0" && x.Principal == m.Principal && x.Recycle == "0" && x.Preserve == "1"),//进行中
                    //                sun = _dbContext.Mission.Count(x=>x.Principal == m.Principal && x.Recycle == "0" && x.Preserve == "1")//总数
                    //            };

                    query = query.Where(x => x.sun != 0);//去除没有负责任务的人
                    //按任务总数排行
                    //var data = query.OrderByDescending(x=>x.sun).Take(30).ToList();//去重(并且只取前30条数据)
                    //if (order == "asc") {
                    //    data = query.OrderByOrdinal(x => x.sun.ToString()).Take(30).ToList();
                    //}


                    //按任务完成总数排行
                    var data = query.OrderByDescending(x => x.wancheng).Take(30).ToList();//去重(并且只取前30条数据)
                    if (order == "asc")
                    {
                        data = query.OrderByOrdinal(x => x.wancheng.ToString()).Take(30).ToList();
                    }
                    var response = ResponseModelFactory.CreateResultInstance;
                    response.SetData(data);
                    return Ok(response);
                }



            }
        }



        public string getname(string getname)
        {
            if (getname.Length > 3)
            {
                getname = getname.Substring(0, 3) + "...";
            }
            return getname;
        }



    }
}