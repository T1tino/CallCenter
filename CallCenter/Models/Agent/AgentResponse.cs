using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AgentResponse : JsonResponse
{
    public Agent Agent { get; set; }

    public static AgentResponse GetResponse(Agent a)
    {
        AgentResponse r = new AgentResponse();
        r.Status = 0;
        r.Agent = a;
        return r;
    }
}
