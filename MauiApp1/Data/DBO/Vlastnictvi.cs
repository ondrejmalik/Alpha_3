using MySqlConnector;

namespace alpha_3_CRUD;

public class Vlastnictvi(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public string? ZpusobNabiti { get; set; }
    public int? IdPozemek { get; set; }
    public int? IdVlastnik { get; set; }
    public int? Podil { get; set; }
    public string? PozemekParcela { get; set; }
    public string? NazevKatastralniUzemi { get; set; }
    public string? VlastnikIdentifikator { get; set; }

    public void Create()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO vlastnictvi (zpusob_nabiti, id_pozemek, id_vlastnik, podil) " +
                           "VALUES (@zpusob_nabiti, @id_pozemek, @id_podil, @podil)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void CreateSmart()
    {
        base.Create(() =>
        {
            string query = "INSERT INTO vlastnictvi (zpusob_nabiti, id_pozemek, id_vlastnik, podil) " +
                           "VALUES (@zpusob_nabiti, " +
                           "(SELECT id FROM pozemek WHERE parcela = @PozemekParcela AND id_katastralni_uzemi = (SELECT id FROM katastralni_uzemi WHERE nazev = @NazevKatastralniUzemi)), " +
                           "(SELECT id FROM vlastnik WHERE identifikator = @VlastnikIdentifikator), " +
                           "@podil)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT * FROM vlastnictvi WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                    ZpusobNabiti = reader.GetString(1);
                    IdPozemek = reader.GetInt32(2);
                    IdVlastnik = reader.GetInt32(3);
                    Podil = reader.GetInt32(4);
                }
            }
        }, id);
    }
    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query = "SELECT * FROM vlastnictvi";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<Vlastnictvi> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Vlastnictvi(config)
                    {
                        Id = reader.GetInt32(0),
                        ZpusobNabiti = reader.GetString(1),
                        IdPozemek = reader.GetInt32(2),
                        IdVlastnik = reader.GetInt32(3),
                        Podil = reader.GetInt32(4),
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
            string query = "UPDATE vlastnictvi " +
                           "SET zpusob_nabiti = @ZpusobNabiti, id_pozemek = @IdPozemek, id_vlastnik = @IdVlastnik, podil = @Podil " +
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
            string query = "DELETE FROM kraj " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
    }


    public override void SetParameters(ref MySqlCommand sqlCommand, int? id = null)
    {
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@ZpusobNabiti", ZpusobNabiti);
        sqlCommand.Parameters.AddWithValue("@IdPozemek", IdPozemek);
        sqlCommand.Parameters.AddWithValue("@IdVlastnik", IdVlastnik);
        sqlCommand.Parameters.AddWithValue("@Podil", Podil);
        sqlCommand.Parameters.AddWithValue("@PozemekParcela", PozemekParcela);
        sqlCommand.Parameters.AddWithValue("@NazevKatastralniUzemi", NazevKatastralniUzemi);
        sqlCommand.Parameters.AddWithValue("@VlastnikIdentifikator", VlastnikIdentifikator);
        base.SetParameters(ref sqlCommand, id);
    }
}