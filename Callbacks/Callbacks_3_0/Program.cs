using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

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

        endpointConfiguration.DisableFeature<TimeoutManager>();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        // Required by callbacks to have each instance uniquely addressable
        endpointConfiguration.MakeInstanceUniquelyAddressable("1");
        endpointConfiguration.EnableCallbacks();
        endpointConfiguration.EnableInstallers();

        return Endpoint.Start(endpointConfiguration);
    }
}