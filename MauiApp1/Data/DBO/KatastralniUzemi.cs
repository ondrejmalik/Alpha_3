using MySqlConnector;

namespace alpha_3_CRUD;

public class KatastralniUzemi(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public string? Nazev { get; set; }
    public int? IdObec { get; set; }
    public string? NazevObec { get; set; }
    public void Create()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO katastralni_uzemi (nazev, id_obec) " +
                           "VALUES (@Nazev, @IdObec)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void CreateSmart()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO katastralni_uzemi (nazev, id_obec) " +
                           "VALUES (@Nazev, (SELECT id FROM obec WHERE nazev = @NazevObec)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT * FROM katastralni_uzemi WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                    Nazev = reader.GetString(1);
                    IdObec = reader.GetInt32(2);
                }
            }
        }, id);
    }
    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query = "SELECT * FROM katastralni_uzemi";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<KatastralniUzemi> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new KatastralniUzemi(config)
                    {
                        Id = reader.GetInt32(0),
                        Nazev = reader.GetString(1),
                        IdObec = reader.GetInt32(2),
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
            string query = "UPDATE katastralni_uzemi " +
                           "SET nazev = @Nazev, id_obec = @IdObec " +
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
            string query = "DELETE FROM katastralni_uzemi " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
    }
    public void Import(List<DBObject> data)
    {
        base.Import((data) =>
        {
            string query = "INSERT INTO katastralni_uzemi (nazev, id_obec) " +
                           "VALUES (@Nazev, @IdObec)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            foreach (KatastralniUzemi item in data)
            {
                Id = item.Id;
                Nazev = item.Nazev;
                IdObec = item.IdObec;
                SetParameters(ref sqlCommand);
                sqlCommand.ExecuteNonQuery();
            }
        }, data);
    }

    public override void SetParameters(ref MySqlCommand sqlCommand, int? id = null)
    {
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@Nazev", Nazev);
        sqlCommand.Parameters.AddWithValue("@IdObec", IdObec);
        sqlCommand.Parameters.AddWithValue("@NazevObec", NazevObec);
        base.SetParameters(ref sqlCommand, id);
    }
}