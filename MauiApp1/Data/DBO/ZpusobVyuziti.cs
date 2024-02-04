using MySqlConnector;

namespace alpha_3_CRUD;

public class ZpusobVyuziti(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public string? Nazev { get; set; }

    public void Create()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO zpusob_vyuziti (nazev) " +
                           "VALUES (@Nazev)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT * FROM zpusob_vyuziti WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                    Nazev = reader.GetString(1);
                }
            }
        }, id);
    }

    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query = "SELECT * FROM zpusob_vyuziti";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<ZpusobVyuziti> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new ZpusobVyuziti(config)
                    {
                        Id = reader.GetInt32(0),
                        Nazev = reader.GetString(1),
                    });
                }
            }

            return new List<DBObject>(result);
        });
        return result;
    }

    public void Update(int id)
    {
        base.Update((id) =>
        {
            string query = "UPDATE zpusob_vyuziti " +
                           "SET nazev = @Nazev " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
    }

    public void Delete(int id)
    {
        base.Delete((id) =>
        {
            string query = "DELETE FROM zpusob_vyuziti " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
    }

    public override void SetParameters(ref MySqlCommand sqlCommand, int? id = null)
    {
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@Nazev", Nazev);
        base.SetParameters(ref sqlCommand, id);
    }
}