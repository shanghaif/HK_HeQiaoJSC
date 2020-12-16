using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.MapQian
{
    [Route("api/v1/MapQian/[controller]/[action]")]
    [ApiController]
    public class AllmapController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AllmapController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 查询所有危房信息的个数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult allroomdata()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = from d in _dbContext.Town
                            where d.IsDeleted == 0
                            select new
                            {
                                name = d.TownName,
                                count = _dbContext.DangerousRoom.Count(x =>x.IsDeleted==0&& x.DangerousAddress.Contains(d.TownName)),

                            };
                response.SetData(query.ToList());
                return Ok(response);
            }
        }


        /// <summary>
        /// 查询所有危房信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult alldangerroom()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.DangerousRoom.Where(x => x.IsDeleted == 0);
                response.SetData(query.ToList());
                return Ok(response);
            }
        }


        /// <summary>
        /// 通过村镇名查询所有危房点位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult mapdian(string budname)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.DangerousRoom.Where(x => x.IsDeleted == 0 && x.DangerousAddress.Contains(budname));
                response.SetData(query.ToList());
                return Ok(response);
            }
        }



        /// <summary>
        /// 查询所有水库信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult allshukudata()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.WaterLevel.Where(x => x.IsDeleted == 0 && x.WaterName.Contains("水库"));
                response.SetData(query.ToList());
                return Ok(response);
            }
        }



        /// <summary>
        /// 查询点击水库监控信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult clickshukudata(string shebeiAddress)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.XlProject.Where(x => x.IsDeleted == 0 && x.XlShebeiType=="水库监控"&&x.ShebeiAddress==shebeiAddress);
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 五水共治
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult allwushuidata()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.WaterLevel.Where(x => x.IsDeleted == 0 && x.WaterName.Contains("水库")==false);
                response.SetData(query.ToList());
                return Ok(response);
            }
        }


        /// <summary>
        /// 通过水库uuid查询水库信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult shuikudata(string uuid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.WaterLevel.FirstOrDefault(x => x.WaterLevelUuid==Guid.Parse(uuid));
                response.SetData(query);
                return Ok(response);
            }
        }



        /// <summary>
        /// 查询公厕数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult gongcelist()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = from d in _dbContext.Gtoilet
                            where d.IsDeleted == 0
                            group d by new
                            {
                                d.GtoiletAddress
                            } into gp
                            select new
                            {
                                gp.Key,
                                count = gp.Count()
                            };
                response.SetData(query.ToList());
                return Ok(response);
            }
        }


        /// <summary>
        /// 查询公厕数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult gongcedata(string name)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                if (name == "all")
                {
                    var query = _dbContext.Gtoilet.Where(x => x.IsDeleted == 0);
                    response.SetData(query.ToList());
                    return Ok(response);
                }
                else
                {
                    var query = _dbContext.Gtoilet.Where(x => x.IsDeleted == 0 && x.GtoiletAddress==name);
                    response.SetData(query.ToList());
                    return Ok(response);
                }

            }
        }



        /// <summary>
        /// 查询景点信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult jingdaian()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                var query = _dbContext.Scenic.Where(x => x.IsDeleted == 0);//表数据
                var datatable = query.ToList();
                var allcount = query.ToList().Count;
                var zhengchangcount = query.Count(x => x.ScenicType.Contains("正常营业"));
                data.Add(new { datatable, allcount, zhengchangcount });
                response.SetData(data);
                return Ok(response);


            }
        }



        /// <summary>
        /// 查询停车位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult tingche()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.ParkingLot.Where(x => x.IsDeleted == 0);//查询所有停车位
                response.SetData(query.ToList());
                return Ok(response);


            }
        }


        /// <summary>
        /// 查询停车位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult tingchedata(string uuid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.ParkingLot.Where(x => x.IsDeleted == 0&&x.ParkingLotUuid==Guid.Parse(uuid));//查询停车位
                response.SetData(query.ToList());
                return Ok(response);


            }
        }




        /// <summary>
        /// 查询灾害点个数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult zaihaidian()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = from d in _dbContext.DisasterInfo
                            where d.IsDeleted == 0
                            group d by new
                            {
                                d.DisasterAddress
                            } into gp
                            select new
                            {
                                gp.Key,
                                count = gp.Count()
                            };
                response.SetData(query.ToList());
                return Ok(response);


            }
        }



        /// <summary>
        /// 查询灾害点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult allzaihaidian(string name)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                if (name == "all")
                {
                    var query = _dbContext.DisasterInfo.Where(x => x.IsDeleted == 0);
                    response.SetData(query.ToList());
                    return Ok(response);
                }
                else
                {
                    var query = _dbContext.DisasterInfo.Where(x => x.IsDeleted == 0&&x.DisasterAddress==name);
                    //var query = _dbContext.DisasterInfo.Where(x => x.IsDeleted == 0 && x.DisasterAddress.Contains(name));                 
                    response.SetData(query.ToList());
                    return Ok(response);

                }



            }
        }



        /// <summary>
        /// 查询所有卫生点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult chaxunweisheng()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.Ygiene.Where(x => x.IsDeleted == 0);//查询停车位
                response.SetData(query.ToList());
                return Ok(response);


            }
        }

        /// <summary>
        /// 查询当前村卫生点信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult dangeweisheng(string weisheng)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.Ygiene.Where(x => x.YgieneAddress.Contains(weisheng)).ToList();
                response.SetData(query);
                return Ok(response);
            }
        }



    }
}
