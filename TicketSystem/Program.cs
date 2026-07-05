using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using TicketSystem.Entities;
using TicketSystem.Repositories;
using UserSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ISalesChannelRepository, SalesChannelRepository>();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

#region User

app.MapGet("/users", async (IUserRepository userRepository) =>
{
    var users = await userRepository.GetAllUsers();
    return users;
});

app.MapGet("/users/{id:int}", async
    Task<Results<NotFound, Ok<User>>>
    (int id, IUserRepository userRepository) =>
{
    var user = await userRepository.GetUser(id);

    if (user is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(user);
});

app.MapPost("/users", async (User user, IUserRepository userRepository) =>
{
    await userRepository.CreateUser(user);
    return TypedResults.Created($"/users/{user.Id}", user);
});

app.MapPut("/users/{id:int}", async
    Task<Results<BadRequest<string>, NoContent, NotFound>>
    (int id, User user, IUserRepository userRepository) =>
{
    if (id != user.Id)
    {
        return TypedResults.BadRequest("El ID en la URL no coincide con el ID en el objeto User");
    }

    var exists = await userRepository.ExistsUserById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await userRepository.UpdateUser(user);
    return TypedResults.NoContent();
});

app.MapDelete("/users/{id:int}", async
    Task<Results<NoContent, NotFound>>
    (int id, IUserRepository userRepository) =>
{
    var exists = await userRepository.ExistsUserById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await userRepository.DeleteUser(id);
    return TypedResults.NoContent();
});

#endregion

#region SalesChannel

app.MapGet("/saleschannels", async (ISalesChannelRepository salesChannelRepository) =>
{
    var saleschannels = await salesChannelRepository.GetAllSalesChannels();
    return saleschannels;
});

app.MapGet("/saleschannels/{id:int}", async
    Task<Results<NotFound, Ok<SalesChannel>>>
    (int id, ISalesChannelRepository salesChannelRepository) =>
{
    var saleschannel = await salesChannelRepository.GetSalesChannel(id);

    if (saleschannel is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(saleschannel);
});

app.MapPost("/saleschannels", async (SalesChannel saleschannel, ISalesChannelRepository salesChannelRepository) =>
{
    await salesChannelRepository.CreateSalesChannel(saleschannel);
    return TypedResults.Created($"/saleschannels/{saleschannel.Id}", saleschannel);
});

app.MapPut("/saleschannels/{id:int}", async
    Task<Results<BadRequest<string>, NoContent, NotFound>>
    (int id, SalesChannel saleschannel, ISalesChannelRepository salesChannelRepository) =>
{
    if (id != saleschannel.Id)
    {
        return TypedResults.BadRequest("El ID en la URL no coincide con el ID en el objeto SalesChannel");
    }

    var exists = await salesChannelRepository.ExistsSalesChannelById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await salesChannelRepository.UpdateSalesChannel(saleschannel);
    return TypedResults.NoContent();
});

app.MapDelete("/saleschannels/{id:int}", async
    Task<Results<NoContent, NotFound>>
    (int id, ISalesChannelRepository salesChannelRepository) =>
{
    var exists = await salesChannelRepository.ExistsSalesChannelById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await salesChannelRepository.DeleteSalesChannel(id);
    return TypedResults.NoContent();
});

#endregion

#region Event

app.MapGet("/events", async (IEventRepository eventRepository) =>
{
    var events = await eventRepository.GetAllEvents();
    return events;
});

app.MapGet("/events/{id:int}", async
    Task<Results<NotFound, Ok<Event>>>
    (int id, IEventRepository eventRepository) =>
{
    var evento = await eventRepository.GetEvent(id);

    if (evento is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(evento);
});

app.MapPost("/events", async (Event evento, IEventRepository eventRepository) =>
{
    await eventRepository.CreateEvent(evento);
    return TypedResults.Created($"/events/{evento.Id}", evento);
});

app.MapPut("/events/{id:int}", async
    Task<Results<BadRequest<string>, NoContent, NotFound>>
    (int id, Event evento, IEventRepository eventRepository) =>
{
    if (id != evento.Id)
    {
        return TypedResults.BadRequest("El ID en la URL no coincide con el ID en el objeto Event");
    }

    var exists = await eventRepository.ExistsEventById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await eventRepository.UpdateEvent(evento);
    return TypedResults.NoContent();
});

app.MapDelete("/events/{id:int}", async
    Task<Results<NoContent, NotFound>>
    (int id, IEventRepository eventRepository) =>
{
    var exists = await eventRepository.ExistsEventById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await eventRepository.DeleteEvent(id);
    return TypedResults.NoContent();
});

#endregion

#region Reservation

app.MapGet("/reservations", async (IReservationRepository reservationRepository) =>
{

    var reservations = await reservationRepository.GetAllReservations();
    return reservations;
});

app.MapGet("/reservations/{id:int}", async
    Task<Results<NotFound, Ok<Reservation>>>
    (int id, IReservationRepository reservationRepository) =>
{
    var reservation = await reservationRepository.GetReservation(id);

    if (reservation is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(reservation);
});

app.MapPost("/reservations", async (Reservation reservation, IReservationRepository reservationRepository) =>
{
    await reservationRepository.CreateReservation(reservation);
    return TypedResults.Created($"/reservations/{reservation.Id}", reservation);
});

app.MapPut("/reservations/{id:int}", async
    Task<Results<BadRequest<string>, NoContent, NotFound>>
    (int id, Reservation reservation, IReservationRepository reservationRepository) =>
{
    if (id != reservation.Id)
    {
        return TypedResults.BadRequest("El ID en la URL no coincide con el ID en el objeto Reservation");
    }

    var exists = await reservationRepository.ExistsReservationById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await reservationRepository.UpdateReservation(reservation);
    return TypedResults.NoContent();
});

app.MapDelete("/reservations/{id:int}", async
    Task<Results<NoContent, NotFound>>
    (int id, IReservationRepository reservationRepository) =>
{
    var exists = await reservationRepository.ExistsReservationById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await reservationRepository.DeleteReservation(id);
    return TypedResults.NoContent();
});

#endregion

#region Ticket

app.MapGet("/tickets", async (ITicketRepository ticketRepository) =>
{
    var tickets = await ticketRepository.GetAllTickets();
    return tickets;
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

#endregion

app.Run();
