using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Contracts;
using Controllers;
using Core;
using Entities;
using Resources.Const;

// Little messy in the references but this is the first time I use dependency injection in Console
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddScoped<ICash, Cash>()
            .AddScoped<IValidation, Validation>()
            .AddTransient<CashController>()
    ).Build();

Scope(host.Services, "Main Scope");

await host.RunAsync();

static void Scope(IServiceProvider services, string scope)
{
    // Set environment variables
    Environment.SetEnvironmentVariable("REGION", Names.MX);
    
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    //Main logic
    Console.WriteLine("Insert total charge: ");
    var chargeTotal = Console.ReadLine();

    Console.WriteLine("Insert customer provided cash");
    var cashProvided = Console.ReadLine();

    CashController cashController = provider.GetRequiredService<CashController>();
    MessageResponse results = cashController.GetChange(chargeTotal, cashProvided);
    
    Console.WriteLine(results.response);
}