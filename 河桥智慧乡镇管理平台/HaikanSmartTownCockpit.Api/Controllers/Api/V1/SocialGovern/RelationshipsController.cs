using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.ViewModels;
using HaikanSmartTownCockpit.Api.ViewModels.SocialGovern;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.SocialGovern
{
    [Route("api/v1/socialgovern/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class RelationshipsController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public RelationshipsController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        #region 图片上传
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> UpLoadPictureAsync([FromForm] FileCommon fc)
        {
            var response = ResponseModelFactory.CreateInstance;
            //var check = System.IO.File.Exists(filename);

            var files = Request.Form.Files;
            if (files == null || files.Count() <= 0)
            {
                response.SetFailed("请上图片");
                return Ok(response);
            }
            //var paths = new List<string>();
            //var dataPaths = new List<string>();
            var allowType = new string[] { "image/jpeg", "image/png" };
            var message = "";
            try
            {
                if (files.Any(c => allowType.Contains(c.ContentType)))
                {
                    if (files[0].Length > 1024 * 1024 * 5)
                    {
                        message += files[0].FileName + "大小超过5M";
                        response.SetFailed(message);
                        return Ok(response);
                    }
                    int index = files[0].FileName.LastIndexOf('.');
                    string fname = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + "_" + Guid.NewGuid().ToString() + files[0].FileName.Substring(index);
                    string strpath = Path.Combine("UploadFiles/" + fc.FileSavePath, fname);
                    string path = Path.Combine(_hostingEnvironment.WebRootPath, strpath);
                    //System.IO.File.Create(path);
                    var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    await files[0].CopyToAsync(stream);

                    stream.Close();
                    response.SetData(new { strpath, fname });
                    //    paths.Add(strpath);
                    //dataPaths.Add(fname);
                    //}

                }
                if (message.Length > 0)
                {
                    response.SetFailed(message);
                }
                //response.SetData(new { paths, dataPaths });
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetFailed(ex.Message);
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
        public IActionResult Create(RelationshipsViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {

                var query = _dbContext.Relationships.FirstOrDefault();
                if(query==null)
                {
                    var entity = _mapper.Map<RelationshipsViewModel, Relationships>(model);
                    entity.RelationshipsUuid = Guid.NewGuid();
                    entity.Addpeople = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.Relationships.Add(entity);
                }
                
                else
                {
                    query.Picture = model.Picture;
                }

                _dbContext.SaveChanges();

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
        [HttpGet]
        public IActionResult LoadRelationships()
        {
            using (_dbContext)
            {
                var entity = _dbContext.Relationships.FirstOrDefault();
                var response = ResponseModelFactory.CreateInstance;
                if(entity==null)
                {
                    response.SetFailed("暂无数据");
                    return Ok(response);
                }

                response.SetData(_mapper.Map<Relationships, RelationshipsViewModel>(entity));
                return Ok(response);
            }
        }
       
        #endregion
    }
}
