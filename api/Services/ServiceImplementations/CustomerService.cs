using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class CustomerService : ICustomerService
{
    private readonly ICustomerDAO _customerDAO; 

    public CustomerService(ICustomerDAO customerDAO)
    {
        _customerDAO = customerDAO;
    }

    public void InsertCustomer(CustomerDTO customerDTO)
    {
        _customerDAO.Create(customerDTO);
    }

    public List<Customer> GetCustomers()
    {
        return _customerDAO.Read();
    }

    public string UpdateCustomer(CustomerUpdateDTO customerUpdateDTO)
    {
        _customerDAO.Update(customerUpdateDTO);
        return "Update Successful!";
    }

    public string CalculateDistance(string id, string lon, string lat)
    {
        var customer = _customerDAO.Read(id);

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
        return _customerDAO.Search(text);
    }

    public List<Customer> GetCustomersByZipCode()
    {
        return _customerDAO.ReadOrderByZipCode();
    }
}
