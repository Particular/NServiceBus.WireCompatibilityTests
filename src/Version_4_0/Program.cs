using System.Reflection;
using System.Threading;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    static string endpointName = "WireCompat" + Assembly.GetExecutingAssembly().GetName().Name;
    static void Main()
    {

        //HACK: for trial dialog issue https://github.com/Particular/NServiceBus/issues/2001
        var synchronizationContext = SynchronizationContext.Current;
        var bus = CreateBus();
        SynchronizationContext.SetSynchronizationContext(synchronizationContext);
        TestRunner.EndpointName = endpointName;
        TestRunner.RunTests(bus);
    }

    static UnicastBus CreateBus()
    {
        Configure.GetEndpointNameAction = () => endpointName;

        Logging.ConfigureLogging();
        Configure.Features.Enable<Sagas>();
        Configure.Serialization.Json();
        var configure = Configure.With();
        configure.ApplyMessageConventions();
        configure.DefaultBuilder();
        configure.UseTransport<Msmq>();
        configure.InMemorySagaPersister();
        configure.UseInMemoryTimeoutPersister();
        configure.InMemorySubscriptionStorage();
        configure.RijndaelEncryptionService();
        configure.FileShareDataBus("..\\..\\..\\tempstorage");
        configure.Configurer.ConfigureComponent<EncryptionVerifier>(DependencyLifecycle.SingleInstance);

        return (UnicastBus) configure.UnicastBus()
            .CreateBus().Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }
}