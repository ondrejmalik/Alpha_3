using MySqlConnector;

namespace alpha_3_CRUD;

public class Pozemek(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public string? Parcela { get; set; }
    public string? Popis { get; set; }
    public int? VymeraM2 { get; set; }
    public int? IdDruh { get; set; }
    public int? IdZpusobVyuziti { get; set; }
    public int? IdZpusobOchrany { get; set; }
    public int? IdKatastralniUzemi { get; set; }
    public string? NazevZpusobVyuziti { get; set; }
    public string? NazevZpusobOchrany { get; set; }
    public string? NazevKatastralniUzemi { get; set; }
    public void Create()
    {
        base.Create(() =>
        {
            string query =
                "INSERT INTO pozemek (parcela, vymera_m2, id_druh,popis, id_zpusob_vyuziti, id_zpusob_ochrany, id_katastralni_uzemi) " +
                "VALUES (@Parcela, @VymeraM2, @IdDruh, @Popis, @IdZpusobVyuziti, @IdZpusobOchrany, @IdKatastralniUzemi)";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void CreateSmart()
    {
        base.Create(() =>
        {
            string query =
                "INSERT INTO pozemek (parcela, vymera_m2, id_druh,popis, id_zpusob_vyuziti, id_zpusob_ochrany, id_katastralni_uzemi) " +
                "VALUES (@Parcela, @VymeraM2, @IdDruh, @Popis, (SELECT id FROM zpusob_vyuziti WHERE nazev = @NazevZpusobVyuziti), (SELECT id FROM zpusob_ochrany WHERE nazev = @NazevZpusobOchrany), (SELECT id FROM katastralni_uzemi WHERE nazev = @NazevKatastralniUzemi))";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT * FROM pozemek WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            MySqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Id = reader.GetInt32(0);
                Parcela = reader.GetString(1);
                VymeraM2 = reader.GetInt32(2);
                IdDruh = reader.GetInt32(3);
                IdZpusobVyuziti = reader.GetInt32(4);
                IdZpusobOchrany = reader.GetInt32(5);
                Popis = reader.GetString(6);
                IdKatastralniUzemi = reader.GetInt32(7);
            }
        }, id);
    }
    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query = "SELECT * FROM pozemek";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<Pozemek> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Pozemek(config)
                    {
                        Id = reader.GetInt32(0),
                        Parcela = reader.GetString(1),
                        VymeraM2 = reader.GetInt32(2),
                        IdDruh = reader.GetInt32(3),
                        IdZpusobVyuziti = reader.GetInt32(4),
                        IdZpusobOchrany = reader.GetInt32(5),
                        Popis = reader.GetString(6),
                        IdKatastralniUzemi = reader.GetInt32(7),
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
            string query = "UPDATE pozemek " +
                           "SET parcela = @Parcela, vymera_m2 = @VymeraM2, id_druh = @IdDruh,id_zpusob_vyuziti = @IdZpusobVyuziti, id_zpusob_ochrany = @IdZpusobOchrany, popis = @Popis, id_katastralni_uzemi = @IdKatastralniUzemi " +
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
            string query = "DELETE FROM pozemek " +
                           "WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            sqlCommand.ExecuteNonQuery();
        }, id);
    }


    public override void SetParameters(ref MySqlCommand sqlCommand, int? id = null)
    {
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@Parcela", Parcela);
        sqlCommand.Parameters.AddWithValue("@VymeraM2", VymeraM2);
        sqlCommand.Parameters.AddWithValue("@IdDruh", IdDruh);
        sqlCommand.Parameters.AddWithValue("@IdZpusobVyuziti", IdZpusobVyuziti);
        sqlCommand.Parameters.AddWithValue("@IdZpusobOchrany", IdZpusobOchrany);
        sqlCommand.Parameters.AddWithValue("@Popis", Popis);
        sqlCommand.Parameters.AddWithValue("@IdKatastralniUzemi", IdKatastralniUzemi);
        sqlCommand.Parameters.AddWithValue("@NazevZpusobVyuziti", NazevZpusobVyuziti);
        sqlCommand.Parameters.AddWithValue("@NazevZpusobOchrany", NazevZpusobOchrany);
        sqlCommand.Parameters.AddWithValue("@NazevKatastralniUzemi", NazevKatastralniUzemi);
        base.SetParameters(ref sqlCommand, id);
    }
}