using Microsoft.EntityFrameworkCore;
using System.Linq;

public class CustomerDAO : ICustomerDAO
{
    private readonly ApplicationDbContext _dbContext;
    
    public CustomerDAO(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(CustomerDTO customerDto)
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
        _dbContext.SaveChanges();
    }

    public List<Customer> Read()
    {
        var customers = _dbContext.Customers
            .Select(c => new Customer
            {
                Id = c.Id,
                Name = c.Name,
                Age = c.Age,
                EyeColor = c.EyeColor,
                Gender = c.Gender,
                Company = c.Company,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                About = c.About,
                Registered = c.Registered,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                Tags = c.Tags,
            })
            .ToList();

        return customers;
    }

    public Customer Read(string id)
    {
        var customers = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
        return customers;
    }

    public List<Customer> ReadOrderByZipCode()
    {
        var customers = _dbContext.Customers
        .Include(c => c.Address)
        .Include(c => c.Tags)
        .OrderBy(c => c.Address.ZipCode)
        .Select(c => new Customer
        {
            Id = c.Id,
            Name = c.Name,
            Age = c.Age,
            EyeColor = c.EyeColor,
            Gender = c.Gender,
            Company = c.Company,
            Email = c.Email,
            Phone = c.Phone,
            Address = c.Address,
            About = c.About,
            Registered = c.Registered,
            Latitude = c.Latitude,
            Longitude = c.Longitude,
            Tags = c.Tags
        })
        .ToList();

        return customers;
    }

    public void Update(CustomerUpdateDTO dto)
    {
        var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == dto.Id);

        customer.Name = dto.Name;
        customer.Email = dto.Email;
        customer.Phone = dto.Phone;

        _dbContext.SaveChanges();
    }

    public List<Customer> Search(string text)
    {
        var customers = _dbContext.Customers
            .Include(c => c.Address)
            .Where(c =>
                c.Name.Contains(text) ||
                c.Age.ToString().Contains(text) ||
                c.EyeColor.Contains(text) ||
                c.Gender.Contains(text) ||
                c.Company.Contains(text) ||
                c.Email.Contains(text) ||
                c.Phone.Contains(text) ||
                c.Address.Street.Contains(text) ||
                c.Address.City.Contains(text) ||
                c.Address.State.Contains(text) ||
                c.Address.ZipCode.ToString().Contains(text) ||
                c.About.Contains(text) ||
                c.Registered.Contains(text) ||
                c.Latitude.ToString().Contains(text) ||
                c.Longitude.ToString().Contains(text) ||
                c.Tags.Any(tag => tag.Name.Contains(text))
            )
            .Select(c => new Customer
            {
                Id = c.Id,
                Name = c.Name,
                Age = c.Age,
                EyeColor = c.EyeColor,
                Gender = c.Gender,
                Company = c.Company,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                About = c.About,
                Registered = c.Registered,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                Tags = c.Tags,
            })
            .ToList();

        return customers;
    }
}