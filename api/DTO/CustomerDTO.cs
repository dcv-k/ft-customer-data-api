public class CustomerDTO
{
    public string _id { get; set; } = null!;
    public int index { get; set; }
    public int age { get; set; }
    public string? eyeColor { get; set; }
    public string name { get; set; } = null!;
    public string? gender { get; set; }
    public string? company { get; set; }
    public string? email { get; set; }
    public string phone { get; set; } = null!;
    public address address { get; set; } = null!;
    public string? about { get; set; }
    public string? registered { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string[] tags { get; set; } = Array.Empty<string>();
}

public class address
{
    public int number { get; set; }
    public string street { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public int zipcode { get; set; }
}