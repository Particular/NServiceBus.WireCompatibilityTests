using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main()
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
        endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();

        endpointConfiguration.UseDataBus<FileShareDataBus>().BasePath(@"..\tempstorage");

        endpointConfiguration.EnableInstallers();

        return Endpoint.Start(endpointConfiguration);
    }
}