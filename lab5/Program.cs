// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.EntityFrameworkCore;
// using WebAPIApp.Models;
//  
// namespace WebAPIApp
// {
//     public class Startup
//     {
//         public void ConfigureServices(IServiceCollection services)
//         {
//             string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
//             
//             // устанавливаем контекст данных
//             
//             services.AddDbContext<UsersContext>(options => options.UseSqlServer(con));
//  
//             services.AddControllers(); // используем контроллеры без представлений
//         }
//  
//         public void Configure(IApplicationBuilder app)
//         {
//             app.UseDeveloperExceptionPage();
//  
//             app.UseRouting();
//  
//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
//             });
//         }
//     }
// }