using Newtonsoft.Json;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;

public class DataSeeder
{
    private readonly AppDbContext _dbContext;
    private readonly CustomerService _customerService;
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public DataSeeder(IConfiguration configuration, AppDbContext dbContext, CustomerService customerService, UserService userService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
        _userService = userService;
        _configuration = configuration;
    }

    public void Seed()
    {
        if (!_dbContext.Customers.Any())
        {
            var customerDataPath = _configuration.GetSection("AppSettings:CustomerDataPath").Value;
            var accountDataPath = _configuration.GetSection("AppSettings:AccountDataPath").Value;

            if (System.IO.File.Exists(customerDataPath) && System.IO.File.Exists(accountDataPath))
            {
                List<CustomerDTO> customerData = ReadJSON<CustomerDTO>(customerDataPath);

                if (customerData != null)
                {
                    foreach (var customerDto in customerData)
                    {
                        _customerService.InsertCustomer(customerDto);
                    }
                }

                List<UserDTO> userData = ReadJSON<UserDTO>(accountDataPath);

                if (userData != null)
                {
                    foreach (var userDto in userData)
                    {
                        _userService.RegisterUser(userDto);
                    }
                }
            }
        }
    }

    public List<T> ReadJSON<T>(string path)
    {
        string jsonData = System.IO.File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<T>>(jsonData);
    }
}
