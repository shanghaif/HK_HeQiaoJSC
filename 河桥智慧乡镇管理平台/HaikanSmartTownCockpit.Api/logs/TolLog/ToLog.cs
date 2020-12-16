using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.logs.TolLog
{
    
    public class ToLog
    {
        public static string AddLog(string Typem ,string Content , haikanHeQiaoContext _dbContext)
        {
            string messange = "";
            var entity = new HaikanSmartTownCockpit.Api.Entities.SystemLog();
            entity.SystemLogUuid = Guid.NewGuid();
            entity.UserId = AuthContextService.CurrentUser.Guid.ToString();
            entity.UserName = AuthContextService.CurrentUser.DisplayName;
            entity.Type = Typem;
            entity.OperationContent = Content;
            entity.OperationTime = DateTime.Now;
            entity.AddTime = DateTime.Now;
            entity.AddPeople = "系统日志";
            entity.IsDelete = 0;
            _dbContext.SystemLog.Add(entity);
            _dbContext.SaveChanges();
            return messange;
        }
       

    }
}
