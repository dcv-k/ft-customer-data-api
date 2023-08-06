using Newtonsoft.Json;

public class DataSeeder
{
    private readonly AppDbContext _dbContext;

    public DataSeeder(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public void Seed()
    {
        if (!_dbContext.Customers.Any())
        {
            var jsonFilePath = "./Data/UserData.json";
            
            if (System.IO.File.Exists(jsonFilePath))
            {
                string jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var userData = JsonConvert.DeserializeObject<List<CustomerDTO>>(jsonData);

                if (userData != null)
                {
                    foreach (var user in userData)
                    {
                        var customer = new Customer
                        {
                            Id = user._id,
                            Name = user.name,
                            Age = user.age,
                            EyeColor = user.eyeColor ?? "",
                            Gender = user.gender ?? "",
                            Company = user.company ?? "",
                            Email = user.email ?? "",
                            Phone = user.phone,
                            Address = new Address
                            {
                                Number = user.address.number,
                                Street = user.address.street,
                                City = user.address.city,
                                State = user.address.state,
                                ZipCode = user.address.zipcode
                            },
                            About = user.about ?? "",
                            Registered = user.registered ?? "",
                            Latitude = user.latitude,
                            Longitude = user.longitude,
                            Tags = user.tags.Select(tagName => new Tag { Name = tagName }).ToList()
                        };

                        _dbContext.Customers.Add(customer);
                    }

                    _dbContext.SaveChanges();
                }
            }
        }
    }
}