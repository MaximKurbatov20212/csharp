using MassTransit;
using System.Reflection;
using lab6_elon.consumers;
using lab6_elon.controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

class Program 
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        builder.Services.AddMassTransit(registrConfigurator =>
        {
            
            registrConfigurator.UsingRabbitMq((_, cfg) =>
            {
                cfg.Host("localhost", "/", hostConfigurator =>
                {
                    hostConfigurator.Username("guest");
                    hostConfigurator.Password("guest");
                });
                
                cfg.ReceiveEndpoint("Elon_queue", ep =>
                {
                    ep.Consumer<NumberConsumer>();
                    ep.Consumer<DeckConsumer>();
                });
            });
        });


        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
