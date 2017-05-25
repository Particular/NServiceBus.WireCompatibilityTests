using System;
using NServiceBus;
public class PubSubConfigOverride
{
    public static void RegisterPublishers(TransportExtensions<MsmqTransport> transport)
    {
        var routing = transport.Routing();
        foreach (var endpointName in EndpointNames.All)
        {
            var assemblyName = endpointName.Replace("WireCompat","");
            var eventType = Type.GetType($"{assemblyName}.Messages.MyEvent, {assemblyName}.Messages");
            routing.RegisterPublisher(eventType, endpointName);
        }
    }
}
