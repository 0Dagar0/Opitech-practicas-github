using System;

class Program
{
    static void Main(string[] args)
    {
        Store store = new Store();

        // Crear órdenes
        Order order1 = new Order
        {
            Id = 1,
            CustomerName = "Pepito",
            Amount = 100
        };

        Order order2 = new Order
        {
            Id = 2,
            CustomerName = "Ana",
            Amount = 200
        };

        Order order3 = new Order
        {
            Id = 3,
            CustomerName = "Carlos",
            Amount = 300
        };

        // Agregar órdenes
        store.AddOrder(order1);
        store.AddOrder(order2);
        store.AddOrder(order3);

        // Procesar pagos
        store.ProcessOrderPayment(1, new CreditCardPayment());
        store.ProcessOrderPayment(2, new DigitalWalletPayment());
        store.ProcessOrderPayment(3, new BankTransferPayment());

        // Intentar pagar otra vez
        Console.WriteLine("\nIntentando pagar la orden nuevamente:");
        store.ProcessOrderPayment(1, new CreditCardPayment());

        // Mostrar resumen
        Console.WriteLine("\nResumen de la tienda:");
        store.ShowSummary();
    }
}