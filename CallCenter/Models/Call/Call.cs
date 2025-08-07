using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class Call
{
    public static int Receive(string phoneNumber)
    {
        //command
        SqlCommand command = new SqlCommand("spReceiveCall");
        //parameters
        command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
        //execute
        return SqlServerConnection.ExecuteProcedure(command);
    }

    public static int End()
    {
        //command
        SqlCommand command = new SqlCommand("spEndCallRandom");
        //execute
        return SqlServerConnection.ExecuteProcedure(command);
    }

    public static CallDuration CallDurationByDate(DateTime date)
    {
        //query
        string query = @"
            select duration, count(*) as total
            from viewCallTotals 
            where date = @date and idStatus = 3 
            group by duration 
            order by duration;";
        //command
        SqlCommand command = new SqlCommand(query);
        //parameters
        command.Parameters.AddWithValue("@date", date);
        //execute
        DataTable table = SqlServerConnection.ExecuteQuery(command);
        //return object
        CallDuration result = new CallDuration();
        result.Minutes = new int[table.Rows.Count];
        result.Totals = new int[table.Rows.Count];
        //index
        int index = 0;
        //read data
        foreach(DataRow row in table.Rows)
        {
            result.Minutes[index] = (Int32)row["duration"];
            result.Totals[index] = (Int32)row["total"];
            index++;
        }
        //return result
        return result;
    }

    public static CallTotal CallTotalsByHour(DateTime date)
    {
        //query
        string query = @"
            select hour, count(*) as total 
            from viewCallTotals 
            where date = @date 
            group by hour 
            order by hour";
        //command
        SqlCommand command = new SqlCommand(query);
        //parameters
        command.Parameters.AddWithValue("@date", date);
        //execute
        DataTable table = SqlServerConnection.ExecuteQuery(command);
        //return object
        CallTotal result = new CallTotal();
        result.Hour = new int[24];
        result.Totals = new int[24];
        //24 hours
        for (int h = 0; h < 24; h++)
        {
            result.Hour[h] = h;
            result.Totals[h] = 0;
        }
        //read data
        foreach (DataRow row in table.Rows)
            result.Totals[(Int32)row["hour"]] = (Int32)row["total"];
        //return result
        return result;
    }
}