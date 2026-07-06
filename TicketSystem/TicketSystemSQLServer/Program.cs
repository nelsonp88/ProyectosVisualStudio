using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using TicketSystem.Entities;
using TicketSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<ITicketRepository, TicketRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/tickets", async (ITicketRepository ticketRepository) =>
{

    var tickets = await ticketRepository.GetAllTickets();
    return tickets;

    /*var tickets = new List<Ticket>()
    {
        new Ticket { Id = 1, TicketCode = "ABC123", EventId = 1, ReservationId = 7953459, Price = 24, SeatNumber = "AF8", CreatedAt = DateTime.Now, Status = TicketStatus.Sold},
        new Ticket { Id = 2, TicketCode = "OPQ423", EventId = 2, ReservationId = 6774454, Price = 18, SeatNumber = "BF9", CreatedAt = DateTime.Now, Status = TicketStatus.Sold},
        new Ticket { Id = 3, TicketCode = "RFS776", EventId = 3, ReservationId = 9976653, Price = 14, SeatNumber = "RT6", CreatedAt = DateTime.Now, Status = TicketStatus.Sold},
        new Ticket { Id = 4, TicketCode = "UOH092", EventId = 4, ReservationId = 1343234, Price = 32, SeatNumber = "EW1", CreatedAt = DateTime.Now, Status = TicketStatus.Sold},
        new Ticket { Id = 5, TicketCode = "OPW577", EventId = 5, ReservationId = 7758866, Price = 11, SeatNumber = "FG2", CreatedAt = DateTime.Now, Status = TicketStatus.Sold}
    };
    return tickets;*/
});

app.MapGet("/tickets/{id:int}", async 
    Task<Results<NotFound, Ok<Ticket>>>
    (int id, ITicketRepository ticketRepository) =>
{
    var ticket = await ticketRepository.GetTicket(id);

    if (ticket is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(ticket);
});

app.MapPost("/tickets", async (Ticket ticket, ITicketRepository ticketRepository) =>
{
    await ticketRepository.CreateTicket(ticket);
    return TypedResults.Created($"/tickets/{ticket.Id}", ticket);
});

app.MapPut("/tickets/{id:int}", async
    Task<Results<BadRequest<string>, NoContent, NotFound>>
    (int id, Ticket ticket, ITicketRepository ticketRepository) =>
{
    if (id != ticket.Id)
    {
        return TypedResults.BadRequest("El ID en la URL no coincide con el ID en el objeto Ticket");
    }

    var exists = await ticketRepository.ExistsTicketById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await ticketRepository.UpdateTicket(ticket);
    return TypedResults.NoContent();
});

app.MapDelete("/tickets/{id:int}", async 
    Task<Results<NoContent, NotFound>>
    (int id, ITicketRepository ticketRepository) =>
{
    var exists = await ticketRepository.ExistsTicketById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await ticketRepository.DeleteTicket(id);
    return TypedResults.NoContent();
});

app.Run();
