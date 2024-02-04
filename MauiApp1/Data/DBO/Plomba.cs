using MySqlConnector;

namespace alpha_3_CRUD;

public class Plomba(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public int? CisloJednacihoRizeni { get; set; }
    public string? Popis { get; set; }
    public DateTime? Datum { get; set; }
    public int? IdPozemek { get; set; }
    public string? NazevPozemek { get; set; }
    
    public void Create()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO plomba (cislo_jednaciho_rizeni, popis, datum, id_pozemek) " +
                           "VALUES (@CisloJednacihoRizeni, @Popis, @Datum, @IdPozemek)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }
    public void CreateSmart()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO plomba (cislo_jednaciho_rizeni, popis, datum, id_pozemek) " +
                           "VALUES (@CisloJednacihoRizeni, @Popis, @Datum, (SELECT id FROM pozemek WHERE nazev = @NazevPozemek)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT * FROM plomba WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                    CisloJednacihoRizeni = reader.GetInt32(1);
                    Popis = reader.GetString(2);
                    Datum = reader.GetDateTime(3);
                }
            }
        }, id);
    }
    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query = "SELECT * FROM plomba";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<Plomba> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Plomba(config)
                    {
                        Id = reader.GetInt32(0),
                        CisloJednacihoRizeni = reader.GetInt32(1),
                        Popis = reader.GetString(2),
                        Datum = reader.GetDateTime(3),
                        IdPozemek = reader.GetInt32(4),
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
            string query = "UPDATE plomba " +
                           "SET nazev = @Nazev, cislo_jednaciho_rizeni = @CisloJednacihoRizeni, popis = @Popis, datum = @Datum, id_pozemek = @IdPozemek" +
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
            string query = "DELETE FROM plomba " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
    }

    public override void SetParameters(ref MySqlCommand sqlCommand, int? id = null)
    {
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@CisloJednacihoRizeni", CisloJednacihoRizeni);
        sqlCommand.Parameters.AddWithValue("@Popis", Popis);
        sqlCommand.Parameters.AddWithValue("@Datum", Datum);
        sqlCommand.Parameters.AddWithValue("@IdPozemek", IdPozemek);
        sqlCommand.Parameters.AddWithValue("@NazevPozemek", NazevPozemek);
        base.SetParameters(ref sqlCommand, id);
    }
}