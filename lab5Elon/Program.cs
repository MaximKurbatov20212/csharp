using lab1;
using lab5Elon;
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

        // app.UseHttpsRedirection(); // https
        // app.UseRouting();
        // app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
