using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.Entities;
using OnlineStoreAPI.Requests;
using OnlineStoreAPI.Storage;

namespace OnlineStoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ApplicationStorage _applicationStorage;

    public ProductController(ApplicationStorage applicationStorage)
    {
        _applicationStorage = applicationStorage;
    }
    
    [HttpPost("[action]")]
    public IActionResult UpdateProduct([FromBody]UpdateProductRequest request)
    {
        var id = Guid.Parse(request.productId);
        
        var product = _applicationStorage.Products.Where(p => p.id == id).First();

        if (product == null)
        {
            return NotFound($"Product of ID {id} not found");
        }

        if (!string.IsNullOrWhiteSpace(request.description))
        {
            product.description = request.description;
        }

        if (!string.IsNullOrWhiteSpace(request.name))
        {
            product.name = request.name;
        }

        if (request.price >= 0f)
        {
            product.price = request.price;
        }
        
        return Ok(product);
    }

    [HttpPost("[action]")]
    public IActionResult WriteReview([FromBody] CreateReviewRequest request)
    {
        var id = Guid.Parse(request.productId);
        
        var product = _applicationStorage.Products.Where(p => p.id == id).First();

        if (product == null)
        {
            return NotFound($"Product of ID {id} not found");
        }

        var review = new Review()
        {
            id = Guid.NewGuid(),
            content = request.reviewContent
        };
        
        product.reviews.Add(review);
        
        return Ok(review);
    }
}