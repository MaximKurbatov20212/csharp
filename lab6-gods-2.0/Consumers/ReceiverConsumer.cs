using System.Threading.Tasks;
using MassTransit;
using Message;

public class ReceiverConsumer : IConsumer<SenderCompleted>
{
    private readonly ColorApiClient _colorApiClient;
    private readonly int _elonPort;
    private readonly int _markPort;
    
    public ReceiverConsumer(ColorApiClient colorApiClient, int elonPort, int markPort)
    {
        _colorApiClient = colorApiClient;
        _elonPort = elonPort;
        _markPort = markPort;
    }

    public async Task Consume(ConsumeContext<SenderCompleted> context)
    {
        await Utils.getDecksFromPlayer(_colorApiClient, _elonPort, _markPort);
        
        await context.Publish(new ReceiverCompleted()); 
    }
}