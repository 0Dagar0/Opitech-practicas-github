using System;
public class BankTransferPayment : IPaymentMethod
{
    public bool ProcessPayment(double Amount)
    {
        Console.WriteLine($"Procesando pago con transferencia bancaria ${Amount}");
        return true;
    }
}

