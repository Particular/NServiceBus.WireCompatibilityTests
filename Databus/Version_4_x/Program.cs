using System.Threading;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    static void Main()
    {
        //HACK: for trial dialog issue https://github.com/Particular/NServiceBus/issues/2001
        var synchronizationContext = SynchronizationContext.Current;
        var bus = CreateBus();
        SynchronizationContext.SetSynchronizationContext(synchronizationContext);
        TestRunner.RunTests(bus);
    }

    static UnicastBus CreateBus()
    {
        Configure.GetEndpointNameAction = () => EndpointNames.EndpointName;

        Logging.ConfigureLogging();
        Asserter.LogError = log4net.LogManager.GetLogger("Asserter").Error;
        Configure.Features.Enable<Sagas>();
        Configure.Serialization.Json();
        var configure = Configure.With();
        configure.DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("CommonMessages"));
        configure.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));
        configure.DefaultBuilder();
        configure.UseTransport<Msmq>();
        configure.InMemorySagaPersister();
        configure.UseInMemoryTimeoutPersister();
        configure.InMemorySubscriptionStorage();
        configure.FileShareDataBus(@"..\tempstorage");

        return (UnicastBus) configure.UnicastBus()
            .CreateBus().Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }
}