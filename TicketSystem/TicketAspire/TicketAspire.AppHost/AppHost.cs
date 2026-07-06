using Projects;

var builder = DistributedApplication.CreateBuilder(args);

/*var apiService = builder.AddProject<Projects.TicketAspire_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");*/

var ticketwebapi = builder.AddProject<Projects.TicketApi>("ticketwebapi").WithHttpHealthCheck("/health");

var ticketwebapp = builder.AddProject<Projects.TicketApp>(name: "ticketwebapp")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(ticketwebapi)
    .WaitFor(ticketwebapi);

/*builder.AddProject<Projects.TicketAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);*/

builder.Build().Run();
