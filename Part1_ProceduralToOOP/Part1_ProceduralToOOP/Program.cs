namespace Part1_ProceduralToOOP;

public class Program
{
    public static void Main()
    {


        var system = new OrderSystem();
        var menu = new Menu(system);

        SeedSampleData(system);

        menu.PrintCustomers();
        menu.PrintProducts();
        menu.PrintAllOrders();

        Console.WriteLine();
        Console.Write("Paid sales total: ");
        Console.WriteLine($"{system.TotalSalesPaidOnly():F2}");

        menu.Run();
    }

    private static void SeedSampleData(OrderSystem system)
    {
        system.AddCustomer(new Customer(1, "Mona Ali", "mona@example.com", "Cairo", true));
        system.AddCustomer(new Customer(2, "Omar Hassan", "omar@example.com", "Alexandria", false));
        system.AddCustomer(new Customer(3, "Sara Nabil", "sara@example.com", "Giza", false));

        system.AddProduct(new Product(101, "USB Cable", 50m, 100));
        system.AddProduct(new Product(102, "Wireless Mouse", 250m, 40));
        system.AddProduct(new Product(103, "Mechanical Keyboard", 1200m, 15));
        system.AddProduct(new Product(104, "Laptop Stand", 400m, 25));
    }


}