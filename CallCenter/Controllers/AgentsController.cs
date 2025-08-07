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
    public class AgentsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult Get()
        {
            return Ok(AgentListResponse.GetResponse());
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                Agent a = Agent.Get(id);
                return Ok(AgentResponse.GetResponse(a));
            }
            catch (AgentNotFoundException e)
            {
                return Ok(MessageResponse.GetResponse(1, e.Message, MessageType.Error));
            }
            catch(Exception e)
            {
                return Ok(MessageResponse.GetResponse(999, e.Message, MessageType.Error));
            }
        }
        [HttpPost]
        [Route("")]
        public ActionResult Post()
        {
            return Ok("post");
        }
        [HttpGet]
        [Route("session/[action]")]
        public ActionResult SignIn()
        {
            return Ok("sign in");
        } 
        [HttpGet]
        [Route("filter/{area}/{shift}/{onlyactive}")]
        public ActionResult GetFiltered(string area, string shift, bool onlyActive)
        {
            return Ok("filtered");
        }
    }
}