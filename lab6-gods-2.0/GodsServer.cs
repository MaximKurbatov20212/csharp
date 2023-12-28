using System.Threading.Tasks;

namespace Lab6_Gods
{
    internal class GodsServer
    {
        
        private readonly ColorApiClient _colorApiClient;

        public GodsServer(ColorApiClient colorApiClient)
        {
            _colorApiClient = colorApiClient;
        }

        public async Task RunExperiments(
            int elonPort, 
            int markPort, 
            string urlServer, 
            string urlElon, 
            string urlMark,
            int totalExperiments
            )
        {

            Utils.TotalExperiments = totalExperiments;
            
            var busControl = BusControl.LoadBusRabbitMq(
                urlServer, 
                "guest", 
                "guest", 
                urlElon, 
                urlMark, 
                elonPort, 
                markPort, 
                _colorApiClient);
            
            var (elonEndpoint, markEndpoint) = await LoaderEndPoints.LoadEndPointForTwoPlayers(
                busControl, 
                urlElon,
                urlMark);
            
            BusControl.StartBusControl(busControl);
            
            await Utils.SendDeckByMassTransit(elonEndpoint, markEndpoint);

            Utils.sem.WaitOne();
            
            BusControl.StopBusControl(busControl);
        }
    }
}
