using System;
using System.Threading.Tasks;
using MassTransit;
using Message;

namespace lab6_elon.consumers
{
    public class NumberConsumer : IConsumer<NumberMessage>
    {
        public Task Consume(ConsumeContext<NumberMessage> context)
        {
            if (context.Message.Signature == "elon")
            {
                return Task.CompletedTask;
            }


            Utils.Color = Utils.Cards[context.Message.Number].Color; 
            
            context.Publish(new OkPlayer());
            
            return Task.CompletedTask;
        }
    }
}