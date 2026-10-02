namespace Part1_ProceduralToOOP;

public class Menu
{
    private readonly OrderSystem _system;

    public Menu(OrderSystem system)
    {
        _system = system;
    }


    public void Run()
    {
        int choice = -1;
        while (choice != 0)
        {
            PrintMenu();
            choice = Helpers.ReadInt("Choice: ");

            try
            {
                switch (choice)
                {
                    case 1: PrintCustomers(); break;
                    case 2: PrintProducts(); break;
                    case 3: PrintAllOrders(); break;
                    case 4: PrintOneOrder(); break;
                    case 5: AddCustomer(); break;
                    case 6: AddProduct(); break;
                    case 7: CreateOrder(); break;
                    case 8: AddLineToOrder(); break;
                    case 9: MarkOrderPaid(); break;
                    case 10: ShowPaidSalesTotal(); break;
                    case 0: Console.WriteLine("Bye."); break;
                    default: Console.WriteLine("Unknown choice."); break;
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1) show all customers");
        Console.WriteLine("2) show all products");
        Console.WriteLine("3) show all orders");
        Console.WriteLine("4) show order by id");
        Console.WriteLine("5) Add customer ");
        Console.WriteLine("6) Add product ");
        Console.WriteLine("7) Create order");
        Console.WriteLine("8) Add line to order");
        Console.WriteLine("9) Mark order paid");
        Console.WriteLine("10) Show paid sales total");
        Console.WriteLine("0) Exit");
    }

    public void PrintCustomers()
    {
        Console.WriteLine($"\n=== CUSTOMERS ({_system.Customers.Count}) ===");
        foreach (var customer in _system.Customers)
            Console.WriteLine(customer);
    }

    public void PrintProducts()
    {
        Console.WriteLine($"\n=== PRODUCTS ({_system.Products.Count}) ===");
        foreach (var product in _system.Products)
            Console.WriteLine(product);
    }

    public void PrintAllOrders()
    {
        Console.WriteLine($"\n=== ALL ORDERS ({_system.Orders.Count}) ===");
        foreach (var order in _system.Orders)
        {
            Console.WriteLine();
            Console.WriteLine(order);
        }
    }

    private void AddCustomer()
    {
        int id = Helpers.ReadInt("Customer id: ");
        string name = Helpers.ReadText("Name: ");
        string email = Helpers.ReadText("Email: ");
        string city = Helpers.ReadText("City: ");
        bool isVip = Helpers.ReadYesNo("VIP customer? (y/n): ");

        _system.AddCustomer(new Customer(id, name, email, city, isVip));
        Console.WriteLine("Customer added.");
    }

    private void AddProduct()
    {
        int id = Helpers.ReadInt("Product id: ");
        string name = Helpers.ReadText("Name: ");
        decimal price = Helpers.ReadDecimal("Price: ");
        int stock = Helpers.ReadInt("Stock: ");

        _system.AddProduct(new Product(id, name, price, stock));
        Console.WriteLine("Product added.");
    }

    public void ShowPaidSalesTotal()
    {
        Console.WriteLine($"Paid sales total: {_system.TotalSalesPaidOnly():F2}");
    }

    private void PrintOneOrder()
    {
        int orderId = Helpers.ReadInt("Order id: ");
        Console.WriteLine();
        Console.WriteLine(_system.GetOrder(orderId));
    }

    private void CreateOrder()
    {
        int orderId = Helpers.ReadInt("Order id: ");
        int customerId = Helpers.ReadInt("Customer id: ");
        DateOnly date = Helpers.ReadDate("Date (YYYY-MM-DD): ");
        _system.CreateOrder(orderId, customerId, date);
    }

    private void AddLineToOrder()
    {
        int orderId = Helpers.ReadInt("Order id: ");
        int productId = Helpers.ReadInt("Product id: ");
        int quantity = Helpers.ReadInt("Quantity: ");
        _system.AddLineToOrder(orderId, productId, quantity);
    }

    private void MarkOrderPaid()
    {
        int orderId = Helpers.ReadInt("Order id: ");
        _system.MarkOrderPaid(orderId);
    }

}
