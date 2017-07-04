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
        Asserter.LogError = NServiceBus.Logging.LogManager.GetLogger("Asserter").Error;
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName(EndpointNames.EndpointName);
        var conventions = busConfiguration.Conventions();
        conventions.DefiningMessagesAs(MessageConventions.IsMessage);
        conventions.DefiningEncryptedPropertiesAs(MessageConventions.IsEncryptedProperty);
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