namespace Part1_ProceduralToOOP;

public class Customer
{
    private const decimal Discount = 0.1m;
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string City { get; private set; }

    public bool IsVip { get; private set; }

    public decimal DiscountRate => IsVip ? Discount : 0m;




    public Customer(int id, string name, string email, string city, bool isVip = false)
    {
        Id = id;
        Name = name;
        Email = email;
        City = city;
        IsVip = isVip;


    }

    public override string ToString() =>
        $"#{Id}  {Name}  <{Email}>  {City}  vip={(IsVip ? "yes" : "no")}";



}
