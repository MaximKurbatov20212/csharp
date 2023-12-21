using lab1;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddScoped<ICardPickStrategy, PickFirstBlackCardStrategy>();

        var app = builder.Build();

        // app.UseHttpsRedirection();
        // app.UseRouting();
        // app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}
