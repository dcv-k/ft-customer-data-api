public interface ICustomerService
{
    void InsertCustomer(CustomerDTO customerDto);
    List<Customer> GetCustomers();
    string UpdateCustomer(CustomerUpdateDTO dto);
    string CalculateDistance(string id, string lon, string lat);
    List<Customer> SearchCustomers(string text);
    List<Customer> GetCustomersByZipCode();
}