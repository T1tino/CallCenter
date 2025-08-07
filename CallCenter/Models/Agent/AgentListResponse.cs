using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AgentListResponse : JsonResponse
{
    public List<Agent> Agents { get; set; }

    public static AgentListResponse GetResponse()
    {
        AgentListResponse r = new AgentListResponse();
        r.Status = 0;
        r.Agents = Agent.Get();
        return r;
    }
}
