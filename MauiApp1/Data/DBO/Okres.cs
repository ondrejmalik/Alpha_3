using MySqlConnector;

namespace alpha_3_CRUD;

public class Okres(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public string? Nazev { get; set; }
    public int? IdKraj { get; set; }
    public string? NazevKraj { get; set; }
    public void Create()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO Okres (nazev, id_kraj) " +
                           "VALUES (@Nazev, @IdKraj)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
        //throw new NotImplementedException();
    }
    public void CreateSmart()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO Okres (nazev, id_kraj) " +
                           "VALUES (@Nazev, (SELECT id FROM kraj WHERE nazev = @NazevKraj)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
        //throw new NotImplementedException();
    }
    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT * FROM okres WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                    Nazev = reader.GetString(1);
                    IdKraj = reader.GetInt32(2);
                }
            }
        }, id);
        //throw new NotImplementedException();
    }
    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query = "SELECT * FROM okres";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<Okres> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Okres(config)
                    {
                        Id = reader.GetInt32(0),
                        Nazev = reader.GetString(1),
                        IdKraj = reader.GetInt32(2),
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
            string query = "UPDATE okres " +
                           "SET nazev = @Nazev, id_kraj = @IdKraj " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
        //throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        base.Delete((id) =>
        {
            string query = "DELETE FROM okres " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
        //throw new NotImplementedException();
    }

    public override void SetParameters(ref MySqlCommand sqlCommand, int? id = null)
    {
        sqlCommand.Parameters.AddWithValue("@Id", id);
        sqlCommand.Parameters.AddWithValue("@Nazev", Nazev);
        sqlCommand.Parameters.AddWithValue("@IdKraj", IdKraj);
        sqlCommand.Parameters.AddWithValue("@NazevKraj", NazevKraj);
        base.SetParameters(ref sqlCommand, id);
    }
}