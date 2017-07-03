using System.Reflection;
using System.Threading;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    static string endpointName = $"WireCompatCallbacks{Assembly.GetExecutingAssembly().GetName().Name}";
    public static void Main()
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
        Configure.Features.Disable<TimeoutManager>();
        Configure.Serialization.Json();
        var configure = Configure.With();
        configure.DefiningMessagesAs(t => t.Namespace != null && (t.Namespace.StartsWith("CommonMessages")));
        configure.DefaultBuilder();
        configure.UseTransport<Msmq>();

        return (UnicastBus) configure.UnicastBus()
            .CreateBus().Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }
}