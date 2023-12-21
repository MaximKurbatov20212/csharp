using System.Collections.Generic;
using lab1;

namespace ConsoleApp1
{
    public static class DBService
    {
        public static List<List<Card>> generateExperiments(int count)
        {
            List<List<Card>> experiments = new List<List<Card>>();
            DeckShuffler deckShuffler = new DeckShuffler();

            for (int i = 0; i < count; i++)
            {
                List<Card> l = new List<Card>();
                deckShuffler.ShuffleDeck();
                l.AddRange(deckShuffler.GetDeck());
                experiments.Add(l);
            }

            return experiments;
        }

        public static (List<ExperimentEntity>, List<CardEntity>) toEntity(List<List<Card>> cards)
        {
            List<ExperimentEntity> experimentEntities = new List<ExperimentEntity>();
            List<CardEntity> cardEntities = new List<CardEntity>();

            foreach (var l in cards)
            {
                ExperimentEntity experiment = new ExperimentEntity();

                foreach (var card in l)
                {
                    var c = new CardEntity
                    {
                        Color = card.Color == CardColor.Black ? CardColorEntity.Black : CardColorEntity.Red,
                        Experiment = experiment
                    };

                    cardEntities.Add(c);
                }

                experimentEntities.Add(experiment);
            }

            return (experimentEntities, cardEntities);
        }

        public static void saveExperiments(ApplicationContext db, List<ExperimentEntity> experimentEntities)
        {
            db.Experiments.AddRange(experimentEntities);
            db.SaveChanges();
        }

        public static void saveDecks(ApplicationContext db, List<CardEntity> decks)
        {
            db.CardEntities.AddRange(decks);
            db.SaveChanges();
        }

        public static List<ExperimentEntity> getExperiments(ApplicationContext db)
        {
            return new List<ExperimentEntity>(db.Experiments);
        }
        
        public static List<CardEntity> getDecks(ApplicationContext db)
        {
            return new List<CardEntity>(db.CardEntities);
        }
    }
}