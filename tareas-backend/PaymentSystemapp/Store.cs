using System;
using System.Collections.Generic;

public class Store
{
    private List<Order> orders = new List<Order>();
    private PaymentProcessor processor = new PaymentProcessor();

    public void AddOrder(Order order)
    {
        orders.Add(order);
    }

    public void ProcessOrderPayment(int orderId, IPaymentMethod method)
    {
        foreach (var order in orders)
        {
            if (order.Id == orderId)
            {
                processor.Process(order, method);
                return;
            }
        }

        Console.WriteLine("Orden no encontrada");
    }

    public void ShowSummary()
    {
        double total = 0;
        int pending = 0;

        foreach (var order in orders)
        {
            if (order.IsPaid())
            {
                total += order.Amount;
            }
            else
            {
                pending++;
            }

            Console.WriteLine(order.GetDescription());
        }

        Console.WriteLine($"\nGanancia total: ${total}");
        Console.WriteLine($"Ordenes pendientes: {pending}");
    }
}