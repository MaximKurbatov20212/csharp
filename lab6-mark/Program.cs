using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DeckConsumer = lab6_mark.consumers.DeckConsumer;
using NumberConsumer = lab6_mark.consumers.NumberConsumer;

namespace lab6_mark
{
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
                
                    cfg.ReceiveEndpoint("Mark_queue", ep =>
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
}

