namespace OnlineStoreAPI.Requests;

public class MakePurchaseRequest
{
    public string userId { get; set; }
    public List<string> productIds { get; set; }
}