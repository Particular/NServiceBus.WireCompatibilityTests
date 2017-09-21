using System.Threading.Tasks;
using NServiceBus;

class Program
{
    public static async Task Main()
    {
        var bus = await CreateBus()
            .ConfigureAwait(false);
        await TestRunner.RunTests(bus)
            .ConfigureAwait(false);
    }

    static Task<IEndpointInstance> CreateBus()
    {
        Asserter.LogError = NServiceBus.Logging.LogManager.GetLogger("Asserter").Error;
        var endpointConfiguration = new EndpointConfiguration(EndpointNames.EndpointName);
        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningMessagesAs(MessageConventions.IsMessage);
        conventions.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        var recoverability = endpointConfiguration.Recoverability();
#pragma warning disable 618
        recoverability.DisableLegacyRetriesSatellite();
#pragma warning restore 618
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();

        endpointConfiguration.UseDataBus<FileShareDataBus>().BasePath(@"..\tempstorage");

        endpointConfiguration.EnableInstallers();

        return Endpoint.Start(endpointConfiguration);
    }
}