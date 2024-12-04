namespace OnlineStoreAPI.Entities;

public class Product
{
    public string name { get; set; }
    public Guid id { get; set; }
    public string description { get; set; }
    public float price { get; set; }
    public List<Review> reviews { get; set; } = new List<Review>();
}