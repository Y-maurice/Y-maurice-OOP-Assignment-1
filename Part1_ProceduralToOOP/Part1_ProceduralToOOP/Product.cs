namespace Part1ProceduralToOOP;

public class Product
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public decimal Price { get; private set; }

    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(price);
        ArgumentOutOfRangeException.ThrowIfNegative(stock);

        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }
    private bool HasStock(int quantity) => quantity <= Stock;


    public void ReduceStock(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);


        if (!HasStock(quantity))
            throw new InvalidOperationException(
                $"Not enough stock for {Name}: requested {quantity}, only {Stock} available.");

        Stock -= quantity;
    }


}
