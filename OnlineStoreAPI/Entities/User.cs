namespace OnlineStoreAPI.Entities;

public class User
{
    public string name { get; set; }
    public Guid id{ get; set; }
    public List<Purchase> purchases { get; set; }
}