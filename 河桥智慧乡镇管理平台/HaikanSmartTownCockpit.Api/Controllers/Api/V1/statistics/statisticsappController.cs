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
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task.app
{
    [Route("api/v1/statistics/[controller]/[action]")]
    [ApiController]
    public class statisticsappController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        public statisticsappController(haikanHeQiaoContext dbContext, IMapper mapper)
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
                int wcygzcount = 0;//我参与的工作
                int wcycount = 0;//我参与的任务数量
                int jrdqcount = 0;//今日到期任务数量
                int jxzcount = 0;//进行中任务数量
                int yyqcount = 0;//已逾期任务数量
                int allworkcount = 0;//所有重点工作数量
                int aswccount = 0;//按时完成任务数量
                int yqwccount = 0;//逾期完成任务数量

                int putongtaskcount = 0;//优先级为普通
                int jinjitaskcount = 0;//优先级为紧急
                int feichangjinjitaskcount = 0;//优先级为非常紧急

                var styuerid = "";

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
                            styuerid = styuer.SystemUserUuid.ToString();
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }




                var query2 = _dbContext.Mission.Where(x => x.Recycle == "0");//没有在回收站并且没有在草稿箱
                wcycount = query2.Where(x => x.Participant.Contains("," + styuerid + ",")).ToList().Count;//我参与的任务数量


                //wcygzcount = _dbContext.Priority.Count(x => x.Participant.Contains(styuerid.ToString()) && x.Recycle == "0" && x.Accomplish == "0");//我参与的重点工作数量

                var nowdate = DateTime.Now.ToString("yyyy-MM-dd");//获取当前日期
                var sarttime = Convert.ToDateTime(nowdate + " " + "00:00:00");
                var endtime = Convert.ToDateTime(nowdate + " " + "23:59:59");
                var query = query2.ToList();
                jrdqcount = query.Where(x => Convert.ToDateTime(x.FinishTime) >= sarttime && Convert.ToDateTime(x.FinishTime) <= endtime).Count();//今日到期任务
                jxzcount = query.Where(x => x.AuditStatus == "0").ToList().Count;//进行中
                var datatimes = DateTime.Now;
                yyqcount = query.Where(x => Convert.ToDateTime(x.FinishTime) < datatimes).ToList().Count;//已逾期还未完成的任务
                //allworkcount = _dbContext.Priority.Count(x => x.Recycle == "0");//所有重点工作
                var data = _dbContext.Mission.Where(x => x.AuditStatus=="2" && x.Recycle == "0").ToList();
                if (data!=null)
                {
                    //aswccount = data.Where(x => Convert.ToDateTime(x.FinishTime) >= Convert.ToDateTime(x.Sortord)).Count();//按时完成
                    //yqwccount = data.Where(x => Convert.ToDateTime(x.FinishTime) < Convert.ToDateTime(x.Sortord)).Count();//逾期完成
                    aswccount = data.Count();//完成
                }
                else
                {
                    aswccount = 0;
                    yqwccount = 0;
                }
                

                putongtaskcount = query.Where(x => x.Priority == "普通").ToList().Count;//优先级为普通的任务数量
                jinjitaskcount = query.Where(x => x.Priority == "紧急").ToList().Count;//优先级为紧急的任务数量
                feichangjinjitaskcount = query.Where(x => x.Priority == "非常紧急").ToList().Count;//优先级为非常紧急的任务数量


                //定义一个键值对集合保存各类型任务分布情况
                Dictionary<string, int> taskcount = new Dictionary<string, int>();
                //taskcount.Add("canyugongzuo", wcygzcount);//我参与的重点共工作数量
                taskcount.Add("canyutask", wcycount);//我参与
                taskcount.Add("nowdatedq", jrdqcount);//今日到期
                taskcount.Add("jinxingzhong", jxzcount);//进行中
                taskcount.Add("yiyuqi", yyqcount);//已逾期
                //taskcount.Add("allwork", allworkcount);//所有重点工作
                taskcount.Add("anshiwancheng", aswccount);//按时完成
                taskcount.Add("yuqiwancheng", yqwccount);//逾期完成
                taskcount.Add("putong", putongtaskcount);//普通任务
                taskcount.Add("jinji", jinjitaskcount);//紧急任务
                taskcount.Add("feichangjinji", feichangjinjitaskcount);//非常紧急任务

                return Ok(taskcount);
            }


        }
        /// <summary>
        /// 参与人任务排行统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectcanyupaihang()
        {

            using (_dbContext)
            {
                var alluser = _dbContext.SystemUser.Where(x => x.IsDeleted == 0).ToList();//查询出所有的用户信息

                //创建数据表保存数据传到前台
                DataTable dt = new DataTable("cart");//统计的数据
                DataColumn dc1 = new DataColumn("name", Type.GetType("System.String"));
                DataColumn dc3 = new DataColumn("count", Type.GetType("System.Int32"));
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc3);


                var missdata = _dbContext.Mission;

                for (int i = 0; i < alluser.Count; i++)
                {
                    var task = missdata.Count(x => x.Participant.Contains(alluser[i].Id.ToString()));
                    DataRow dr = dt.NewRow();
                    dr["name"] = alluser[i].RealName;
                    dr["count"] = task;
                    dt.Rows.Add(dr);
                }

                //按数量排序
                DataTable dtCopy = dt.Copy();
                DataView dv = dt.DefaultView;
                dv.Sort = "count desc";
                dtCopy = DtSelectTop(5, dv.ToTable());//取前5条数据
                Dictionary<string, int> taskcount = new Dictionary<string, int>();
                for (int i = 0; i < dtCopy.Rows.Count; i++)
                {
                    var count = Convert.ToInt32(dtCopy.Rows[i]["count"]);
                    var name = dtCopy.Rows[i]["name"].ToString();
                    taskcount.Add(name, count);
                }
                return Ok(taskcount);
            }


        }


        /// <summary>
        /// 负责人任务排行统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectfuzepaihang()
        {
            using (_dbContext)
            {
                var alluser = _dbContext.SystemUser.Where(x => x.IsDeleted == 0).ToList();//查询出所有的用户信息

                //创建数据表保存数据传到前台
                DataTable dt = new DataTable("cart");//统计的数据
                DataColumn dc1 = new DataColumn("name", Type.GetType("System.String"));
                DataColumn dc3 = new DataColumn("count", Type.GetType("System.Int32"));
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc3);

                for (int i = 0; i < alluser.Count; i++)
                {
                    var task = _dbContext.Mission.Count(x => x.Principal.Contains(alluser[i].SystemUserUuid.ToString()));
                    DataRow dr = dt.NewRow();
                    dr["name"] = alluser[i].RealName;
                    dr["count"] = task;
                    dt.Rows.Add(dr);
                }

                //按数量排序
                DataTable dtCopy = dt.Copy();
                DataView dv = dt.DefaultView;
                dv.Sort = "count desc";
                dtCopy = DtSelectTop(5, dv.ToTable());//取前5条数据





                //var query = from m in _dbContext.Mission
                //            join u in _dbContext.SystemUser
                //            on m.Principal equals u.SystemUserUuid.ToString()
                //            where m.Recycle == "0"//没有在回收站
                //            where m.IsSave == "1"//没有在草稿箱中
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

                //var querydata = query.Take(5).ToList();//取前5条数据

                //定义一个键值对集合保存各负责人任务排行统计
                Dictionary<string, int> taskcount = new Dictionary<string, int>();
                for (int i = 0; i < dtCopy.Rows.Count; i++)
                {
                    var count = Convert.ToInt32(dtCopy.Rows[i]["count"]);
                    var name = dtCopy.Rows[i]["name"].ToString();
                    taskcount.Add(name, count);
                }




                return Ok(taskcount);
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
        /// 参与人任务排行统计app
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectcanyupaihangapp()
        {
            using (_dbContext)
            {

                var query = from u in _dbContext.SystemUser
                            select new
                            {
                                name = u.RealName,
                                sun = _dbContext.Mission.Count(x => x.Participant.Contains("," + u.SystemUserUuid.ToString() + ",") && x.Recycle == "0")//总数
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
        /// 负责人任务排行统计app
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectfuzepaihangapp()
        {
            using (_dbContext)
            {

                var query = from u in _dbContext.SystemUser
                            select new
                            {
                                name = u.RealName,
                                //yuqi = _dbContext.Mission.Count(x => Convert.ToDateTime(x.FinishTime) < DateTime.Now && x.Accomplish == "0" && x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0" && x.IsSave == "1"),//已逾期
                                //wancheng = _dbContext.Mission.Count(x => x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Accomplish == "1" && x.Recycle == "0" && x.IsSave == "1"),//已完成
                                //jinxing = _dbContext.Mission.Count(x => x.AuditStatus == "0" && x.Principal.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0" && x.IsSave == "1"),//进行中
                                sun = _dbContext.Mission.Count(x => x.EstablishName.Contains(u.SystemUserUuid.ToString()) && x.Recycle == "0")//总数
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
        /// 任务参与进展统计详情(升序)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectmissionscanyu()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var missdata = _dbContext.Mission;
                var query = from u in _dbContext.SystemUser
                            select new
                            {
                                name = u.RealName,
                                yuqi = missdata.Count(x => x.AuditStatus == "3" && x.Participant.Contains(","+u.SystemUserUuid.ToString()+ ",") && x.Recycle == "0"),//已逾期
                                wancheng = missdata.Count(x => x.Participant.Contains(","+u.SystemUserUuid.ToString()+ ",") && x.AuditStatus == "2" && x.Recycle == "0"),//已完成
                                jinxing = missdata.Count(x => x.AuditStatus == "0" && x.Participant.Contains("," + u.SystemUserUuid.ToString()+",") && x.Recycle == "0"),//进行中
                                sun = missdata.Count(x => x.Participant.Contains("," + u.SystemUserUuid.ToString() + ",") && x.Recycle == "0")//总数
                            };



                query = query.Where(x => x.sun != 0);//去除没有参与任务的人



                //按任务总数排行
                //var data = query.OrderByDescending(x => x.sun).Take(50).ToList();//去重(并且只取前30条数据)
                //if (order == "asc")
                //{
                //    data = query.OrderBy(x => x.sun.ToString()).Take(50).ToList();
                //}                  

                var data = query.OrderBy(x => x.sun).Take(30).ToList();
                
                response.SetData(data);
                return Ok(response);


            }



        }



        /// <summary>
        /// 任务参与进展统计详情(降序序)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectmissionscanyudesc()
        {

            using (_dbContext)
            {
                var missdata = _dbContext.Mission;
                var query = from u in _dbContext.SystemUser
                            select new
                            {
                                name = u.RealName,
                                yuqi = missdata.Count(x => x.AuditStatus == "3" && x.Participant.Contains("," + u.SystemUserUuid.ToString() + ",") && x.Recycle == "0"),//已逾期
                                wancheng = missdata.Count(x => x.Participant.Contains("," + u.SystemUserUuid.ToString() + ",") && x.AuditStatus == "2" && x.Recycle == "0"),//已完成
                                jinxing = missdata.Count(x => x.AuditStatus == "0" && x.Participant.Contains("," + u.SystemUserUuid.ToString() + ",") && x.Recycle == "0"),//进行中
                                sun = missdata.Count(x => x.Participant.Contains("," + u.SystemUserUuid.ToString() + ",") && x.Recycle == "0")//总数
                            };



                query = query.Where(x => x.sun != 0);//去除没有参与任务的人



                //按任务总数排行
                //var data = query.OrderByDescending(x => x.sun).Take(50).ToList();//去重(并且只取前30条数据)
                //if (order == "asc")
                //{
                //    data = query.OrderBy(x => x.sun.ToString()).Take(50).ToList();
                //}                  

                var data = query.OrderByDescending(x => x.sun).Take(30).ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data);
                return Ok(response);


            }



        }



        /// <summary>
        /// 任务进展统计详情(升序)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectjinzhan()
        {


            using (_dbContext)
            {
                var missdata = _dbContext.Mission;
                var query = from u in _dbContext.SystemUser
                            select new
                            {
                                name = u.RealName,
                                yuqi = missdata.Count(x => x.AuditStatus == "3" && x.EstablishName == u.SystemUserUuid.ToString() && x.Recycle == "0"),//已逾期
                                wancheng = missdata.Count(x => x.EstablishName == u.SystemUserUuid.ToString() && x.AuditStatus == "2" && x.Recycle == "0"),//已完成
                                jinxing = missdata.Count(x => x.AuditStatus == "0" && x.EstablishName == u.SystemUserUuid.ToString() && x.Recycle == "0"),//进行中
                                sun = missdata.Count(x => x.EstablishName == u.SystemUserUuid.ToString() && x.Recycle == "0")//总数
                            };

                //var query = from m in _dbContext.Mission
                //            join u
                //            on m.Principal equals u.SystemUserUuid.ToString()
                //            where m.Recycle == "0" && m.IsSave == "1"


                //            //没有在回收站且没有在草稿箱中
                //            select new
                //            {
                //                principal = m.Principal,
                //                name = getname(u.RealName),
                //                yuqi = _dbContext.Mission.Count(x => Convert.ToDateTime(x.FinishTime) < DateTime.Now && x.Accomplish == "0" && x.Principal == m.Principal && x.Recycle == "0" && x.IsSave == "1"),//已逾期
                //                wancheng = _dbContext.Mission.Count(x => x.Principal == m.Principal && x.Accomplish == "1" && x.Principal == m.Principal&& x.Recycle == "0" && x.IsSave == "1"),//已完成
                //                jinxing = _dbContext.Mission.Count(x => x.AuditStatus == "0" && x.Principal == m.Principal && x.Recycle == "0" && x.IsSave == "1"),//进行中
                //                sun = _dbContext.Mission.Count(x=>x.Principal == m.Principal && x.Recycle == "0" && x.IsSave == "1")//总数
                //            };

                query = query.Where(x => x.sun != 0);//去除没有负责任务的人

                var data = query.OrderBy(x => x.sun).Take(30).ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data);
                return Ok(response);



            }




        }



        /// <summary>
        /// 任务进展统计详情(降序)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selectjinzhandesc()
        {

            using (_dbContext)
            {
                var missdata = _dbContext.Mission;
                var query = from u in _dbContext.SystemUser
                            select new
                            {
                                name = u.RealName,
                                yuqi = missdata.Count(x => x.AuditStatus == "3" && x.EstablishName == u.SystemUserUuid.ToString() && x.Recycle == "0"),//已逾期
                                wancheng = missdata.Count(x => x.EstablishName == u.SystemUserUuid.ToString() && x.AuditStatus == "2" && x.Recycle == "0"),//已完成
                                jinxing = missdata.Count(x => x.AuditStatus == "0" && x.EstablishName == u.SystemUserUuid.ToString() && x.Recycle == "0"),//进行中
                                sun = missdata.Count(x => x.EstablishName == u.SystemUserUuid.ToString() && x.Recycle == "0")//总数
                            };

                //var query = from m in _dbContext.Mission
                //            join u
                //            on m.Principal equals u.SystemUserUuid.ToString()
                //            where m.Recycle == "0" && m.IsSave == "1"


                //            //没有在回收站且没有在草稿箱中
                //            select new
                //            {
                //                principal = m.Principal,
                //                name = getname(u.RealName),
                //                yuqi = _dbContext.Mission.Count(x => Convert.ToDateTime(x.FinishTime) < DateTime.Now && x.Accomplish == "0" && x.Principal == m.Principal && x.Recycle == "0" && x.IsSave == "1"),//已逾期
                //                wancheng = _dbContext.Mission.Count(x => x.Principal == m.Principal && x.Accomplish == "1" && x.Principal == m.Principal&& x.Recycle == "0" && x.IsSave == "1"),//已完成
                //                jinxing = _dbContext.Mission.Count(x => x.AuditStatus == "0" && x.Principal == m.Principal && x.Recycle == "0" && x.IsSave == "1"),//进行中
                //                sun = _dbContext.Mission.Count(x=>x.Principal == m.Principal && x.Recycle == "0" && x.IsSave == "1")//总数
                //            };

                query = query.Where(x => x.sun != 0);//去除没有负责任务的人

                var data = query.OrderByDescending(x => x.sun).Take(30).ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data);
                return Ok(response);



            }




        }



        public static string getname(string getname)
        {
            if (getname.Length > 3)
            {
                getname = getname.Substring(0, 3) + "...";
            }
            return getname;
        }


    }
}
