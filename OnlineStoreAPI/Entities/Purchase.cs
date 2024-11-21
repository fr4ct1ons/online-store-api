namespace OnlineStoreAPI.Entities;

public class Purchase
{
    public Guid id { get; set; }
    public List<Product> products { get; set; }
}