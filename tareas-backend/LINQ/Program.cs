using LinqTraining.Data;
using LinqTraining.Models;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

// Ejercicio 1

List<Order> orders = SampleData.GetOrders();

var LastMonthOrders = orders
    .Where(o => o.Date >= DateTime.Today.AddDays(-30))
    .ToList();

var GroupeOrders = LastMonthOrders
    .GroupBy (o => o.Status)
    .ToList();
foreach (var group in GroupeOrders)
{
    Console.WriteLine(group.Key + ": " + group.Count());
}


// Ejercicio 2
List<Book> books = SampleData.GetBooks();

var Top5Bestsold = books
    .GroupBy(b => b.Genre)
    .Select(grup => new {
        Gender = grup.Key,
        Top5Books = grup
            .OrderByDescending(book => book.UnitsSold)
            .Take(5)
            .ToList()
    });

foreach (var grup in Top5Bestsold)
{
    Console.WriteLine($"Genero: {grup.Gender}");
    Console.WriteLine("Top 5 libros:");
    foreach (var libro in grup.Top5Books)
    {
        Console.WriteLine($"- {libro.Title} ({libro.UnitsSold} unidades vendidas)");
    }
    Console.WriteLine();
}

// Ejercicio 3

Console.Write("Ingrese el texto a buscar en el título: ");
string textoBusqueda = Console.ReadLine();

var librosEncontrados = books
    .Where(libro => libro.Title
    .ToLower()
    .Contains(textoBusqueda.ToLower()))
    .ToList();

Console.WriteLine($"\n--- Libros que contienen '{textoBusqueda}' ---");

if (librosEncontrados.Count == 0)
{
    Console.WriteLine("No se encontraron libros.");
}
else
{
    foreach (var libro in librosEncontrados)
    {
        Console.WriteLine($"- {libro.Title} | Autor: {libro.Author} | Ventas: {libro.UnitsSold}");
    }

    Console.WriteLine($"\nTotal de libros encontrados: {librosEncontrados.Count}");
}

// Ejercicio 4

var top3Clientes = orders
    .Where(o => o.Status == "Completed")               // Solo pedidos completados
    .GroupBy(o => o.CustomerName)                      // Agrupar por cliente
    .Select(grupo => new                               // Proyectar resultado
    {
        Cliente = grupo.Key,
        TotalGastado = grupo.Sum(o => o.Amount)
    })
    .OrderByDescending(c => c.TotalGastado)            // Ordenar de mayor a menor
    .Take(3);                                          // Tomar los 3 primeros

Console.WriteLine("=== TOP 3 CLIENTES QUE MÁS COMPRARON ===\n");

foreach (var cliente in top3Clientes)
{
    Console.WriteLine($"Cliente: {cliente.Cliente}");
    Console.WriteLine($"Total gastado: {cliente.TotalGastado:C}");
    Console.WriteLine();
}


// Ejercicio 5


// Obtener todos los clientes distintos
var todosLosClientes = orders
    .Select(o => o.CustomerName)
    .Distinct()
    .ToList();

// Obtener clientes que SÍ tienen al menos un pedido completado
var clientesConPedidoCompletado = orders
    .Where(o => o.Status == "Completed")
    .Select(o => o.CustomerName)
    .Distinct()
    .ToList();

// Clientes sin ningún pedido completado (exclusión)
var clientesSinCompletados = todosLosClientes
    .Except(clientesConPedidoCompletado)
    .ToList();

Console.WriteLine("=== CLIENTES SIN NINGÚN PEDIDO COMPLETADO ===\n");

if (clientesSinCompletados.Count == 0)
{
    Console.WriteLine("Todos los clientes tienen al menos un pedido completado.");
}
else
{
    foreach (var cliente in clientesSinCompletados)
    {
        Console.WriteLine($"- {cliente}");
    }
}