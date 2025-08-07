using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ViewTotalsResponse: JsonResponse
{
    public string Date { get; set; }
    public string LastUpdate { get; set; }
    public CallDuration Duration { get; set; }
    public CallTotal Totals { get; set; }
    public int ActiveSessions { get; set; }
    public int CallsInQueue { get; set; }
    public int ActiveCalls { get; set; }
    public int TotalCalls { get; set; }
    public string WaitTime { get; set; } 
    public string AverageHandleTime { get; set; }

    public static ViewTotalsResponse GetResponse(DateTime date)
    {
        ViewTotalsResponse r = new ViewTotalsResponse();
        r.Date = date.ToLongDateString();
        r.LastUpdate = DateTime.Now.ToLongTimeString();
        r.Duration = Call.CallDurationByDate(date);
        r.Totals = Call.CallTotalsByHour(date);
        return r;
    }
}
