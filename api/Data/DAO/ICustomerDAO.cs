public interface ICustomerDAO
{
    void Create(CustomerDTO customerDto);
    List<Customer> Read();
    public Customer Read(string id);
    string Update(CustomerUpdateDTO dto);
    public List<Customer> Search(string text)
}