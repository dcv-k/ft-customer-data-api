using Newtonsoft.Json;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;

public class DataSeeder
{
    private readonly AppDbContext _dbContext;
    private readonly CustomerService _customerService;

    public DataSeeder(AppDbContext dbContext, CustomerService customerService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
    }

    public void Seed()
    {
        if (!_dbContext.Customers.Any())
        {
            var jsonFilePath = "./Data/CustomerData.json";
            var accountDataPath = "./Data/AccountData.json";

            if (System.IO.File.Exists(jsonFilePath) && System.IO.File.Exists(accountDataPath))
            {
                List<CustomerDTO> customerData = ReadJSON<CustomerDTO>(jsonFilePath);

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
                        string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                        var user = new User
                        {
                            Username = userDto.Username,
                            Password = passwordHash,
                            Type = userDto.Type
                        };

                        _dbContext.Users.Add(user);
                    }

                    _dbContext.SaveChanges();
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
