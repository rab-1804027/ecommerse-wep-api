using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;

var builder = WebApplication.CreateBuilder(args);

// /// For swagger api testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/// For swagger api testing
if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection( );

var products = new List<Product>(){
    new Product("Samsung20", 1240),
    new Product("Nokia", 1000)
};

app.MapGet("/products", ()=>{
    return Results.Ok(products);
});

app.MapGet("/hello", ()=>{
    var response = new { message = "This is a json object", success = true};
    return Results.Ok(response);
});

app.MapGet("/", ()=>{
    return Results.Content("<h1> Hello World </h2>", "text/html");
});

app.MapPost("/", ()=>{
    return Results.Ok();
});

app.MapPut("/", ()=>{
    return Results.Created();
});
 
app.MapDelete("/", ()=>{
    return Results.NoContent();
});

app.Run();

public record Product(string Name, decimal Price);
 