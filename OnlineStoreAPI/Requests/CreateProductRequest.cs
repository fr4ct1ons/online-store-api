namespace OnlineStoreAPI.Requests;

public class CreateProductRequest
{
    public string storeId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public float price { get; set; }
}