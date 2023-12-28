using System.Diagnostics;
using System.Threading.Tasks;
using Lab6_Gods;
using Microsoft.Extensions.DependencyInjection;

public static class Program 
{
    private static async Task Main()
    {
        var services = new ServiceCollection();
        
        ConfigureServices(services);
            
        var serviceProvider = services.BuildServiceProvider();
        
        var godsServer = serviceProvider.GetService<GodsServer>();

        Debug.Assert(godsServer != null, nameof(godsServer) + " != null");
        
        await godsServer.RunExperiments(
            3001, 
            3002,  
            "rabbitmq://localhost", 
            "rabbitmq://localhost/Elon_queue", 
            "rabbitmq://localhost/Mark_queue",
            100
            );
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient<ColorApiClient>();
        services.AddSingleton<GodsServer>();
    }
}
