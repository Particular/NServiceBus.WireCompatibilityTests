using System.Reflection;
using System.Threading;
using NServiceBus;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

class Program
{
    static string endpointName = $"WireCompatCallbacks{Assembly.GetExecutingAssembly().GetName().Name}";
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
        Asserter.LogError = log4net.LogManager.GetLogger("Asserter").Error;
        var configure = Configure.With();
        configure.DisableTimeoutManager();
        configure.DefiningMessagesAs(t => t.Namespace != null && (t.Namespace.StartsWith("CommonMessages")));
        configure.DefaultBuilder();
        configure.MsmqTransport();
        configure.JsonSerializer();

        return (UnicastBus) configure.UnicastBus()
            .CreateBus().Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }
}