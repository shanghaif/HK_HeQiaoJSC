using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.ViewModels.PublicityInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaikanSmartTownCockpit.Api.RequestPayload.PublicityInfo;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.PublicityInfo
{
    [Route("api/v1/PublicityInfo/[controller]/[action]")]
    [ApiController]
    public class PromoInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PromoInfoController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region 后台管理列表
        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(PublicityInfoRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.Promo
                            //orderby p.ReleaseTime descending
                            where p.IsDeleted == 0
                            select new
                            {
                                p.PromoUuid,
                                p.Content,
                                p.Title,
                                ReleaseTime = p.ReleaseTime.Value.ToString("yyyy-MM-dd"),
                                p.IsDeleted,
                            };
                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == Convert.ToInt32(((CommonEnum.IsDeleted)payload.IsDeleted)));
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:党建宣传活动信息数据", _dbContext);
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
        public IActionResult Create(PublicityInfoViewModle model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                //if (_dbContext.Promo.Count(x => x.Content == model.Content) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}

                //var entity = _mapper.Map<FoodViewModel, Promo>(model);
                var entity = new Promo();
                entity.Cover = model.Cover;
                entity.Content = model.Content;
                entity.Photo = model.Photo;
                entity.Title = model.Title;
                entity.ReleaseTime = Convert.ToDateTime(model.ReleaseTime);
                entity.PromoUuid = Guid.NewGuid();
                entity.IsDeleted = 0;
                _dbContext.Promo.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:党建宣传活动信息一条数据", _dbContext);
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
                var entity = _dbContext.Promo.FirstOrDefault(x => x.PromoUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(entity);
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
        public IActionResult Edit(PublicityInfoViewModle model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.Promo.FirstOrDefault(x => x.PromoUuid.ToString() == model.PromoUuid);
                if (entity == null)
                {
                    response.SetFailed("不存在");
                    return Ok(response);
                }
                entity.Cover = model.Cover;
                entity.Content = model.Content;
                entity.Title = model.Title;
                entity.Photo = model.Photo;
                entity.ReleaseTime = Convert.ToDateTime(model.ReleaseTime);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:党建宣传活动信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE Promo SET IsDeleted=@IsDel WHERE PromoUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:党建宣传活动信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
    }
}
