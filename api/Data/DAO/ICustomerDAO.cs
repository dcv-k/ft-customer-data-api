public interface ICustomerDAO
{
    void Create(CustomerDTO customerDto);
    List<Customer> Read();
    Customer Read(string id);
    void Update(CustomerUpdateDTO dto);
    List<Customer> Search(string text);
    List<Customer> ReadOrderByZipCode();
}