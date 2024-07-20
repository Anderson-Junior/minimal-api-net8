using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.OpenApi.Models;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product API",
        Description = "API for managing a list of product.",
    });
});

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("Product"));

builder.Services.AddHttpClient("FruitAPI", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("http://localhost:5050/fruitlist/");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapGet("/products", async (AppDbContext db) =>
    await db.Products.ToListAsync())
    .WithTags("Get all products.");

app.MapGet("/fruitlist/{id}", async (Guid id, AppDbContext db) =>
    await db.Products.FindAsync(id)
        is Product Product
            ? Results.Ok(Product)
            : Results.NotFound())
    .WithTags("Get Product by Id");

app.MapPost("/product", async (Product product, AppDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();

    return Results.Created($"/products/{product.Id}", product);
}).WithTags("Add product to list.");

app.MapGet("/fruits", async (IHttpClientFactory client) =>
{
    var httpClient = client.CreateClient("FruitAPI");

    try
    {
        using HttpResponseMessage response = await httpClient.GetAsync("");

        var FruitModels = new List<Fruit>();

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            FruitModels = await JsonSerializer.DeserializeAsync<List<Fruit>>(contentStream);

            return Results.Ok(FruitModels);
        }

        return response.StatusCode switch
        {
            System.Net.HttpStatusCode.NotFound => Results.NotFound("API de destino não encontrada."),
            System.Net.HttpStatusCode.BadGateway => Results.Problem("Erro de comunicação com a API de destino.", statusCode: 502),
            System.Net.HttpStatusCode.ServiceUnavailable => Results.Problem("API de destino temporariamente indisponível.", statusCode: 503),
            _ => Results.Problem("Erro interno do servidor.", statusCode: 500)
        };
    }
    catch (HttpRequestException ex)
    {
        return Results.Problem("Erro de comunicação com a API externa.", statusCode: 502);
    }
    catch (Exception ex)
    {
        return Results.Problem("Erro interno do servidor.", statusCode: 500);
    }
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();