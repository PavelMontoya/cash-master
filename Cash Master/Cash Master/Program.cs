using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Contracts;
using Controllers;
using Core;
using Entities;
using Resources.Const;
using Resources.Enums;

// Little messy in the references but this is the first time I use dependency injection in Console
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddScoped<ICash, Cash>()
            .AddScoped<IValidation, Validation>()
            .AddTransient<CashController>()
    ).Build();

Scope(host.Services);

await host.RunAsync();

static void Scope(IServiceProvider services)
{
    // Set environment variables
    Environment.SetEnvironmentVariable("REGION", Texts.MX);

    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    try
    {
        //Main logic
        Console.WriteLine(Texts.INSERT_TC);
        var chargeTotal = Console.ReadLine();

        Console.WriteLine(Texts.INSERT_CP);
        var cashProvided = Console.ReadLine();

        CashController cashController = provider.GetRequiredService<CashController>();
        MessageResponse results = cashController.GetChange(chargeTotal, cashProvided);

        if (results.status == Status.Failed)
        {
            Console.WriteLine(Texts.ERROR);
            Console.WriteLine(results.response);
            return;
        }

        Console.WriteLine(Texts.RETURN);
        Console.WriteLine(results.response);
    }
    catch (NullReferenceException e)
    {
        Console.WriteLine(Texts.ERROR);
        Console.WriteLine(e);
        throw;
    }
}