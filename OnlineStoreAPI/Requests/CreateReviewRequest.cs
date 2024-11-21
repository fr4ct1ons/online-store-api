namespace OnlineStoreAPI.Requests;

public class CreateReviewRequest
{
    public string productId { get; set; }
    public string reviewContent { get; set; }
}