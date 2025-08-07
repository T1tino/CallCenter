using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class Session
{
    #region class methods

    public static int Login(int agentId, int pin, int stationId)
    {
        //command
        SqlCommand command = new SqlCommand("spLoginAgent");
        //parameters
        command.Parameters.AddWithValue("@agentId", agentId);
        command.Parameters.AddWithValue("@agentPin", pin);
        command.Parameters.AddWithValue("@stationId", stationId);
        //execute
        return SqlServerConnection.ExecuteProcedure(command);
    }

    #endregion
}
