using System;
using System.Threading.Tasks;
using MassTransit;

public static class LoaderEndPoints
{
    public static async Task<(ISendEndpoint, ISendEndpoint)> LoadEndPointForTwoPlayers(
        IBusControl busControl, 
        string url1, 
        string url2)
    {
        return (
            await busControl.GetSendEndpoint(new Uri(url1)), 
            await busControl.GetSendEndpoint(new Uri(url2)));
    }
    
    public static async Task<(ISendEndpoint, ISendEndpoint)> LoadEndPointForTwoPlayers(
        ConsumeContext context, 
        string url1, 
        string url2)
    {
        return (
            await context.GetSendEndpoint(new Uri(url1)), 
            await context.GetSendEndpoint(new Uri(url2)));
    }
        
}