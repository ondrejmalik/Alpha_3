using MauiApp1.Data;
using MySqlConnector;
using Newtonsoft.Json;
using static MauiApp1.Data.DebugMessage;

namespace alpha_3_CRUD;

public abstract class DBObject
{
    protected DBObject(Config config)
    {
        if (config == null)
        {
            config = new Config(AppSettings.path, AppSettings.keys);
        }

        if (!config.Dict.ContainsKey("server=") || !config.Dict.ContainsKey("username=") ||
            !config.Dict.ContainsKey("database=") || !config.Dict.ContainsKey("port=") ||
            !config.Dict.ContainsKey("password="))
        {
            throw new ArgumentException("ERROR 2 Invalid configuration format.");
        }

        Connector = new MySQLConnector(
            config.Dict["server="],
            config.Dict["username="],
            config.Dict["database="],
            config.Dict["port="],
            config.Dict["password="]
        );
    }

    [JsonIgnore] public MySQLConnector? Connector { get; set; }
    public void Create(Action execute)
    {
        Connector.OpenConnection();
        try
        {
            execute();
        }
        catch (MySqlException e)
        {
            DebugMessage.Message = "ERROR 4 Failed to execute database command."+ e.Message;
            Console.WriteLine("ERROR 4 Failed to execute database command.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Connector.CloseConnection();
    }

    public void Read(Action<int> execute, int id)
    {
        Connector.OpenConnection();
        try
        {
            execute(id);
        }
        catch (Exception e)
        {
            DebugMessage.Message = "ERROR 4 Failed to execute database command."+ e.Message;
            Console.WriteLine("ERROR 4 Failed to execute database command.");
        }

        Connector.CloseConnection();
    }
    public List<DBObject> List(Func<List<DBObject>> execute)
    {
        List<DBObject> result = new();
        Connector.OpenConnection();
        try
        {
            result = execute();
        }
        catch (Exception e)
        {
            DebugMessage.Message = "ERROR 4 Failed to execute database command."+ e.Message;
            Console.WriteLine("ERROR 4 Failed to execute database command.");
        }

        Connector.CloseConnection();
        return result;
    }
    public void Update(Action<int> execute, int id)
    {
        Connector.OpenConnection();
        try
        {
            execute(id);
        }
        catch (Exception e)
        {
            DebugMessage.Message = "ERROR 4 Failed to execute database command."+ e.Message;
            Console.WriteLine("ERROR 4 Failed to execute database command.");
        }

        Connector.CloseConnection();
    }

    public void Delete(Action<int> execute, int id)
    {
        Connector.OpenConnection();
        try
        {
            execute(id);
        }
        catch (Exception e)
        {
            DebugMessage.Message = "ERROR 4 Failed to execute database command."+ e.Message;
            Console.WriteLine("ERROR 4 Failed to execute database command.");
        }

        Connector.CloseConnection();
    }

    public void Import<T>(Action<List<DBObject>> execute, List<T> data)
    {
        Connector.OpenConnection();
        try
        {
            execute(data as List<DBObject>);
        }
        catch (Exception e)
        {
            DebugMessage.Message = "ERROR 4 Failed to execute database command."+ e.Message;
            Console.WriteLine("ERROR 4 Failed to execute database command.");
        }

        Connector.CloseConnection();
    }

    public static T DeserializeJson<T>(string jsonData)
    {
        try
        {
            T result = JsonConvert.DeserializeObject<T>(jsonData);
            return result;
        }
        catch (JsonException ex)
        {
            DebugMessage.Message = "Error 5 Failed deserializing JSON.";
            Console.WriteLine($"Error 5 Failed deserializing JSON: {ex.Message}");
            return default(T);
        }
    }

    public virtual void SetParameters(ref MySqlCommand sqlCommand, int? id = null)
    {
    }
}