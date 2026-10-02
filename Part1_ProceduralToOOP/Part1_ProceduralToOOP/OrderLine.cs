namespace Part1_ProceduralToOOP;


public class OrderLine
{
    public Product Product { get; private set; }

    public int Quantity { get; private set; }

    public decimal LineTotal => Quantity * Product.Price;

    public OrderLine(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"- {Product.Name}  x{Quantity}  @{Product.Price:F2}  = {LineTotal:F2}";
    }
}
