using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BassetS.Library.Core.Abstraction;
using BassetS.Library.Core.Enum;
using BassetS.WApi.Logger.DAO;

namespace BassetS.WApi.Logger.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class LoggerController : ControllerBase , IBaseService
    {
        private readonly SQLiteAdapter dbAdapter;
        public LoggerController(SQLiteAdapter dbAdapter)
        {
            this.dbAdapter = dbAdapter ?? throw new ArgumentNullException(nameof(dbAdapter));
        }

        [HttpGet]
        public int Get()
        {
            return 1;
        }
        [HttpGet("state")]
        public EnumServiceState GetState()
        {
            EnumServiceState res = EnumServiceState.Runned;
            return res;
        }
        [HttpGet("sendmessage")]
        public async void SenMessage(string msg, string src, string area)
        {
            try{
                await dbAdapter.SaveMessageAsync(new Models.MessageLogDto(){
                    DT = DateTime.Now,
                    Message = msg,
                    MessArea = area,
                    Source = src
                });
            }catch{
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
