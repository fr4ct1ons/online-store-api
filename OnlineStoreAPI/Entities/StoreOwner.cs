namespace OnlineStoreAPI.Entities;

public class StoreOwner
{
    public string name { get; set; }
    public Guid id { get; set; }
    public string description { get; set; }
    public List<Product> products { get; set; }
}