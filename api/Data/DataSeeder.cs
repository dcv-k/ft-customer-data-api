using Newtonsoft.Json;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;

public class DataSeeder
{
    private readonly AppDbContext _dbContext;

    public DataSeeder(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (!_dbContext.Customers.Any())
        {
            var jsonFilePath = "./Data/CustomerData.json";
            var accountDataPath = "./Data/AccountData.json";

            if (System.IO.File.Exists(jsonFilePath) && System.IO.File.Exists(accountDataPath))
            {
                string jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var customerData = JsonConvert.DeserializeObject<List<CustomerDTO>>(jsonData);

                if (customerData != null)
                {
                    foreach (var customerDto in customerData)
                    {
                        var customer = new Customer
                        {
                            Id = customerDto._id,
                            Name = customerDto.name,
                            Age = customerDto.age,
                            EyeColor = customerDto.eyeColor ?? "",
                            Gender = customerDto.gender ?? "",
                            Company = customerDto.company ?? "",
                            Email = customerDto.email ?? "",
                            Phone = customerDto.phone,
                            Address = new Address
                            {
                                Number = customerDto.address.number,
                                Street = customerDto.address.street,
                                City = customerDto.address.city,
                                State = customerDto.address.state,
                                ZipCode = customerDto.address.zipcode
                            },
                            About = customerDto.about ?? "",
                            Registered = customerDto.registered ?? "",
                            Latitude = customerDto.latitude,
                            Longitude = customerDto.longitude,
                            Tags = customerDto.tags.Select(tagName => new Tag { Name = tagName }).ToList()
                        };

                        _dbContext.Customers.Add(customer);
                    }

                    _dbContext.SaveChanges();
                }

                string accountData = System.IO.File.ReadAllText(accountDataPath);
                var userData = JsonConvert.DeserializeObject<List<UserDTO>>(accountData);

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
}
