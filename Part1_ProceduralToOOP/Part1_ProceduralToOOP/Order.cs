
using System.Text;

namespace Part1_ProceduralToOOP;

public class Order
{
    private const int MaxLines = 20;

    private readonly List<OrderLine> _orderLines = [];
    // readonly so no one can reassign the field (not losing the reference)
    public int Id { get; private set; }

    public Customer Customer { get; private set; }

    public bool IsPaid { get; private set; }


    public IReadOnlyList<OrderLine> OrderLines => _orderLines;
    // return the orderlines list, so it's used only for iteration and view  


    public DateOnly Date { get; private set; }

    public Order(int id, Customer customer, DateOnly date)
    {
        Id = id;
        Customer = customer;
        Date = date;

    }

    public void AddLine(Product product, int quantity)
    {
        if (IsPaid)
            throw new InvalidOperationException("Order already paid, cannot add lines to it");

        if (OrderLines.Count >= MaxLines)
            throw new InvalidOperationException("no more lines can be added to this order");

        product.ReduceStock(quantity);
        _orderLines.Add(new OrderLine(product, quantity));
    }

    public decimal CalculateTotal()
    {
        decimal subTotal = 0m;
        foreach (var line in OrderLines)
        {
            subTotal += line.LineTotal;
        }

        return subTotal * (1 - Customer.DiscountRate);
    }

    public void MarkAsPaid()
    {
        if (_orderLines.Count == 0)
            throw new InvalidOperationException("nothing in this order to be paid");

        IsPaid = true;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"=== ORDER #{Id} ===");
        sb.AppendLine($"Date: {Date:yyyy-MM-dd}");
        sb.AppendLine($"Customer: {Customer.Name} (#{Customer.Id})");
        sb.AppendLine($"Paid: {(IsPaid ? "yes" : "no")}");
        sb.AppendLine("Lines:");

        foreach (var line in _orderLines)
            sb.AppendLine($"  {line}");

        sb.Append($"TOTAL: {CalculateTotal():F2}");
        return sb.ToString();
    }
}
