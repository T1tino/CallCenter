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
    public class SessionController : ControllerBase
    {
        [HttpGet("[action]/{station}")]
        [Route("[action]/{station}")]
        public ActionResult Login(int station)
        {
            //receive headers
            if (!String.IsNullOrEmpty(Request.Headers["agent"]) &&
                !String.IsNullOrEmpty(Request.Headers["pin"]))
            {
                //read headers
                int agent = Int32.Parse(Request.Headers["agent"]);
                int pin = Int32.Parse(Request.Headers["pin"]);
                //login
                int result = Session.Login(agent, pin, station);
                //message
                string message = ((LoginStatus)result).ToString();
                //message type
                MessageType type = MessageType.Success;
                if (result > 0) type = MessageType.Error;
                //return result
                return Ok(MessageResponse.GetResponse(result, message, type));
            }
            else
            {
                return Ok(MessageResponse.GetResponse(500, "Missing Security Headers", MessageType.Error));
            }
            
        }
    }
}