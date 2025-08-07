using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class SqlServerConnection
{
    #region variables

    private static string connectionString =
        "Data Source = " + Config.Configuration.SqlServer.Server + ";" +
        "Initial Catalog = " + Config.Configuration.SqlServer.Database + "; " +
        "User Id = " + Config.Configuration.SqlServer.User + "; " +
        "Password = " + Config.Configuration.SqlServer.Password + "; ";

    #endregion

    #region class methods

    private static SqlConnection GetConnection()
    {
        //connection
        SqlConnection connection = new SqlConnection();
        try
        {
            //assign connection string
            connection.ConnectionString = connectionString;
            //open connection
            connection.Open();
        }
        catch(ArgumentException e)
        {
        }
        catch(SqlException e)
        {
        }
        catch(Exception e)
        {
        }
        //return connection
        return connection;
    }

    public static DataTable ExecuteQuery(SqlCommand command)
    {
        //create table
        DataTable table = new DataTable();
        //get connection
        SqlConnection connection = GetConnection();
        //check if connection is open
        if (connection.State == ConnectionState.Open)
        {
            try
            {
                //assign connection to command
                command.Connection = connection;
                //adapter
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                //execute query and fill result table
                adapter.Fill(table);
                //close connection
                connection.Close();
                connection.Dispose();
            }
            catch (SqlException e)
            {
            }
            catch (Exception e)
            {
            }
        }
        //return table
        return table;
    }


    public static bool ExecuteCommend(SqlCommand command)
    {
        //result
        bool result = false;
        //get connection
        SqlConnection connection = GetConnection();
        //check if connection is open
        if (connection.State == ConnectionState.Open)
        {
            try
            {
                //assign connection to command
                command.Connection = connection;
                //execute procedure
                command.ExecuteNonQuery();
                //result
                result = true;
            }
            catch (SqlException e)
            {
            }
            catch (Exception e)
            {
            }
            //close connection
            connection.Close();
            connection.Dispose();
        }
        //return result
        return result;
    }


    public static int ExecuteProcedure(SqlCommand command)
    {
        //result
        int result = 999;
        //get connection
        SqlConnection connection = GetConnection();
        //check if connection is open
        if (connection.State == ConnectionState.Open)
        {
            try
            {
                //assign connection to command
                command.Connection = connection;
                //command is a stored procedure
                command.CommandType = CommandType.StoredProcedure;
                //return parameter
                SqlParameter returnParameter = new SqlParameter("@status", DbType.Int32);
                //parameter is output
                returnParameter.Direction = ParameterDirection.Output;
                //add parameter to command
                command.Parameters.Add(returnParameter);
                //execute procedure
                command.ExecuteNonQuery();
                //read result
                result = (Int32)command.Parameters["@status"].Value;
                //close connection
                connection.Close();
                connection.Dispose();
            }
            catch (SqlException e)
            {
            }
            catch (Exception e)
            {
            }
        }
        //return result
        return result;
    }

    #endregion
}
