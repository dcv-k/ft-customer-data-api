public class Address
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int ZipCode { get; set; }
    public string CustomerId { get; set; }
    public Customer Customer { get; set; }
}