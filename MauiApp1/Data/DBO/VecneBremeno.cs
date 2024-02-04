using MySqlConnector;

namespace alpha_3_CRUD;

public class VecneBremeno(Config config) : DBObject(config)
{
    public int Id { get; set; }
    public string? Popis { get; set; }
    public DateTime? PoradiK { get; set; }
    public int? IdOpravneniK { get; set; }
    public int? IdOpravneniVeProspechOsobe { get; set; }
    public int? IdOpravneniVeProspechNemovitosti { get; set; }
    public string? IdentifikatorOpravneniK { get; set; }
    public string? KatastralniUzemiOpravneniK { get; set; }
    public string? IdentifikatorVeProspechOsobe { get; set; }
    public string? IdentifikatorVeProspechNemovitosti { get; set; }
    public string? NazevKatastralniUzemi { get; set; }

    public void Create()
    {
        base.Create(() =>
        {
            string query =
                "INSERT INTO vecne_bremeno (popis, poradi_k, id_opravneni_k, id_opravneni_ve_prospech_osobe, id_opravneni_ve_prospech_nemovitosti) " +
                "VALUES (@Popis, @PoradiK, @IdOpravneniK, @IdOpravneniVeProspechOsobe, @IdOpravneniVeProspechNemovitosti)";
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
                "INSERT INTO vecne_bremeno (popis, poradi_k, id_opravneni_k, id_opravneni_ve_prospech_osobe, id_opravneni_ve_prospech_nemovitosti) " +
                "VALUES (@Popis, @PoradiK, " +
                "(SELECT id FROM pozemek WHERE parcela = @IndentifikatorOpravneniK AND id_katastralni_uzemi = (SELECT id FROM katastralni_uzemi WHERE nazev = @KatastralniUzemiOpravneniK)), " +
                "(SELECT id FROM vlastnik WHERE identifikator = @IdentifikatorVeProspechOsobe), " +
                "(SELECT id FROM pozemek WHERE parcela = @IdentifikatorVeProspechNemovitosti AND id_katastralni_uzemi = (SELECT id FROM katastralni_uzemi WHERE nazev = @NazevKatastralniUzemi)))";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand);
            sqlCommand.ExecuteNonQuery();
        });
    }

    public void Read(int id)
    {
        base.Read((id) =>
        {
            string query = "SELECT * FROM vecne_bremeno WHERE id = @id";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            SetParameters(ref sqlCommand, id);
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                    Popis = reader.GetString(1);
                    PoradiK = reader.GetDateTime(2);
                    IdOpravneniK = reader.GetInt32(3);
                    IdOpravneniVeProspechOsobe = reader.GetInt32(4);
                    IdOpravneniVeProspechNemovitosti = reader.GetInt32(5);
                }
            }
        }, id);
    }

    public List<DBObject> List()
    {
        List<DBObject> result;
        result = base.List(() =>
        {
            string query =
                "SELECT vecne_bremeno.id,vecne_bremeno.popis,vecne_bremeno.poradi_k, " +
                "vecne_bremeno.id_opravneni_k,vecne_bremeno.id_opravneni_ve_prospech_osobe, " +
                "vecne_bremeno.id_opravneni_ve_prospech_nemovitosti " +
                "FROM vecne_bremeno";
            MySqlCommand sqlCommand = new(query, Connector.Connection);
            List<VecneBremeno> result = new();
            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new VecneBremeno(config)
                    {
                        Id = reader.GetInt32(0),
                        Popis = reader.GetString(1),
                        PoradiK = reader.GetDateTime(2),
                        IdOpravneniK = reader.GetInt32(3),
                        IdOpravneniVeProspechOsobe = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                        IdOpravneniVeProspechNemovitosti = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
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
            string query = "UPDATE kraj " +
                           "SET popis = @Popis, poradi_k = @PoradiK, id_opravneni_k = @IdOpravneniK, id_opravneni_ve_prospech_osobe = @IdOpravneniVeProspechOsobe, id_opravneni_ve_prospech_nemovitosti = @IdOpravneniVeProspechNemovitosti " +
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
        sqlCommand.Parameters.AddWithValue("@Popis", Popis);
        sqlCommand.Parameters.AddWithValue("@PoradiK", PoradiK);
        sqlCommand.Parameters.AddWithValue("@IdOpravneniK", IdOpravneniK);
        sqlCommand.Parameters.AddWithValue("@IdOpravneniVeProspechOsobe", IdOpravneniVeProspechOsobe);
        sqlCommand.Parameters.AddWithValue("@IdOpravneniVeProspechNemovitosti", IdOpravneniVeProspechNemovitosti);
        sqlCommand.Parameters.AddWithValue("@IdentifikatorOpravneniK", IdentifikatorOpravneniK);
        sqlCommand.Parameters.AddWithValue("@KatastralniUzemiOpravneniK", KatastralniUzemiOpravneniK);
        sqlCommand.Parameters.AddWithValue("@IdentifikatorVeProspechOsobe", IdentifikatorVeProspechOsobe);
        sqlCommand.Parameters.AddWithValue("@IdentifikatorVeProspechNemovitosti", IdentifikatorVeProspechNemovitosti);
        sqlCommand.Parameters.AddWithValue("@NazevKatastralniUzemi", NazevKatastralniUzemi);
        base.SetParameters(ref sqlCommand, id);
    }
}