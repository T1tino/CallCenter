using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

public class AgentMapper
{
    /// <summary>
    /// Maps data row values into an Agent object
    /// </summary>
    /// <param name="row">Data row</param>
    /// <returns></returns>
    public static Agent ToObject(DataRow row)
    {
        //map column values
        int id = (Int32)row["agent_id"];
        string name = (string)row["agent_name"];
        string photo = (string)row["agent_photo"];
        //return agent
        return new Agent(id, name, photo);
    }

    /// <summary>
    /// Populates a list of agents from a data table
    /// </summary>
    /// <param name="table">Data table</param>
    /// <returns></returns>
    public static List<Agent> ToList(DataTable table)
    {
        //create list
        List<Agent> list = new List<Agent>();
        //populate list
        foreach (DataRow row in table.Rows)
        {
            list.Add(ToObject(row));
        }
        //return list
        return list;
    }
}
