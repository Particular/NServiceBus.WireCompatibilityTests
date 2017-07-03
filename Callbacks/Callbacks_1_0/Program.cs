using System.Reflection;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

class Program
{
    static string endpointName = $"WireCompatCallbacks{Assembly.GetExecutingAssembly().GetName().Name}";

    public static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        var bus = await CreateBus()
            .ConfigureAwait(false);
        TestRunner.EndpointName = endpointName;
        await TestRunner.RunTests(bus)
            .ConfigureAwait(false);
    }

    static Task<IEndpointInstance> CreateBus()
    {
        var endpointConfiguration = new EndpointConfiguration(endpointName);
        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningMessagesAs(t => t.Namespace != null && (t.Namespace.StartsWith("CommonMessages")));

        endpointConfiguration.DisableFeature<TimeoutManager>();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        var recoverabilitySettings = endpointConfiguration.Recoverability();
#pragma warning disable 618
        recoverabilitySettings.DisableLegacyRetriesSatellite();
#pragma warning restore 618
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        // Required by callbacks to have each instance uniquely addressable
        endpointConfiguration.MakeInstanceUniquelyAddressable("1");
#if (CallbacksV2)
        endpointConfiguration.EnableCallbacks();
#endif
        endpointConfiguration.EnableInstallers();

        return Endpoint.Start(endpointConfiguration);
    }

}