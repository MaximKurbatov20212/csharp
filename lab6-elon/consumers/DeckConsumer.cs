using System;
using System.Threading.Tasks;
using lab1;
using MassTransit;
using Message;

namespace lab6_elon.consumers
{
    public class DeckConsumer : IConsumer<DeckMessage>
    {
        public Task Consume(ConsumeContext<DeckMessage> context)
        {
            var deck = context.Message.Deck;
            
            ICardPickStrategy strategy = new PickFirstBlackCardStrategy();
            
            Utils.Cards = deck;
            
            var decision = strategy.Pick(deck.ToArray());

            Console.WriteLine($"My decision: {decision} card");
            
            context.Publish(new NumberMessage(decision, "elon"));
            
            return Task.CompletedTask;
        }
    }
}
