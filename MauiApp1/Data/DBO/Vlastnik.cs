using System.Data;
using MySqlConnector;

namespace alpha_3_CRUD;

public class Vlastnik(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public string? Jmeno { get; set; }
    public string? Prijmeni { get; set; }
    public string? Adresa { get; set; }
    public string? Identifikator { get; set; }
    
    public void Create()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO vlastnik (jmeno, prijmeni, adresa, identifikator) " +
                           "VALUES (@Jmeno, @Prijmeni, @Adresa, @Identifikator)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT vlastnik.id, vlastnik.jmeno,vlastnik.prijmeni,vlastnik.adresa,vlastnik.identifikator FROM vlastnik WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                    Jmeno = reader.GetString(1);
                    Prijmeni = reader.GetString(2);
                    Adresa = reader.GetString(3);
                    Identifikator = reader.GetString(4);
                }
            }
        }, id);
    }
    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query = "SELECT vlastnik.id, vlastnik.jmeno,vlastnik.prijmeni,vlastnik.adresa,vlastnik.identifikator FROM vlastnik";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<Vlastnik> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Vlastnik(config)
                    {
                        Id = reader.GetInt32(0),
                        Jmeno = reader.GetString(1),
                        Prijmeni = reader.GetString(2),
                        Adresa = reader.GetString(3),
                        Identifikator = reader.GetString(4),
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
            string query = "UPDATE vlastnik " +
                           "SET jmeno = @Jmeno, prijmeni = @Prijmeni, adresa = @Adresa, identifikator = @Identifikator " +
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
            string query = "DELETE FROM vlastnik " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
    }


    public override void SetParameters(ref MySqlCommand sqlCommand, int? id = null)
    {
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@Jmeno", Jmeno);
        sqlCommand.Parameters.AddWithValue("@Prijmeni", Prijmeni);
        sqlCommand.Parameters.AddWithValue("@Adresa", Adresa);
        sqlCommand.Parameters.AddWithValue("@Identifikator", Identifikator);
        base.SetParameters(ref sqlCommand, id);
    }
}