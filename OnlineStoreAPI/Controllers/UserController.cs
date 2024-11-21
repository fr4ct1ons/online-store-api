using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.Entities;
using OnlineStoreAPI.Requests;
using OnlineStoreAPI.Storage;

namespace OnlineStoreAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ApplicationStorage _applicationStorage;

    public UserController(ApplicationStorage applicationStorage)
    {
        _applicationStorage = applicationStorage;
    }

    [HttpGet("[action]")]
    public IActionResult GetUser(string userId)
    {
        Guid id = Guid.Parse(userId);

        var user = _applicationStorage.Users.Where(u => u.id == id).First();

        if (user == null)
        {
            return NotFound($"User with ID {userId} not found");
        }

        return Ok(user);
    }

    [HttpPost("[action]")]
    public IActionResult CreateUser([FromBody]CreateUserRequest request)
    {
        Guid id = Guid.NewGuid();
        User newUser = new User()
        {
            id = id,
            name = request.name,
            purchases = new List<Purchase>()
        };

        _applicationStorage.Users.Add(newUser);

        return Ok(newUser);
    }

    [HttpDelete("[action]")]
    public IActionResult DeleteUser(string userId)
    {
        Guid id = Guid.Parse(userId);

        User user = _applicationStorage.Users.Where(u => u.id == id).First();

        if (user == null)
        {
            return NotFound($"User with ID {userId} not found");
        }
        else
        {
            _applicationStorage.Users.Remove(user);
        }

        return NoContent();
    }

    [HttpPost("[action]")]
    public IActionResult MakePurchase([FromBody] MakePurchaseRequest request)
    {
        Guid userId = Guid.Parse(request.userId);
        List<Guid> productIds = request.productIds.Select(s => Guid.Parse(s)).ToList();
        
        List<Product> products = _applicationStorage.Products.Where(p => productIds.Contains(p.id)).ToList();
        if (products.Count == 0)
        {
            return NotFound($"Product with ID {request.productIds.Count} not found");
        }

        var user = _applicationStorage.Users.Where(u => u.id == userId).First();
        if (user == null)
        {
            return NotFound($"User with ID {userId} not found");
        }
        
        Purchase purchase = new Purchase()
        {
            id = Guid.NewGuid(),
            products = products
        };
        
        user.purchases.Add(purchase);

        return Ok(purchase);
    }

}