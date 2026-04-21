using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (para permitir conexión desde Angular)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

//  Datos en memoria (simulación de base de datos)
var expensesList = new List<Expense>
{
    new Expense { id = 1, description = "Groceries", amount = 150.75m, date = DateTime.Now.AddDays(-2), category = ExpenseCategory.Food },
    new Expense { id = 2, description = "Gas", amount = 50.00m, date = DateTime.Now.AddDays(-1), category = ExpenseCategory.Transportation },
    new Expense { id = 3, description = "Movie Tickets", amount = 30.00m, date = DateTime.Now.AddDays(-3), category = ExpenseCategory.Entertainment },
    new Expense { id = 4, description = "Electricity Bill", amount = 120.00m, date = DateTime.Now.AddDays(-10), category = ExpenseCategory.Utilities },
    new Expense { id = 5, description = "Doctor Visit", amount = 200.00m, date = DateTime.Now.AddDays(-5), category = ExpenseCategory.Healthcare }
};

// GET - Obtener todos
app.MapGet("/expenses", () =>
{
    return Results.Ok(expensesList);
});

//  POST - Crear nuevo
app.MapPost("/expenses/add", (Expense expense) =>
{
    // Asignar nuevo ID automáticamente
    var newId = expensesList.Count > 0 ? expensesList.Max(e => e.id) + 1 : 1;
    expense.id = newId;
    expensesList.Add(expense);
    return Results.Ok(expense);
});

//  PUT 
app.MapPut("/expenses/update/{id}", (int id, Expense updatedExpense) =>
{
    var expenseToUpdate = expensesList.FirstOrDefault(e => e.id == id);

    if (expenseToUpdate == null)
        return Results.NotFound();

    expenseToUpdate.description = updatedExpense.description;
    expenseToUpdate.amount = updatedExpense.amount;
    expenseToUpdate.date = updatedExpense.date;
    expenseToUpdate.category = updatedExpense.category;

    return Results.Ok(expenseToUpdate);
});

//  DELETE
app.MapDelete("/expenses/delete/{id}", (int id) =>
{
    var expenseToRemove = expensesList.FirstOrDefault(e => e.id == id);

    if (expenseToRemove == null)
        return Results.NotFound();

    expensesList.Remove(expenseToRemove);

    return Results.Ok();
});

app.Urls.Add("http://localhost:5000");
app.Urls.Add("https://localhost:7072");

app.Run();

//  MODELOS 
public class Expense
{
    public int id { get; set; }
    public string description { get; set; } = string.Empty;  // string.Empty evita el warning
    public decimal amount { get; set; }
    public DateTime date { get; set; }
    public ExpenseCategory category { get; set; }
}

public enum ExpenseCategory
{
    Food,           // 0
    Transportation, // 1
    Entertainment,  // 2
    Utilities,      // 3
    Healthcare,     // 4
    Other           // 5
}