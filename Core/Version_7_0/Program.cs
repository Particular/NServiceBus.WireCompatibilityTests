using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        var bus = await CreateBus()
            .ConfigureAwait(false);
        await TestRunner.RunTests(bus)
            .ConfigureAwait(false);
    }

    static Task<IEndpointInstance> CreateBus()
    {
        var endpointConfiguration = new EndpointConfiguration(EndpointNames.EndpointName);
        var conventions = endpointConfiguration.Conventions();
        conventions.ApplyMessageConventions();

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        var transport = endpointConfiguration.UseTransport<MsmqTransport>();
        PubSubConfigOverride.RegisterPublishers(transport);
        endpointConfiguration.UsePersistence<InMemoryPersistence>();

        endpointConfiguration.UseDataBus<FileShareDataBus>().BasePath("..\\..\\..\\tempstorage");

        endpointConfiguration.EnableInstallers();

        return Endpoint.Start(endpointConfiguration);
    }

}