using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        [HttpPost("[action]/{phonenumber}")]
        [Route("[action]/{phonenumber}")]
        public ActionResult Receive(string phoneNumber)
        {
            if (Call.Receive(phoneNumber) == 0)
                return Ok(MessageResponse.GetResponse(0, "Call added to queue", MessageType.Success));
            else
                return Ok(MessageResponse.GetResponse(1, "Could not add call to queue", MessageType.Error));
        }

        [HttpPost("[action]")]
        [Route("[action]")]
        public ActionResult End()
        {
            int c = Call.End();
            return Ok(MessageResponse.GetResponse(0, c + " calls ended", MessageType.Success));
        }

        [HttpGet("[action]/{date}")]
        [Route("[action]/{date}")]
        public ActionResult Totals(DateTime date)
        {
            return Ok(ViewTotalsResponse.GetResponse(date));
        }
    }
}