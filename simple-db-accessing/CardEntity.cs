namespace ConsoleApp1
{
    public class CardEntity
    {
        public int Id { get; set; }
        public CardColorEntity Color { get; set; }
        

        public int? ExperimentId { get; set; }
        public ExperimentEntity? Experiment { get; set; }

        public CardEntity() {}
        
        public CardEntity(CardColorEntity color, ExperimentEntity experimentEntity)
        {
            Color = color;
            Experiment = experimentEntity;
        }
    }
}