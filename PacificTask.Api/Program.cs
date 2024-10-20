using PacificTask.Data.Image;
using Microsoft.Data.Sqlite;
using PacificTask.Application.Image.Query;
using PacificTask.Client;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<SqliteConnection>(_ =>
{
    var connection = new SqliteConnection(connectionString);
    return connection;
});

builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddHttpClient<IImageApiClient, ImageApiClient>(x =>
{
    x.BaseAddress = new Uri("https://my-json-server.typicode.com");
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetImageForUserQuery).Assembly));

//for index.html
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
