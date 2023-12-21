using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;
using lab1;
using NUnit.Framework;

namespace test_simple_db_accessing
{
    public class Tests
    {
        private List<ExperimentEntity> _experimentEntities = new List<ExperimentEntity>();
        private List<CardEntity> _deckEntities = new List<CardEntity>();

        readonly ApplicationContext db = new ApplicationContext();

        [SetUp]
        public void Setup()
        {
            List<List<Card>> decks = DBService.generateExperiments(1);

            (_experimentEntities, _deckEntities) = DBService.toEntity(decks);

            DBService.saveExperiments(db, _experimentEntities);

            DBService.saveDecks(db, _deckEntities);
        }

        [Test]
        public void IsCorrectExperiments()
        {
            var has_error = false;

            var experimentsFromDatabase = DBService.getExperiments(db);

            experimentsFromDatabase.Sort((e1, e2) => e1.Id.CompareTo(e2.Id));
            _experimentEntities.Sort((e1, e2) => e1.Id.CompareTo(e2.Id));

            for (int i = 0; i < _experimentEntities.Count; i++)
            {
                for (int j = 0; j < experimentsFromDatabase[i].Deck.Count; j++)
                {
                    if (experimentsFromDatabase[i].Deck[j].Color != _experimentEntities[i].Deck[j].Color)
                    {
                        has_error = true;
                    }
                }
            }

            Assert.False(has_error);
        }
    }
}
