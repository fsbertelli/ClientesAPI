using System.Net.Mime;
using System.Runtime.CompilerServices;
using ClientesAPI.Data;
using ClientesAPI.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapClienteEndpoints();
app.MapGet("/info", () => new
{
    timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
});



app.Run();

