using FizzBuzzLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using FizzBuzzLibrary.Options;

// create service collection
var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);

// create service provider
var serviceProvider = serviceCollection.BuildServiceProvider();

// get fizzBuzz from service provider
var fizzBuzz = serviceProvider.GetService<FizzBuzz>();

if(fizzBuzz != null)
{
    // get to work
    fizzBuzz.Work(10000);

    // disposing of services flushes logger before console log terminates
    serviceProvider.Dispose();
}
else
{
    throw new Exception("Fizz Buzz not instantiated!");
}

/// <summary>
/// Configure Services
/// </summary>
static void ConfigureServices(IServiceCollection services)
{
    // build config
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

    // add options
    services.Configure<FizzBuzzOptions>(configuration.GetSection("FizzBuzzOptions"));

    // configure logging
    services.AddLogging(configure => configure.AddConsole());

    // add services
    services.AddTransient<FizzBuzz>();
}
