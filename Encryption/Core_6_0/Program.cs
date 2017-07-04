using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

class Program
{

    public static void Main()
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
        Asserter.LogError = NServiceBus.Logging.LogManager.GetLogger("Asserter").Error;
        var endpointConfiguration = new EndpointConfiguration(EndpointNames.EndpointName);
        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningMessagesAs(MessageConventions.IsMessage);
#pragma warning disable 618
        conventions.DefiningEncryptedPropertiesAs(MessageConventions.IsEncryptedProperty);
#pragma warning restore 618

        endpointConfiguration.DisableFeature<TimeoutManager>();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        var recoverability = endpointConfiguration.Recoverability();
#pragma warning disable 618
        recoverability.DisableLegacyRetriesSatellite();
#pragma warning restore 618
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.MakeInstanceUniquelyAddressable("1");


        ConfigureEncryption(endpointConfiguration);

        endpointConfiguration.RegisterComponents(
            components =>
            {
                components.ConfigureComponent<MutatorVerifier>(DependencyLifecycle.SingleInstance);
            });
        endpointConfiguration.EnableInstallers();

        return Endpoint.Start(endpointConfiguration);
    }

    static void ConfigureEncryption(EndpointConfiguration endpointConfiguration)
    {
        var encryptionKey = Encoding.ASCII.GetBytes("gdDbqRpqdRbTs3mhdZh9qCaDaxJXl+e6");
        var decryptionKeys = new List<byte[]>
        {
            encryptionKey
        };
        var keyIdentifier = "20151014";

        var conventions = endpointConfiguration.Conventions();
#pragma warning disable 618
        conventions.DefiningEncryptedPropertiesAs(p => p.Name.StartsWith("Encrypted"));
        endpointConfiguration.RijndaelEncryptionService(keyIdentifier, encryptionKey, decryptionKeys);
#pragma warning restore 618
    }
}