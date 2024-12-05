using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.Entities;
using OnlineStoreAPI.Requests;
using OnlineStoreAPI.Storage;

namespace OnlineStoreAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreController : ControllerBase
{
    private readonly ApplicationStorage _applicationStorage;

    public StoreController(ApplicationStorage applicationStorage)
    {
        _applicationStorage = applicationStorage;
    }
    
    [HttpGet("[action]")]
    public IActionResult GetStore([Required] string userId)
    {
        Guid id = Guid.Parse(userId);

        var store = _applicationStorage.Stores.Where(u => u.id == id).First();

        if (store == null)
        {
            return NotFound($"User with ID {userId} not found");
        }

        return Ok(store);
    }

    [HttpPost("[action]")]
    public IActionResult LoginStore([FromBody] LoginRequest request)
    {
        var store = _applicationStorage.Stores.First(s => s.name == request.username);

        if (store == null)
        {
            return NotFound($"Store with name {request.username} not found");
        }

        if (store.password != request.password)
        {
            return Unauthorized();
        }
        
        return Ok(store);
    }

    [HttpGet("[action]")]
    public IActionResult GetStores()
    {
        return Ok(_applicationStorage.Stores);
    }

    [HttpPost("[action]")]
    public IActionResult CreateStore([FromBody] CreateStoreRequest request)
    {
        if (_applicationStorage.Stores.Any(s => s.name == request.name))
        {
            return Conflict($"Store with name {request.name} already exists");
        }
        
        string name = request.name;
        string description = request.description;

        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(description))
        {
            return BadRequest("Please provide a name and a description");
        }

        var store = new StoreOwner()
        {
            name = name,
            description = description,
            id = Guid.NewGuid(),
            products = new List<Product>(),
            password = request.password
        };
        
        _applicationStorage.Stores.Add(store);

        return Ok(store);
    }

    [HttpDelete("[action]")]
    public IActionResult DeleteStore([Required] Guid id)
    {
        var store = _applicationStorage.Stores.Where(u => u.id == id).First();

        if (store == null)
        {
            return NotFound($"User with ID {id} not found");
        }
        
        _applicationStorage.Stores.Remove(store);

        return NoContent();
    }

    [HttpPost("[action]")]
    public IActionResult CreateProduct([FromBody]CreateProductRequest request)
    {
        Guid storeGuid = Guid.Parse(request.storeId);
        
        var store = _applicationStorage.Stores.Where(u => u.id == storeGuid).First();
        if (store == null)
        {
            return NotFound($"Store with ID {storeGuid} not found");
        }

        var product = new Product()
        {
            name = request.name,
            description = request.description,
            price = request.price,
            id = Guid.NewGuid(),
        };
        
        store.products.Add(product);
        _applicationStorage.Products.Add(product);
        
        return Ok(product);
    }
    
    [HttpPost("[action]")]
    public IActionResult UpdateStore([FromBody] UpdateStoreRequest request)
    {
        try
        {
            var id = Guid.Parse(request.storeId);
            var store = _applicationStorage.Stores.Where(s => s.id == id).First();

            if (store == null)
            {
                return NotFound($"Store with ID {request.storeId} not found");
            }

            if (!string.IsNullOrWhiteSpace(request.description))
            {
                store.description = request.description;
            }

            if (!string.IsNullOrWhiteSpace(request.name))
            {
                store.name = request.name;
            }

            return Ok(store);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("[action]")]
    public IActionResult GetRandomStores(int? count)
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        var stores = _applicationStorage.Stores.OrderBy(s => rnd.Next()).Take(count ?? 5);

        return Ok(stores);
    }

    [HttpGet("[action]")]
    public IActionResult SearchStores(string query)
    {
        var stores = _applicationStorage.Stores.Where(s => s.name.ToLower().Contains(query.ToLower()));

        return Ok(stores);
    }
}