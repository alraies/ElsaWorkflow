﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace MERP.ElsaService.Web;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();
//         Log.Logger = new LoggerConfiguration()
// #if DEBUG
//             .MinimumLevel.Debug()
// #else
//             .MinimumLevel.Information()
// #endif
//             .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//             .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
//             .Enrich.FromLogContext()
//             .WriteTo.Async(c => c.File("Logs/logs.txt"))
//             .WriteTo.Async(c => c.Console())
//             .CreateLogger();

        try
        {
            Log.Information("Starting web host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<ElsaServiceWebModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
