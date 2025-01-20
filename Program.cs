using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
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

List<Category> categories = new List<Category>();

app.MapGet("/api/categories", ([FromQuery] string searchValue = "") => {

  //  Console.WriteLine($"Search Value: {searchValue}");
    if(!string.IsNullOrEmpty(searchValue))
    {
        var searchedCategories = categories.Where(c=> !string.IsNullOrEmpty(searchValue) && c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
       // Console.WriteLine($"Found Categories: {searchedCategories.Count}");
        return Results.Ok(searchedCategories);
    }

    return Results.Ok(categories);
}
);

app.MapPost("/api/categories", ([FromBody] Category categoryData ) => {
    
    /// input validation
    if(string.IsNullOrEmpty(categoryData.Name))
    {
        return Results.BadRequest("Category name is required and can not be empty");
    }

    var newCategory = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description,
        CreatedAt = DateTime.UtcNow
    };

    categories.Add(newCategory);

    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);
});

app.MapPut("/api/categories/{categoriesId}", (Guid categoriesId, [FromBody] Category categoryData) => {
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoriesId);
 
    if(foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exists");
    }

    if(categoryData == null)
    {
        return Results.BadRequest("Category data is missing");
    }

    if(!string.IsNullOrEmpty(categoryData.Name))
    {
        foundCategory.Name = categoryData.Name;
    }
    
    if(!string.IsNullOrEmpty(categoryData.Description))
    {
        foundCategory.Description = categoryData.Description;
    }

    return Results.NoContent();

});

app.MapDelete("/api/categories/{categoryId}", (Guid categoryId) => {
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

    if(foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exists");
    }

    categories.Remove(foundCategory);
    return Results.NoContent();

});

app.Run();

public record Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } 
}
 