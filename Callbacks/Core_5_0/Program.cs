using System.Reflection;
using NServiceBus;
using NServiceBus.Features;

class Program
{
    static string endpointName = $"WireCompatCallbacks{Assembly.GetExecutingAssembly().GetName().Name}";

    public static void Main()
    {
        var bus = CreateBus();
        TestRunner.EndpointName = endpointName;
        TestRunner.RunTests(bus);
    }

    static IBus CreateBus()
    {
        Asserter.LogError = NServiceBus.Logging.LogManager.GetLogger("Asserter").Error;
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName(endpointName);
        var conventions = busConfiguration.Conventions();
        conventions.DefiningMessagesAs(t => t.Namespace != null && (t.Namespace.StartsWith("CommonMessages")));
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.UseTransport<MsmqTransport>();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.DisableFeature<TimeoutManager>();
        busConfiguration.EnableInstallers();
        var startableBus = Bus.Create(busConfiguration);
        return startableBus.Start();
    }

}