using System.Threading.Tasks;
using MassTransit;
using Message;

public class ReadyPlayersConsumer : IConsumer<OkPlayer>
{
    static class ReadyPlayerControl
    {
        internal static int CountReady { get; set; }
        internal static readonly object LockObject = new object();
    }
    
    public Task Consume(ConsumeContext<OkPlayer> context)
    {
        lock (ReadyPlayerControl.LockObject)
        {
            ReadyPlayerControl.CountReady += 1;
            
            if (ReadyPlayerControl.CountReady != 2) return Task.CompletedTask;
            
            context.Publish(new SenderCompleted());
            
            ReadyPlayerControl.CountReady = 0;
        }
        
        return Task.CompletedTask;
    }
}