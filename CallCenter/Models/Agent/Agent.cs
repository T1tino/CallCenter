using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class Agent
{
    #region statements

    private static string select = @"
        select id as agent_id, name as agent_name, photo as agent_photo 
        from agents
        order by agent_name";
    private static string selectById = @"
        select id as agent_id, name as agent_name, photo as agent_photo 
        from agents
        where id = @ID";

    #endregion

    #region attributes

    private int _id;
    private string _name;
    private int _pin;
    private string _photo;

    #endregion

    #region properties

    public int Id { get => _id; }
    public string Name { get => _name; set => _name = value; }
    public int Pin { set => _pin = value; }
    public string Photo { get => Config.Configuration.Paths.Domain + Config.Configuration.Paths.Photos.Agents + _photo; set => _photo = value; }

    #endregion

    #region constructors

    /// <summary>
    /// Creates an empty object
    /// </summary>
    public Agent()
    {
        _id = 0;
        _name = "";
        _pin = 0;
        _photo = "nophoto.png";
    }
    /// <summary>
    /// Creates an object with data from the arguments
    /// </summary>
    /// <param name="id">Agent id</param>
    /// <param name="firstName">First name</param>
    /// <param name="lastName">Last name</param>
    /// <param name="dateOfBirth">Date of birth</param>
    /// <param name="photo">Photo file name</param>
    public Agent(int id, string name, int pin, string photo)
    {
        _id = id;
        _name = name;
        _pin = pin;
        _photo = photo;
    }

    public Agent(int id, string name, string photo)
    {
        _id = id;
        _name = name;
        _pin = 0;
        _photo = photo;
    }
    #endregion

    #region class methods

    /// <summary>
    /// Returns a list of all the agents
    /// </summary>
    /// <returns></returns>
    public static List<Agent> Get()
    {
        //sql command
        SqlCommand command = new SqlCommand(select);
        //execute query and populate list
        return AgentMapper.ToList(SqlServerConnection.ExecuteQuery(command));
    }
    /// <summary>
    /// Returns the agent with the specified id
    /// </summary>
    /// <param name="id">Agent id</param>
    /// <returns></returns>
    public static Agent Get(int id)
    {
        //sql command
        SqlCommand command = new SqlCommand(selectById);
        //paramaters
        command.Parameters.AddWithValue("@ID", id);
        //execute query 
        DataTable table = SqlServerConnection.ExecuteQuery(command);
        //check if rows were found
        if (table.Rows.Count > 0)
            return AgentMapper.ToObject(table.Rows[0]);
        else
            throw new AgentNotFoundException(id);
    }

    #endregion
}
