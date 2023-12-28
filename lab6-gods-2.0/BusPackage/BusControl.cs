using System;
using MassTransit;

public static class BusControl
{
    public static IBusControl LoadBusRabbitMq(
        string url, 
        string name, 
        string password, 
        string urlElon, 
        string urlMark, 
        int portElon, 
        int portMark, 
        ColorApiClient client)
    {
        var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri(url), h =>
            {
                h.Username(name);
                h.Password(password);
            });
            
            cfg.ReceiveEndpoint(ep =>
            {
                ep.Consumer<ReadyPlayersConsumer>();
                ep.Consumer(() => new ReceiverConsumer(client, portElon, portMark));
                ep.Consumer(() => new SenderConsumer(urlElon, urlMark));
            });
            
        });
        return busControl;
    }
    
    public static void StartBusControl(IBusControl busControl)
    {
        busControl.Start();
    }
    
    public static void StopBusControl(IBusControl busControl)
    {
        busControl.Stop();
    }
}