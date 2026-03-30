
using System;
public class CreditCardPayment: IPaymentMethod
{
    public bool ProcessPayment(double Amount)
    {
        Console.WriteLine($"Procesando pago con tarjeta de credito ${Amount}");
        return true;
    }
}


