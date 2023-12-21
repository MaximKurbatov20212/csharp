using System;
using System.Collections.Generic;
using lab1;

namespace ConsoleApp1
{
    class Program 
    {
        public static void Main(string[] args)
        {
            int count = 100;
            using ApplicationContext db = new ApplicationContext();

            List<List<Card>> decks = DBService.generateExperiments(count);

            var (experimentEntities, deckEntities) = DBService.toEntity(decks);

            DBService.saveExperiments(db, experimentEntities);

            DBService.saveDecks(db, deckEntities);

            double win = 0;
            
            foreach (var experiment in DBService.getExperiments(db))
            {
                CollisiumSandbox sandbox = new CollisiumSandbox(new Elon(new PickFirstBlackCardStrategy()),
                    new Mark(new PickFirstBlackCardStrategy()));
            
                win += sandbox.Play(experiment.Deck
                    .ConvertAll(cardEntity => new Card(cardEntity.Color == CardColorEntity.Red ? CardColor.Black : CardColor.Red)).ToArray()
                ) ? 1 : 0;
            }
            
            Console.WriteLine("Wins : " + win / count * 100 + "%");
        }
    }
}