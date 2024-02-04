using System.Reflection;
using System.Security.Principal;
using alpha_3_CRUD;

namespace MauiApp1.Data;

public class DBOService
{
    public Task<DBObject> GetSingle(int id, Type type)
    {
        Config config = new("config.txt",
            new List<string>() { "server=", "username=", "database=", "port=", "password=" });
        DBObject result = (DBObject)Activator.CreateInstance(type, config);
        switch (result)
        {
            case Obec tmp:
                tmp.Read(id);
                break;
            case Kraj tmp:
                tmp.Read(id);
                break;
            case KatastralniUzemi tmp:
                tmp.Read(id);
                break;
            case Plomba tmp:
                tmp.Read(id);
                break;
            case ZpusobOchrany tmp:
                tmp.Read(id);
                break;
            case ZpusobVyuziti tmp:
                tmp.Read(id);
                break;
            case VecneBremeno tmp:
                tmp.Read(id);
                break;
            case Vlastnictvi tmp:
                tmp.Read(id);
                break;
            case Vlastnik tmp:
                tmp.Read(id);
                break;
            case Okres tmp:
                tmp.Read(id);
                break;
            case DruhPozemku tmp:
                tmp.Read(id);
                break;
            case Pozemek tmp:
                tmp.Read(id);
                break;
            default:
                // Handle other cases if needed
                break;
        }

        return Task.FromResult<DBObject>(result);
    }

    public Task<List<DBObject>> GetList(Type type)
    {
        Config config = new("config.txt",
            new List<string>() { "server=", "username=", "database=", "port=", "password=" });
        DBObject obj = (DBObject)Activator.CreateInstance(type, config);
        List<DBObject> result = new();
        switch (obj)
        {
            case Obec tmp:
                result = tmp.List();
                break;
            case Kraj tmp:
                result = tmp.List();
                break;
            case KatastralniUzemi tmp:
                result = tmp.List();
                break;
            case Plomba tmp:
                result = tmp.List();
                break;
            case ZpusobOchrany tmp:
                result = tmp.List();
                break;
            case ZpusobVyuziti tmp:
                result = tmp.List();
                break;
            case VecneBremeno tmp:
                result = tmp.List();
                break;
            case Vlastnictvi tmp:
                result = tmp.List();
                break;
            case Vlastnik tmp:
                result = tmp.List();
                break;
            case Okres tmp:
                result = tmp.List();
                break;
            case DruhPozemku tmp:
                result = tmp.List();
                break;
            case Pozemek tmp:
                result = tmp.List();
                break;
        }

        return Task.FromResult(result);
    }

    public Task<List<DBObject>> Delete(int id, Type type)
    {
        Config config = new("config.txt",
            new List<string>() { "server=", "username=", "database=", "port=", "password=" });
        DBObject obj = (DBObject)Activator.CreateInstance(type, config);
        List<DBObject> result = new();
        switch (obj)
        {
            case Obec tmp:
                tmp.Delete(id);
                break;
            case Kraj tmp:
                tmp.Delete(id);
                break;
            case KatastralniUzemi tmp:
                tmp.Delete(id);
                break;
            case Plomba tmp:
                tmp.Delete(id);
                break;
            case ZpusobOchrany tmp:
                tmp.Delete(id);
                break;
            case ZpusobVyuziti tmp:
                tmp.Delete(id);
                break;
            case VecneBremeno tmp:
                tmp.Delete(id);
                break;
            case Vlastnictvi tmp:
                tmp.Delete(id);
                break;
            case Vlastnik tmp:
                tmp.Delete(id);
                break;
            case Okres tmp:
                tmp.Delete(id);
                break;
            case DruhPozemku tmp:
                tmp.Delete(id);
                break;
            case Pozemek tmp:
                tmp.Delete(id);
                break;
        }

        return Task.FromResult(result);
    }

    public void Create(DBObject newObject, Type type)
    {
        PropertyInfo[] parentProperties = typeof(DBObject).GetProperties();
        PropertyInfo[] childProperties = type.GetProperties();
        IEnumerable<PropertyInfo> uniqueProperties = childProperties.Where(childProp => 
            !parentProperties.Any(parentProp => parentProp.Name == childProp.Name));
        Config config = new("config.txt", new List<string>() { "server=", "username=", "database=", "port=", "password=" });
        DBObject obj = (DBObject)Activator.CreateInstance(type, config);
        foreach (var prop in uniqueProperties)
        {
            obj.GetType().GetProperty(prop.Name)?.SetValue(obj, prop.GetValue(newObject));
        }

        switch (obj)
        {
            case Obec tmp:
                tmp.Create();
                break;
            case Kraj tmp:
                tmp.Create();
                break;
            case KatastralniUzemi tmp:
                tmp.Create();
                break;
            case Plomba tmp:
                tmp.Create();
                break;
            case ZpusobOchrany tmp:
                tmp.Create();
                break;
            case ZpusobVyuziti tmp:
                tmp.Create();
                break;
            case VecneBremeno tmp:
                tmp.Create();
                break;
            case Vlastnictvi tmp:
                tmp.Create();
                break;
            case Vlastnik tmp:
                tmp.Create();
                break;
            case Okres tmp:
                tmp.Create();
                break;
            case DruhPozemku tmp:
                tmp.Create();
                break;
            case Pozemek tmp:
                tmp.Create();
                break;
        }

    }

    public void Update(DBObject newObject,int id, Type type)
    {
        PropertyInfo[] parentProperties = typeof(DBObject).GetProperties();
        PropertyInfo[] childProperties = type.GetProperties();
        IEnumerable<PropertyInfo> uniqueProperties = childProperties.Where(childProp => 
            !parentProperties.Any(parentProp => parentProp.Name == childProp.Name));
        Config config = new("config.txt", new List<string>() { "server=", "username=", "database=", "port=", "password=" });
        DBObject obj = (DBObject)Activator.CreateInstance(type, config);
        foreach (var prop in uniqueProperties)
        {
            obj.GetType().GetProperty(prop.Name)?.SetValue(obj, prop.GetValue(newObject));
        }
        
        switch (obj)
        {
            case Obec tmp:
                tmp.Update(id);
                break;
            case Kraj tmp:
                tmp.Update(id);
                break;
            case KatastralniUzemi tmp:
                tmp.Update(id);
                break;
            case Plomba tmp:
                tmp.Update(id);
                break;
            case ZpusobOchrany tmp:
                tmp.Update(id);
                break;
            case ZpusobVyuziti tmp:
                tmp.Update(id);
                break;
            case VecneBremeno tmp:
                tmp.Update(id);
                break;
            case Vlastnictvi tmp:
                tmp.Update(id);
                break;
            case Vlastnik tmp:
                tmp.Update(id);
                break;
            case Okres tmp:
                tmp.Update(id);
                break;
            case DruhPozemku tmp:
                tmp.Update(id);
                break;
            case Pozemek tmp:
                tmp.Update(id);
                break;
        }
    }
    public List<List<string>> ReadLV(string identifikator, string katastralniUzemi)
    {
        PTV ptv = new(new Config(AppSettings.path, AppSettings.keys));
        return ptv.ReadLV(identifikator, katastralniUzemi);
    }

    public List<List<string>> ReadParcela(string cisloPracely, string katastralniUzemi)
    {
        PTV ptv = new(new Config(AppSettings.path, AppSettings.keys));
        return ptv.ReadParcela(cisloPracely, katastralniUzemi);
    }
    public void ChangePodil(string identifikatorVlastnikaPridani,string identifikatorVlastnikaOdebrani, string katastralniUzemi, string cisloPracely, int podil)
    {
        PTV ptv = new(new Config(AppSettings.path, AppSettings.keys));
        ptv.ChangePodli(identifikatorVlastnikaPridani, identifikatorVlastnikaOdebrani, katastralniUzemi, cisloPracely, podil);
    }

    public static void Import<T>(List<T> data, Type type)
    {
        DBObject obj = (DBObject)Activator.CreateInstance(type, new Config(AppSettings.path, AppSettings.keys));
        switch (obj)
        {
            case DruhPozemku tmp:
                tmp.Import(data as List<DruhPozemku>);
                break;
        }
    }
}