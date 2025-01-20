using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

var builder = WebApplication.CreateBuilder(args);

/// for controller
builder.Services.AddControllers();
/// For swagger api testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/// For swagger api testing
if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection( );

/* 

/// Anisul Islam part 6 prjnto

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

*/

app.MapPost("/", ( ) => "Api is working fine");

app.MapControllers();
app.Run();