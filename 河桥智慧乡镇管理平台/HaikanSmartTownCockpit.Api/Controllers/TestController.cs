using Haikan3.Utils;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HaikanSmartTownCockpit.Api.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 测试日志
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Logger()
        {
            _logger.LogDebug(message: "LogDebug()...");
            _logger.LogInformation(message: "LogInformation()...");
            _logger.LogWarning(message: "LogWarning()...");
            _logger.LogError(message: "LogError()...");
            ResponseResultModel response = ResponseModelFactory.CreateResultInstance;
            response.SetSuccess(message: "test logger success");
            return Ok(value: response);
        }

        [HttpGet]
        public string Test1()
        {
            var user = User;
            return "xxxxxxxxxxxxxxx";
        }


        /// <summary>
        /// 短信测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string MSG()
        {
            var str= YunSendMsg.SendMsg("16657110023", "林志国就是个弟弟!");
            return str;
        }


        [HttpGet]
        public string GetIP()
        {
            var ip = HttpContext.Connection.RemoteIpAddress;
            var port = HttpContext.Connection.RemotePort;
            return (ip+":" + port.ToString());
        }
    }
}
