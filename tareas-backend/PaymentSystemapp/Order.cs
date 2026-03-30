

public class Order
{
    public int Id { get; set; }
    public required string CustomerName { get; set; }
    public double Amount { get; set; }

    private bool isPaid = false;
    private string paymentMethodUsed = "";

    public string GetDescription()
    {
        string status  = isPaid ? "Paid" : "Pending";
        return $" Order {Id} - {CustomerName} - ${Amount} - {status} - Method: {paymentMethodUsed}";
    }

    public bool IsPaid()
    {
        return isPaid;
    }

    public void MarkAsPaid (string method)
    {
        isPaid = true;
        paymentMethodUsed = method;
    }




}
