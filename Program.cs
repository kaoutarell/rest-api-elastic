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
    var settings =
        new ConnectionSettings(new Uri(
                "c17c36d8ee5f41dc849a95088dca1333:dXMtY2VudHJhbDEuZ2NwLmNsb3VkLmVzLmlvJDFkMzQ0NTEwNTY5ODRiZDJiMDQxZWYzODBlODQxZTdlJGRhZmViOGMwYTRmOTRhYzBhNTAxNmVhN2RiN2E1MDZl"))
            .ApiKeyAuthentication("K-9N65YB2gKofPwPVIa6", "amKMKIhcBc5cR31VU--VNA")
            .DefaultIndex("book_index");
    return new ElasticClient(settings);
});

var app = builder.Build();

//3. Map controllers
app.MapControllers();

//4. Redirect root URL to API route
app.MapGet("/", () =>
{
    return Results.Redirect("api/books");
});

app.Run();