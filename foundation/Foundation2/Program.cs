using System;
using System.Collections.Generic;

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return string.Equals(country, "USA", StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }

    public string GetName()
    {
        return name;
    }

    public Address GetAddress()
    {
        return address;
    }
}

class Product
{
    private string name;
    private string productId;
    private decimal price;
    private int quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public decimal TotalCost()
    {
        return price * quantity;
    }

    public string GetName()
    {
        return name;
    }

    public string GetProductId()
    {
        return productId;
    }
}

class Order
{
    private Customer customer;
    private List<Product> products;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public decimal TotalPrice()
    {
        decimal productTotal = 0;
        foreach (var product in products)
        {
            productTotal += product.TotalCost();
        }
        decimal shippingCost = customer.LivesInUSA() ? 5.00m : 35.00m;
        return productTotal + shippingCost;
    }

    public string PackingLabel()
    {
        var labels = new List<string>();
        foreach (var product in products)
        {
            labels.Add($"{product.GetName()} (ID: {product.GetProductId()})");
        }
        return string.Join("\n", labels);
    }

    public string ShippingLabel()
    {
        return $"{customer.GetName()}\n{customer.GetAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Elm St", "Springfield", "IL", "USA");
        Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create products
        Product product1 = new Product("Widget A", "001", 10.00m, 2);  // Total cost: $20.00
        Product product2 = new Product("Widget B", "002", 15.00m, 1);  // Total cost: $15.00
        Product product3 = new Product("Gadget C", "003", 7.50m, 4);   // Total cost: $30.00

        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product3);

        // Display results for Order 1
        Console.WriteLine("Order 1 Packing Label:");
        Console.WriteLine(order1.PackingLabel());
        Console.WriteLine("\nOrder 1 Shipping Label:");
        Console.WriteLine(order1.ShippingLabel());
        Console.WriteLine($"\nOrder 1 Total Price: ${order1.TotalPrice():F2}");

        // Display results for Order 2
        Console.WriteLine("\nOrder 2 Packing Label:");
        Console.WriteLine(order2.PackingLabel());
        Console.WriteLine("\nOrder 2 Shipping Label:");
        Console.WriteLine(order2.ShippingLabel());
        Console.WriteLine($"\nOrder 2 Total Price: ${order2.TotalPrice():F2}");
    }
}