// ﻿using Microsoft.Extensions.DependencyInjection;
//
// namespace lab4
// {
//     class GenMain 
//     {
//         public static void M(string[] args)
//         {
//             CreateHostBuilder(args).Build().Run();
//         }
//         
//
//         public static IHostBuilder CreateHostBuilder(string[] args)        	
//         {
//             return Host.CreateDefaultBuilder(args)
//                 .ConfigureServices((hostContext, services) =>
//                 {
//                     
//                     // services.AddHostedService<CollisiumExperimentWorker>();
//                     //
//                     // services.AddScoped<CollisiumSandbox>();
//                     // services.AddScoped<IDeckShuffler, DeckShuffler>();
//                     // services.AddScoped<Elon>();
//                     // services.AddScoped<Mark>();
//                     // services.AddScoped<ICardPickMarkStrategy, StratagyNumberOne>();
//                     // services.AddScoped<ICardPickElonStrategy, StratagyNumberOne>();
//                 });
//         }
//     }
// }