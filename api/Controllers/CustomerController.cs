using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var customers = _customerService.GetCustomers();
        return new JsonResult(new { customers });
    }

    [HttpPost("update", Name = "update")]
    public IActionResult Update([FromBody] CustomerUpdateDTO dto)
    {
        string result = _customerService.UpdateCustomer(dto);
        if (result == "Update Successful!")
        {
            return Ok(result);
        }
        else if (result == "Invalid data." || result == "Customer not found.")
        {
            return BadRequest(result);
        }
        else
        {
            return NotFound(result);
        }
    }

    [HttpGet("distance", Name = "distance")]
    public IActionResult Distance([FromQuery] string id, string lon, string lat)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat))
        {
            return BadRequest("Missing required parameters.");
        }

        if (!double.TryParse(lon, out double longitude) || !double.TryParse(lat, out double latitude))
        {
            return BadRequest("Invalid longitude or latitude format.");
        }

        string result = _customerService.CalculateDistance(id, lon, lat);

        if (result == null)
        {
            return NotFound("Customer not found.");
        }

        return Ok(result);
    }

    [HttpGet("search", Name = "search")]
    public IActionResult Search([FromQuery] string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("Search text cannot be empty.");
        }

        var customers = _customerService.SearchCustomers(text);
        return Ok(customers);
    }

    [HttpGet("zip", Name = "zip")]
    public IActionResult Zip()
    {
        var customers = _customerService.GetCustomersByZipCode();
        return Ok(customers);
    }
}
