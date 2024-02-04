using System.Text.RegularExpressions;
using MauiApp1.Data;

namespace alpha_3_CRUD;

public class Config
{
    public StreamReader Sr { get; set; }
    public Dictionary<string, string> Dict { get; set; } = new();
    private int atemptCount;

    public Config(string fileName, List<string> keys)
    {
        try
        {
            Configure(fileName, keys);
        }
        catch (IOException e)
        {
            atemptCount++;
            if (atemptCount < 5)
            {
                ReadConfigFromFile();
            }
            else
            {
                Console.WriteLine(e);
                throw new IOException("ERROR 1 Failed to read configuration file.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void Configure(string filename, List<string> keys)
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            @$"alpha3\{filename}");
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path);
            StreamWriter sw = new(path);
            sw.WriteLine("server=");
            sw.WriteLine("username=root");
            sw.WriteLine("database=alpha3");
            sw.WriteLine("port=3306");
            sw.WriteLine("password=student"); //TODO change password
            sw.Close();
        }

        Sr = new(path);
        foreach (var key in keys)
        {
            Dict.Add(key, "");
        }

        ReadConfigFromFile();
    }

    public void ReadConfigFromFile()
    {
        string configText = Sr.ReadToEnd();
        foreach (var key in Dict.Keys)
        {
            Regex re = new($"{key}(.*)");
            Dict[key] = re.Match(configText).Groups[1].Value;
        }
    }
}