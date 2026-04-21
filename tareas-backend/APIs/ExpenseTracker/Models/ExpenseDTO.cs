namespace ExpenseTracker.Models
{
    public class ExpenseDTO
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;

    }
}
