using ServiciosEInyeccionDependencias.Application.Interfaces;
using ServiciosEInyeccionDependencias.Application.Repositories;
using ServiciosEInyeccionDependencias.Application.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// INYECCIÓN DE DEPENDENCIAS - REGISTRO DE SERVICIOS

// Registrar IBookRepository con su implementación concreta InMemoryBookRepository
// AddScoped = crea una instancia por cada petición HTTP
builder.Services.AddScoped<IBookRepository, InMemoryBookRepository>();

// Registrar BookService (sin interfaz, se registra directamente)
builder.Services.AddScoped<BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();