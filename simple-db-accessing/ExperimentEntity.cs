using System.Collections.Generic;

namespace ConsoleApp1
{
    public class ExperimentEntity
    {
        public int Id { get; set; }
        public List<CardEntity> Deck { get; set; }
    }

    public enum CardColorEntity
    {
        Red,
        Black
    }
}
