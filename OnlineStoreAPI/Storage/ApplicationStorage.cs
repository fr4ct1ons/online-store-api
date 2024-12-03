using System.Xml;
using Newtonsoft.Json;
using OnlineStoreAPI.Entities;
using Formatting = Newtonsoft.Json.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OnlineStoreAPI.Storage;

public class ApplicationStorage
{
    public List<User> Users { get; set; } = new();
    public List<StoreOwner> Stores { get; set; } = new();
    public List<Product> Products { get; set; } = new();

    private const string FilePrefix = "Data/";
    
    static string UsersFileName => FilePrefix + "users.json";
    static string StoresFileName => FilePrefix + "stores.json";
    static string ProducstFileName => FilePrefix + "products.json";

    public static List<User> UsersPointer;
    public static List<StoreOwner> StoresPointer;
    public static List<Product> ProductsPointer;
    static bool Setup = false;

    public ApplicationStorage()
    {
        Console.WriteLine("Setup Application Storage");
        
        try
        {
            if (File.Exists(UsersFileName))
            {
                string content = File.ReadAllText(UsersFileName);
                Users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }
        catch (Exception e)
        { }

        try
        {
            if (File.Exists(StoresFileName))
            {
                string content = File.ReadAllText(StoresFileName);
                Stores = JsonSerializer.Deserialize<List<StoreOwner>>(content);
            }
        }
        catch (Exception e)
        { }
        
        try
        {
            /*
            if (File.Exists(ProducstFileName))
            {
                string content = File.ReadAllText(ProducstFileName);
                Products = JsonSerializer.Deserialize<List<Product>>(content);
            }
            
            */

            foreach (StoreOwner store in Stores)
            {
                Products.AddRange(store.products);
            }
        }
        catch (Exception e)
        { }
        
        UsersPointer = Users;
        StoresPointer = Stores;
        ProductsPointer = Products;
        Setup = true;
    }

    public static void SaveToJsonFile()
    {
        if (!Setup)
        {
            return;
        }
        Console.WriteLine("Application Storage Saver");
        try
        {
            File.WriteAllText(UsersFileName, JsonConvert.SerializeObject(UsersPointer, Formatting.Indented));
            File.WriteAllText(StoresFileName, JsonConvert.SerializeObject(StoresPointer, Formatting.Indented));
            File.WriteAllText(ProducstFileName, JsonConvert.SerializeObject(ProductsPointer, Formatting.Indented));
            Console.WriteLine("All done!");
        }
        catch (Exception e)
        { }
    }
}