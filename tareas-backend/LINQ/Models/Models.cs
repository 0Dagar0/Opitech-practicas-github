namespace LinqTraining.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsAvailable { get; set; }


    public void UpdateStock(int quantity)
    {
        if (quantity < 0 && Stock + quantity < 0)
        {
            throw new InvalidOperationException("Cannot reduce stock below zero.");
        }
        Stock += quantity;
        IsAvailable = Stock > 0;
    }
}

public class ProductProjection
{
    public string Name { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public string Category { get; set; } = string.Empty;
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public DateTime HireDate { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty; // "Pending", "Completed", "Cancelled"
    public List<string> Products { get; set; } = new();
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int UnitsSold { get; set; }
}
