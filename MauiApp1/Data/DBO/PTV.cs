using MauiApp1.Data;
using MySqlConnector;

namespace alpha_3_CRUD;

public class PTV
{
    MySQLConnector Connector { get; set; }

    public PTV(Config config)
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

    public List<List<string>> ReadLV(string identifikator, string katastralniUzemi)
    {
        Connector.OpenConnection();
        string query = "CALL list_vlastnictvi(@identifikator, @katastralni_uzemi)";
        MySqlCommand sqlCommand = new MySqlCommand(query, Connector.Connection);
        SetParameters(ref sqlCommand, identifikator, katastralniUzemi);
        List<List<string>> result = new();
        result.Add(new List<string>());
        result.Add(new List<string>());
        bool first = true;
        int row = 1;
        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                if (first)
                {
                    first = false;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result[0].Add(reader.GetName(i));
                    }
                }
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result[row].Add(reader[i].ToString());
                }
                row++;
            }
        }

        Connector.CloseConnection();

        return result;
    }
    public List<List<string>> ReadParcela(string cisloParcely, string katastralniUzemi)
    {
        Connector.OpenConnection();
        string query = "Call show_parcela(@identifikator, @katastralni_uzemi)";
        MySqlCommand sqlCommand = new MySqlCommand(query, Connector.Connection);
        SetParameters(ref sqlCommand, cisloParcely, katastralniUzemi);
        List<List<string>> result = new();
        result.Add(new List<string>());
        result.Add(new List<string>());
        bool first = true;
        int row = 1;
        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                if (first)
                {
                    first = false;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result[0].Add(reader.GetName(i));
                    }
                }
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result[row].Add(reader[i].ToString());
                }
                row++;
            }
        }

        Connector.CloseConnection();

        return result;
    }
    public void ChangePodli(string identifikatorVlastnikapridani,string identifikatorVlastnikaodebrani, string katastralniUzemi, string cisloPracely, int diffPodil)
    {
        Connector.OpenConnection();
        string query = "Call change_podil(@cislo_parcely, @katastralni_uzemi, @identifikator_pridani, @identifikator_odebrani, @podil_diff)";
        MySqlCommand sqlCommand = new(query, Connector.Connection);
        SetParameters(ref sqlCommand, identifikatorVlastnikapridani, identifikatorVlastnikaodebrani, katastralniUzemi, cisloPracely, diffPodil);
        sqlCommand.ExecuteNonQuery();
    }
    public void SetParameters(ref MySqlCommand sqlCommand, string identifikator = null, string katastralniUzemi = null)
    {
        sqlCommand.Parameters.AddWithValue("@identifikator", identifikator);
        sqlCommand.Parameters.AddWithValue("@katastralni_uzemi", katastralniUzemi);
    }
    public void SetParameters(ref MySqlCommand sqlCommand, string identifikatorVlastnikaPridani,string identifikatorVlastnikaOdebrani, string katastralniUzemi, string cisloPracely, int podilDiff)
    {
        sqlCommand.Parameters.AddWithValue("@identifikator_pridani", identifikatorVlastnikaPridani);
        sqlCommand.Parameters.AddWithValue("@identifikator_odebrani", identifikatorVlastnikaOdebrani);
        sqlCommand.Parameters.AddWithValue("@katastralni_uzemi", katastralniUzemi);
        sqlCommand.Parameters.AddWithValue("@cislo_parcely", cisloPracely);
        sqlCommand.Parameters.AddWithValue("@podil_diff", podilDiff);
    }
    
}