namespace OnlineStoreAPI.Requests;

public class UpdateProductRequest
{
    public string productId { get; set; }
    public string description { get; set; }
    public string name { get; set; }
    public float price { get; set; }
}