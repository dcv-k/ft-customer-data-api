public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Customer> Customers { get; set; }

    public static implicit operator string(Tag v)
    {
        throw new NotImplementedException();
    }
}