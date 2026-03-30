using System;
public class PaymentProcessor
{
    public void Process(Order order, IPaymentMethod paymentMethod)
    {
        if (order.IsPaid())
        {
            Console.WriteLine("La orden ya esta pagada.");
            return;
        }

        bool success = paymentMethod.ProcessPayment(order.Amount);

        if (success)
        {
            order.MarkAsPaid(paymentMethod.GetType().Name);
            Console.WriteLine("Pago existoso");
        }

        else
        {
            Console.WriteLine("Pago fallido");
        }
    }
}


