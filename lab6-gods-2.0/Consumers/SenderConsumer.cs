using System.Threading.Tasks;
using MassTransit;
using Message;

public class SenderConsumer: IConsumer<ReceiverCompleted>
{
    private readonly string _url1;
    private readonly string _url2;
    
    public SenderConsumer(string url1, string url2)
    {
        _url1 = url1;
        _url2 = url2;
    }

    public async Task Consume(ConsumeContext<ReceiverCompleted> context)
    {
        var (elonEndpoint, markEndpoint) = LoaderEndPoints.LoadEndPointForTwoPlayers(context, _url1, _url2).Result;
        
        await Utils.SendDeckByMassTransit(elonEndpoint, markEndpoint);
    }
}