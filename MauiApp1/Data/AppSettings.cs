namespace MauiApp1.Data;

public class AppSettings
{
    public static string path { get; set; } = "config.txt";
    public static List<string> keys { get; set; } = new List<string>() { "server=", "username=", "database=", "port=", "password=" };
}