namespace OnlineStoreAPI.Entities;

public class User
{
    public string username { get; set; }
    public string password { get; set; }
    public Guid id{ get; set; }
    public List<Purchase> purchases { get; set; }
}