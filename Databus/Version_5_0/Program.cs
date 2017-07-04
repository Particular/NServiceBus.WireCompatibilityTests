using NServiceBus;

class Program
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
        conventions.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.UseTransport<MsmqTransport>();
        busConfiguration.UsePersistence<InMemoryPersistence>();
#if (Version6)
        busConfiguration.UseDataBus<FileShareDataBus>().BasePath(@"..\tempstorage");
#else
#pragma warning disable 618
        busConfiguration.FileShareDataBus(@"..\tempstorage");
#pragma warning restore 618
#endif
        busConfiguration.EnableInstallers();
        var startableBus = Bus.Create(busConfiguration);
        return startableBus.Start();
    }

}