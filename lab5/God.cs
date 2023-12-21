using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using lab1;
using Newtonsoft.Json;

namespace lab5
{
    class God 
    {
        private const int _elonPort = 5001;
        private const int _markPort = 5002;
        private const int count = 100; 
        
        private static readonly CardsSplitter _cardsSplitter = new CardsSplitter();

        private static async Task Main(string[] args)
        {
            using ApplicationContext db = new ApplicationContext();
            generateExpreiments(db);

            var win = 0;
            for (var i = 0; i < count; i++)
            {
                List<Card> deck = getDeckFromDB(db.Experiments.ToArray()[i], db);

                (var elonCards, var markCards) = _cardsSplitter.GetDeckForPlayers(deck.ToArray());
                
                var markChoice = await SendDeckAsync(markCards.ToList(), _markPort);
                var elonChoice = await SendDeckAsync(elonCards.ToList(), _elonPort);

                if (markChoice == -1 || elonChoice == -1)
                {
                   continue; 
                }

                win += markCards[elonChoice].Color == elonCards[markChoice].Color ? 1 : 0;
            }
            Console.WriteLine((double) win / count * 100 + "%");
        }


        private static async Task<int> SendDeckAsync(List<Card> cards, int port)
        {
            using var client = new HttpClient();
            
            var json = JsonConvert.SerializeObject(cards); 
            
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync($"http://localhost:{port}/api/v1/cards", content);

            // response.EnsureSuccessStatusCode();
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Not success response:" + response.StatusCode);
                return -1;
            }
            
            var responseBody = Convert.ToInt32(await response.Content.ReadAsStringAsync());
            return responseBody;
        }

        private static void generateExpreiments(ApplicationContext db)
        {
            List<List<Card>> decks = DBService.generateExperiments(count);
            
            (var _experimentEntities, var _deckEntities) = DBService.toEntity(decks);
            
            DBService.saveExperiments(db, _experimentEntities);
            DBService.saveDecks(db, _deckEntities);
        }
        
        private static List<Card> getDeckFromDB(ExperimentEntity experiment, ApplicationContext db)
        {
            List<Card> deck = new List<Card>();

            foreach (var entity in db.CardEntities)
            {
                if (entity.Experiment == experiment)
                {
                    deck.Add(new Card(entity.Color == CardColorEntity.Black ? CardColor.Black : CardColor.Red));
                }
            }
            return deck;
        }
    }
}