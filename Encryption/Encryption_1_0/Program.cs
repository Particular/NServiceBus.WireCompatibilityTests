using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

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
        Asserter.LogError = NServiceBus.Logging.LogManager.GetLogger("Asserter").Error;
        var endpointConfiguration = new EndpointConfiguration(EndpointNames.EndpointName);
        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningMessagesAs(MessageConventions.IsMessage);

        endpointConfiguration.DisableFeature<TimeoutManager>();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        var recoverabilitySettings = endpointConfiguration.Recoverability();
#pragma warning disable 618
        recoverabilitySettings.DisableLegacyRetriesSatellite();
#pragma warning restore 618
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.MakeInstanceUniquelyAddressable("1");


        ConfigureEncryption(endpointConfiguration);

        endpointConfiguration.RegisterComponents(
            components =>
            {
                components.ConfigureComponent<EncryptionVerifier>(DependencyLifecycle.SingleInstance);
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

        var keys = new Dictionary<string, byte[]>
        {
            {keyIdentifier, encryptionKey}
        };
        var encryptionService = new NServiceBus.Encryption.MessageProperty.RijndaelEncryptionService(keyIdentifier, keys, decryptionKeys);

        NServiceBus.Encryption.MessageProperty.EncryptionConfigurationExtensions.EnableMessagePropertyEncryption(
            configuration: endpointConfiguration,
            encryptionService: encryptionService,
            encryptedPropertyConvention: MessageConventions.IsEncryptedProperty);
    }
}