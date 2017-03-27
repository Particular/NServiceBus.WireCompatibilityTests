using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static string endpointName = $"WireCompat{Assembly.GetExecutingAssembly().GetName().Name}";

    static void Main()
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
        conventions.ApplyMessageConventions();

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        var recoverabilitySettings = endpointConfiguration.Recoverability();
#pragma warning disable 618
        recoverabilitySettings.DisableLegacyRetriesSatellite();
#pragma warning restore 618
        endpointConfiguration.UseTransport<MsmqTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();

        ConfigureEncryption(endpointConfiguration);

        endpointConfiguration.UseDataBus<FileShareDataBus>().BasePath("..\\..\\..\\tempstorage");

        // Required by callbacks to have each instance uniquely addressable
        endpointConfiguration.MakeInstanceUniquelyAddressable("1");

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

#if (UseExternalEncryption)
        var keys = new Dictionary<string, byte[]>
        {
            {keyIdentifier, encryptionKey}
        };
        var encryptionService = new NServiceBus.Encryption.MessageProperty.RijndaelEncryptionService(keyIdentifier, keys, decryptionKeys);

        NServiceBus.Encryption.MessageProperty.EncryptionConfigurationExtensions.EnableMessagePropertyEncryption(
            configuration: endpointConfiguration,
            encryptionService: encryptionService,
            encryptedPropertyConvention: p => p.Name.StartsWith("Encrypted"));
#else
        var conventions = endpointConfiguration.Conventions();
#pragma warning disable 618
        conventions.DefiningEncryptedPropertiesAs(p => p.Name.StartsWith("Encrypted"));
        endpointConfiguration.RijndaelEncryptionService(keyIdentifier, encryptionKey, decryptionKeys);
#pragma warning restore 618
#endif
    }
}