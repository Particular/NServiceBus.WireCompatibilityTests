using System.Reflection;
using NServiceBus;
using NServiceBus.Features;

class Program
{
    static string endpointName = $"WireCompatEncryption{Assembly.GetExecutingAssembly().GetName().Name}";

    static void Main()
    {
        var bus = CreateBus();
        TestRunner.EndpointName = endpointName;
        TestRunner.RunTests(bus);
    }

    static IBus CreateBus()
    {
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName(endpointName);
        busConfiguration.Conventions().ApplyMessageConventions();
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.UseTransport<MsmqTransport>();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.DisableFeature<TimeoutManager>();
        busConfiguration.RijndaelEncryptionService();
        busConfiguration.RegisterComponents(
            components =>
            {
                components.ConfigureComponent<EncryptionVerifier>(DependencyLifecycle.SingleInstance);
            });
        busConfiguration.EnableInstallers();
        var startableBus = Bus.Create(busConfiguration);
        return startableBus.Start();
    }

}