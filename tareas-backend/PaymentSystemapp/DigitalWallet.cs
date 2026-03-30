using System;
public class DigitalWalletPayment : IPaymentMethod
{
    public bool ProcessPayment(double Amount)
    {
        Console.WriteLine($"Procesando pago con billetera digital ${Amount}");
        return true;
    }
}

