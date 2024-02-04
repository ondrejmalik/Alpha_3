namespace alpha_3_CRUD;

using MySqlConnector;

public class MySQLConnector(string server, string user, string database, string port, string password)
{
    public MySqlConnection Connection { get; set; } =
        new MySqlConnection($"server={server};user={user};database={database};port={port};password={password};");
    
    public void OpenConnection()
    {
        try
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        catch (Exception e)
        {
            throw new Exception("ERROR 3 Failed to open database connection.", e);
            throw;
        }
    }
    public void CloseConnection()
    {
        try
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}