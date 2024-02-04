using System.Runtime.Serialization;
using MySqlConnector;

namespace alpha_3_CRUD;

public class Obec(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public string? Nazev { get; set; }
    public int? IdOkes { get; set; }
    public string? NazevOkres { get; set; }

    public void Create()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO obec (nazev, id_okres) " +
                           "VALUES (@Nazev, @IdOkres)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void CreateSmart()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO obec (nazev, id_okres) " +
                           "VALUES (@Nazev, (SELECT id FROM okres WHERE nazev = @NazevOkres))";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT obec.id,obec.nazev,obec.id_okres," +
                           "okres.nazev " +
                           "FROM obec " +
                           "JOIN okres ON obec.id_okres = okres.id " +
                           "WHERE obec.id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                    Nazev = reader.GetString(1);
                    IdOkes = reader.GetInt32(2);
                    NazevOkres = reader.GetString(3);
                }
            }
        }, id);
    }

    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query = "SELECT obec.id, obec.nazev, obec.id_okres," +
                           "okres.nazev " +
                           "FROM obec " +
                           "JOIN okres ON obec.id_okres = okres.id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<Obec> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Obec(config)
                    {
                        Id = reader.GetInt32(0),
                        Nazev = reader.GetString(1),
                        IdOkes = reader.GetInt32(2),
                        NazevOkres = reader.GetString(3)
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
            string query = "UPDATE obec " +
                           "SET nazev = @Nazev, id_okres = @IdOkres " +
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
            string query = "DELETE FROM obec " +
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
        sqlCommand.Parameters.AddWithValue("@IdOkres", IdOkes);
        sqlCommand.Parameters.AddWithValue("@NazevOkres", NazevOkres);
        base.SetParameters(ref sqlCommand);
    }
}