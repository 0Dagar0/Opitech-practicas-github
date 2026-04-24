using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private static readonly List<Expense> _expenses = new List<Expense>();

    [HttpPost]
    public IActionResult RegisterExpense([FromBody] ExpenseDTO expense)
    {
        var newExpense = new Expense
        {
            Id = Guid.NewGuid(),
            Description = expense.Description,
            Amount = expense.Amount,
            Date = expense.Date,
            Category = expense.Category,
            PaymentMethod = expense.PaymentMethod
        };

        _expenses.Add(newExpense);

        return CreatedAtAction(nameof(RegisterExpense), new { id = newExpense.Id }, newExpense);
    }


    [HttpGet]
    public IActionResult GetAllExpenses()
    {
        return Ok(_expenses);
    }

}