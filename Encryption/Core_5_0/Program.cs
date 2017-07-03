using NServiceBus;
using NServiceBus.Features;

public class Program
{

    public static void Main()
    {
        var bus = CreateBus();
        TestRunner.RunTests(bus);
    }

    static IBus CreateBus()
    {
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName(EndpointNames.EndpointName);
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