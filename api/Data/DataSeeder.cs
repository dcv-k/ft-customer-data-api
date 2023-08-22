using Newtonsoft.Json;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;

public class DataSeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICustomerService _customerService;
    private readonly IConfiguration _configuration;

    public DataSeeder(IConfiguration configuration, ApplicationDbContext dbContext, ICustomerService customerService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
        _configuration = configuration;
    }

    public void Seed()
    {
        if (!_dbContext.Customers.Any())
        {
            var customerDataPath = "./Data/SeedSource/CustomerData.json";

            if (System.IO.File.Exists(customerDataPath))
            {
                List<CustomerDTO> customerData = ReadJSON<CustomerDTO>(customerDataPath);

                if (customerData != null)
                {
                    foreach (var customerDto in customerData)
                    {
                        _customerService.InsertCustomer(customerDto);
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
