// var builder = WebApplication.CreateBuilder(args);
//
// //add services
// builder.Services.AddControllers(); //link it to the controller we already have
//
// var app = builder.Build();
// app.MapControllers();
//
// //redirecting
// app.MapGet("/", () =>
// {
//     return Results.Redirect("api/books");
// });
//
// app.Run();

using Nest;

var builder = WebApplication.CreateBuilder(args);

//1. Add services
builder.Services.AddControllers();

//2. Add ES client
builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var uriString = Environment.GetEnvironmentVariable("ELASTICSEARCH_URI");
    var apiKeyId = Environment.GetEnvironmentVariable("ELASTIC_API_KEY_ID");
    var apiKeySecret = Environment.GetEnvironmentVariable("ELASTIC_API_KEY_SECRET");

    Console.WriteLine($"ELASTICSEARCH_URI: '{uriString}'");

    if (string.IsNullOrWhiteSpace(uriString))
        throw new InvalidOperationException("ELASTICSEARCH_URI environment variable is not set.");

    var settings = new ConnectionSettings(new Uri(uriString))
        .ApiKeyAuthentication(apiKeyId, apiKeySecret)
        .DefaultIndex("book_index");

    return new ElasticClient(settings);
});


//configure swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //dotnet add package Swashbuckle.AspNetCore

var app = builder.Build();

//we want to use swagger to call the apis
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//3. Map controllers
app.MapControllers();

//4. Redirect root URL to API route
app.MapGet("/", () =>
{
    return Results.Redirect("/swagger");
});

app.Run();