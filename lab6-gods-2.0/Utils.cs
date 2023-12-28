using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using lab1;
using MassTransit;
using Message;

public static class Utils 
{
    public static bool Stopper { get; set; } = true;
    private static int Wins { get; set; }
    public static int TotalExperiments { get; set; }
    private static int Count { get; set; }

    public static Semaphore sem = new Semaphore(0, 1); 
    
    private static readonly CardsSplitter _cardsSplitter = new CardsSplitter();
    private static readonly DeckShuffler _deckShuffler = new DeckShuffler();

    public static async Task SendDeckByMassTransit (
        ISendEndpoint elonEndpoint, 
        ISendEndpoint markEndpoint)
    {
        
        if (TotalExperiments == Count)
        {
            Finish();
            return;
        }
        
        _deckShuffler.ShuffleDeck();
        (var elonCards, var markCards) = _cardsSplitter.GetDeckForPlayers(_deckShuffler.GetDeck());
        
        await Task.WhenAll(
            elonEndpoint.Send(new DeckMessage(elonCards.ToList())), 
            markEndpoint.Send(new DeckMessage(markCards.ToList())));
    }

    public static async Task getDecksFromPlayer(ColorApiClient colorApiClient, int elonPort, int markPort)
    {
        if (TotalExperiments == Count)
        {
            Finish();
            return;
        }
        
        var res = await Task.WhenAll(
                colorApiClient.GetColor(elonPort, "elon"), 
                colorApiClient.GetColor(markPort, "mark")
            );
        
        (var elonColor, var markColor) = (res[0], res[1]);
        

        Console.WriteLine($"[{Count}]");
        Console.WriteLine("\telonColor: " + elonColor);
        Console.WriteLine("\tmarkColor: " + markColor);
        
        Console.WriteLine();
        
        Wins += elonColor == markColor ? 1 : 0;
        Count += 1;
    }

    private static void Finish()
    {
        sem.Release();
        Console.WriteLine($"Wins: {(double) Wins / TotalExperiments * 100}%");
        
    }
}
