using LinqTraining.Models;

namespace LinqTraining.Data;

public static class SampleData
{
    // ─────────────────────────────────────────────
    // PRODUCTS  (used in the live demo — Block 2)
    // ─────────────────────────────────────────────
    public static List<Product> GetProducts() => new()
    {
        new() { Id = 1,  Name = "Wireless Mouse",       Category = "Electronics",  Price = 29.99m,  Stock = 45,  IsAvailable = true  },
        new() { Id = 2,  Name = "USB-C Hub",            Category = "Electronics",  Price = 49.99m,  Stock = 0,   IsAvailable = false },
        new() { Id = 3,  Name = "Mechanical Keyboard",  Category = "Electronics",  Price = 89.99m,  Stock = 12,  IsAvailable = true  },
        new() { Id = 4,  Name = "Desk Lamp",            Category = "Office",       Price = 34.50m,  Stock = 30,  IsAvailable = true  },
        new() { Id = 5,  Name = "Notebook A5",          Category = "Stationery",   Price = 8.99m,   Stock = 200, IsAvailable = true  },
        new() { Id = 6,  Name = "Ballpoint Pen Set",    Category = "Stationery",   Price = 5.49m,   Stock = 0,   IsAvailable = false },
        new() { Id = 7,  Name = "Monitor Stand",        Category = "Office",       Price = 59.99m,  Stock = 8,   IsAvailable = true  },
        new() { Id = 8,  Name = "Webcam 1080p",         Category = "Electronics",  Price = 74.99m,  Stock = 3,   IsAvailable = true  },
        new() { Id = 9,  Name = "Standing Desk Mat",    Category = "Office",       Price = 44.00m,  Stock = 15,  IsAvailable = true  },
        new() { Id = 10, Name = "Sticky Notes Pack",    Category = "Stationery",   Price = 3.99m,   Stock = 500, IsAvailable = true  },
        new() { Id = 11, Name = "Noise-Cancel Headset", Category = "Electronics",  Price = 129.99m, Stock = 0,   IsAvailable = false },
        new() { Id = 12, Name = "Cable Organizer",      Category = "Office",       Price = 12.00m,  Stock = 60,  IsAvailable = true  },
    };

    // ─────────────────────────────────────────────
    // EMPLOYEES  (Exercise 1 — Block 2)
    // ─────────────────────────────────────────────
    public static List<Employee> GetEmployees() => new()
    {
        new() { Id = 1,  Name = "Valentina Torres",  Department = "Engineering",  Salary = 52000m, IsActive = true,  HireDate = new DateTime(2021, 3, 15) },
        new() { Id = 2,  Name = "Carlos Mendoza",    Department = "Marketing",    Salary = 38000m, IsActive = true,  HireDate = new DateTime(2020, 7, 1)  },
        new() { Id = 3,  Name = "Luisa Fernández",   Department = "Engineering",  Salary = 61000m, IsActive = true,  HireDate = new DateTime(2019, 11, 20) },
        new() { Id = 4,  Name = "Andrés Castillo",   Department = "HR",           Salary = 35000m, IsActive = false, HireDate = new DateTime(2018, 4, 10) },
        new() { Id = 5,  Name = "Mariana Ríos",      Department = "Engineering",  Salary = 47000m, IsActive = true,  HireDate = new DateTime(2022, 1, 8)  },
        new() { Id = 6,  Name = "Felipe Vargas",     Department = "Marketing",    Salary = 41000m, IsActive = false, HireDate = new DateTime(2021, 9, 5)  },
        new() { Id = 7,  Name = "Natalia Gómez",     Department = "Finance",      Salary = 55000m, IsActive = true,  HireDate = new DateTime(2020, 2, 14) },
        new() { Id = 8,  Name = "Diego Ramírez",     Department = "Engineering",  Salary = 68000m, IsActive = true,  HireDate = new DateTime(2017, 6, 30) },
        new() { Id = 9,  Name = "Sofía Herrera",     Department = "HR",           Salary = 33000m, IsActive = true,  HireDate = new DateTime(2023, 3, 1)  },
        new() { Id = 10, Name = "Julián Morales",    Department = "Finance",      Salary = 49000m, IsActive = true,  HireDate = new DateTime(2021, 12, 15) },
        new() { Id = 11, Name = "Camila Ortega",     Department = "Marketing",    Salary = 29000m, IsActive = true,  HireDate = new DateTime(2023, 8, 22) },
        new() { Id = 12, Name = "Sebastián Ruiz",    Department = "Engineering",  Salary = 72000m, IsActive = false, HireDate = new DateTime(2016, 5, 18) },
    };

    // ─────────────────────────────────────────────
    // ORDERS  (Exercise 2 — Block 3)
    // ─────────────────────────────────────────────
    public static List<Order> GetOrders()
    {
        var today = DateTime.Today;
        return new()
        {
            new() { Id = 1,  CustomerName = "Empresa Tech S.A.",    Amount = 1250.00m, Date = today.AddDays(-5),  Status = "Completed",  Products = new() { "Wireless Mouse", "USB-C Hub" } },
            new() { Id = 2,  CustomerName = "Distribuidora Norte",  Amount = 380.50m,  Date = today.AddDays(-10), Status = "Pending",    Products = new() { "Desk Lamp" } },
            new() { Id = 3,  CustomerName = "Empresa Tech S.A.",    Amount = 920.00m,  Date = today.AddDays(-3),  Status = "Completed",  Products = new() { "Webcam 1080p", "Monitor Stand" } },
            new() { Id = 4,  CustomerName = "Soluciones Del Sur",   Amount = 210.75m,  Date = today.AddDays(-45), Status = "Cancelled",  Products = new() { "Notebook A5" } },
            new() { Id = 5,  CustomerName = "Global Services Ltd",  Amount = 4500.00m, Date = today.AddDays(-7),  Status = "Completed",  Products = new() { "Noise-Cancel Headset", "Mechanical Keyboard" } },
            new() { Id = 6,  CustomerName = "Distribuidora Norte",  Amount = 760.00m,  Date = today.AddDays(-20), Status = "Completed",  Products = new() { "Standing Desk Mat", "Cable Organizer" } },
            new() { Id = 7,  CustomerName = "Oficinas Centro",      Amount = 95.00m,   Date = today.AddDays(-2),  Status = "Pending",    Products = new() { "Sticky Notes Pack", "Ballpoint Pen Set" } },
            new() { Id = 8,  CustomerName = "Global Services Ltd",  Amount = 1800.00m, Date = today.AddDays(-15), Status = "Completed",  Products = new() { "Mechanical Keyboard" } },
            new() { Id = 9,  CustomerName = "Soluciones Del Sur",   Amount = 3200.00m, Date = today.AddDays(-1),  Status = "Pending",    Products = new() { "Monitor Stand", "Webcam 1080p", "USB-C Hub" } },
            new() { Id = 10, CustomerName = "Oficinas Centro",      Amount = 450.00m,  Date = today.AddDays(-60), Status = "Cancelled",  Products = new() { "Desk Lamp" } },
            new() { Id = 11, CustomerName = "Empresa Tech S.A.",    Amount = 670.00m,  Date = today.AddDays(-8),  Status = "Pending",    Products = new() { "USB-C Hub" } },
            new() { Id = 12, CustomerName = "Distribuidora Norte",  Amount = 2100.00m, Date = today.AddDays(-4),  Status = "Completed",  Products = new() { "Noise-Cancel Headset", "Wireless Mouse" } },
        };
    }

    // ─────────────────────────────────────────────
    // BOOKS  (Homework dataset)
    // ─────────────────────────────────────────────
    public static List<Book> GetBooks() => new()
    {
        new() { Id = 1,  Title = "Clean Code",                        Author = "Robert C. Martin",   Genre = "Technology",  Year = 2008, Price = 35.99m,  Stock = 12,  UnitsSold = 340 },
        new() { Id = 2,  Title = "The Pragmatic Programmer",          Author = "Andrew Hunt",         Genre = "Technology",  Year = 1999, Price = 32.50m,  Stock = 0,   UnitsSold = 215 },
        new() { Id = 3,  Title = "Design Patterns",                   Author = "Gang of Four",        Genre = "Technology",  Year = 1994, Price = 45.00m,  Stock = 5,   UnitsSold = 189 },
        new() { Id = 4,  Title = "Sapiens",                           Author = "Yuval Noah Harari",   Genre = "History",     Year = 2011, Price = 18.99m,  Stock = 40,  UnitsSold = 512 },
        new() { Id = 5,  Title = "Thinking, Fast and Slow",           Author = "Daniel Kahneman",     Genre = "Psychology",  Year = 2011, Price = 22.00m,  Stock = 25,  UnitsSold = 430 },
        new() { Id = 6,  Title = "Refactoring",                       Author = "Martin Fowler",       Genre = "Technology",  Year = 2018, Price = 38.99m,  Stock = 0,   UnitsSold = 175 },
        new() { Id = 7,  Title = "Atomic Habits",                     Author = "James Clear",         Genre = "Self-Help",   Year = 2018, Price = 16.99m,  Stock = 80,  UnitsSold = 780 },
        new() { Id = 8,  Title = "The 7 Habits of Highly Effective",  Author = "Stephen Covey",       Genre = "Self-Help",   Year = 1989, Price = 14.50m,  Stock = 55,  UnitsSold = 620 },
        new() { Id = 9,  Title = "Introduction to Algorithms",        Author = "Thomas H. Cormen",    Genre = "Technology",  Year = 2009, Price = 59.99m,  Stock = 8,   UnitsSold = 96  },
        new() { Id = 10, Title = "Homo Deus",                         Author = "Yuval Noah Harari",   Genre = "History",     Year = 2015, Price = 20.99m,  Stock = 0,   UnitsSold = 310 },
        new() { Id = 11, Title = "Deep Work",                         Author = "Cal Newport",         Genre = "Self-Help",   Year = 2016, Price = 17.99m,  Stock = 30,  UnitsSold = 395 },
        new() { Id = 12, Title = "The Psychology of Money",           Author = "Morgan Housel",       Genre = "Finance",     Year = 2020, Price = 19.99m,  Stock = 22,  UnitsSold = 460 },
        new() { Id = 13, Title = "Rich Dad Poor Dad",                 Author = "Robert Kiyosaki",     Genre = "Finance",     Year = 1997, Price = 12.99m,  Stock = 0,   UnitsSold = 700 },
        new() { Id = 14, Title = "C# in Depth",                       Author = "Jon Skeet",           Genre = "Technology",  Year = 2019, Price = 42.00m,  Stock = 7,   UnitsSold = 140 },
        new() { Id = 15, Title = "The Lean Startup",                  Author = "Eric Ries",           Genre = "Business",    Year = 2011, Price = 21.00m,  Stock = 18,  UnitsSold = 283 },
        new() { Id = 16, Title = "Zero to One",                       Author = "Peter Thiel",         Genre = "Business",    Year = 2014, Price = 18.50m,  Stock = 0,   UnitsSold = 320 },
        new() { Id = 17, Title = "Educated",                          Author = "Tara Westover",       Genre = "Biography",   Year = 2018, Price = 15.99m,  Stock = 14,  UnitsSold = 210 },
        new() { Id = 18, Title = "Influence",                         Author = "Robert Cialdini",     Genre = "Psychology",  Year = 1984, Price = 17.50m,  Stock = 33,  UnitsSold = 370 },
    };
}
