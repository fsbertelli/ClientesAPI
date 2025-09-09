using ClientesAPI.Data;
using ClientesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPI.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
    {
        //Rota para obter todos os clientes
        app.MapGet("/clientes", async (AppDbContext db) => db.Clientes.ToListAsync());
        
        //Rota para obter um cliente por ID
        app.MapGet("/clientes/{id}", async (int id, AppDbContext db) => 
            await db.Clientes.FindAsync(id) is Cliente c ? Results.Ok(c) : Results.NotFound() );
        
        // Rota para criar um novo cliente
        app.MapPost("/clientes", async (Cliente cliente, AppDbContext db) =>
        {
            db.Clientes.Add(cliente);
            await db.SaveChangesAsync();
            return Results.Created($"/clientes/{cliente.Id}", cliente);
        });
        
        // Rota para atualizar um cliente existente
        app.MapPut("/clientes/{id}", async (int id, Cliente input, AppDbContext db) =>
        {
            var cliente = await db.Clientes.FindAsync(id);
            if (cliente is null) return Results.NotFound();

            cliente.Nome = input.Nome;
            // ... (outros campos)

            
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
        
        // Rota para deletar um cliente
        app.MapDelete("/clientes/{id}", async (int id, AppDbContext db) =>
        {
            var cliente = await db.Clientes.FindAsync(id);
            if (cliente is null) return Results.NotFound();

            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();
            return Results.Ok(cliente);
        });
        

    }

}