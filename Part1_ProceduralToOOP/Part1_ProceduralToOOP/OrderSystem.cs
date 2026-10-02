namespace Part1_ProceduralToOOP;

public class OrderSystem
{
    private readonly List<Customer> _customers = [];
    private readonly List<Product> _products = [];
    private readonly List<Order> _orders = [];

    public IReadOnlyList<Customer> Customers => _customers;
    public IReadOnlyList<Product> Products => _products;
    public IReadOnlyList<Order> Orders => _orders;

    public void AddCustomer(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        if (FindCustomerById(customer.Id) is not null)
            throw new InvalidOperationException($"Customer id {customer.Id} already exists.");

        _customers.Add(customer);
    }

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (FindProductById(product.Id) is not null)
            throw new InvalidOperationException($"Product id {product.Id} already exists.");

        _products.Add(product);
    }

    public void CreateOrder(int orderId, int customerId, DateOnly date)
    {
        if (FindOrderById(orderId) is not null)
            throw new InvalidOperationException($"Order id {orderId} already exists.");

        var customer = GetCustomer(customerId);
        _orders.Add(new Order(orderId, customer, date));
    }

    public void AddLineToOrder(int orderId, int productId, int quantity)
    {
        var order = GetOrder(orderId);
        var product = GetProduct(productId);
        order.AddLine(product, quantity);
    }

    public void MarkOrderPaid(int orderId) => GetOrder(orderId).MarkAsPaid();

    public decimal TotalSalesPaidOnly()
    {
        decimal sum = 0m;
        foreach (var order in _orders)
        {
            if (order.IsPaid)
                sum += order.CalculateTotal();
        }
        return sum;
    }

    public Order GetOrder(int id) =>
        FindOrderById(id) ?? throw new InvalidOperationException($"Order id {id} not found.");

    private Customer GetCustomer(int id) =>
        FindCustomerById(id) ?? throw new InvalidOperationException($"Customer id {id} not found.");

    private Product GetProduct(int id) =>
        FindProductById(id) ?? throw new InvalidOperationException($"Product id {id} not found.");

    private Customer? FindCustomerById(int id)
    {
        foreach (var customer in _customers)
        {
            if (customer.Id == id)
                return customer;
        }
        return null;
    }

    private Product? FindProductById(int id)
    {
        foreach (var product in _products)
        {
            if (product.Id == id)
                return product;
        }
        return null;
    }

    private Order? FindOrderById(int id)
    {
        foreach (var order in _orders)
        {
            if (order.Id == id)
                return order;
        }
        return null;
    }
}