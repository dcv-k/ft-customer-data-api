using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class CustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly AppDbContext _dbContext;

    public void InsertCustomer(CustomerDTO customerDto)
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

    public CustomerService(ILogger<CustomerService> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public List<Customer> GetCustomers()
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

    public string UpdateCustomer(CustomerUpdateDTO dto)
    {
        if (dto == null)
        {
            return "Invalid data.";
        }

        var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == dto.Id);
        if (customer == null)
        {
            return "Customer not found.";
        }

        if (!string.IsNullOrWhiteSpace(dto.Name))
        {
            customer.Name = dto.Name;
        }

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            customer.Email = dto.Email;
        }

        if (!string.IsNullOrWhiteSpace(dto.Phone))
        {
            customer.Phone = dto.Phone;
        }

        _dbContext.SaveChanges();

        return "Update Successful!";
    }

    public string CalculateDistance(string id, string lon, string lat)
    {
        var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);

        double customerLongitude = Convert.ToDouble(customer.Longitude);
        double customerLatitude = Convert.ToDouble(customer.Latitude);

        double.TryParse(lon, out double longitude);
        double.TryParse(lat, out double latitude);

        double R = 6371;

        double lat1Rad = Math.PI * customerLatitude / 180.0;
        double lon1Rad = Math.PI * customerLongitude / 180.0;
        double lat2Rad = Math.PI * latitude / 180.0;
        double lon2Rad = Math.PI * longitude / 180.0;

        double deltaLat = lat2Rad - lat1Rad;
        double deltaLon = lon2Rad - lon1Rad;

        double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                   Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distance = R * c;

        return $"Distance between customer {customer.Name} and provided location: {distance:F2} kilometers";
    }

    public List<Customer> SearchCustomers(string text)
    {
        var customers = _dbContext.Customers
            .Include(c => c.Address)
            .Where(c =>
                c.Id.Contains(text) ||
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

    public List<Customer> GetCustomersByZipCode()
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
}
