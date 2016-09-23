using System;
using System.Reflection;
using NServiceBus;

public static class PubSubInitiator
{
    public static void InitiatePubSub(this IBus bus)
    {
        var messagesAssemblyName = Assembly.GetExecutingAssembly().GetName().Name + ".Messages";
        var typeName = messagesAssemblyName + ".MyEvent, " + messagesAssemblyName;
        var messageType = Type.GetType(typeName, true);
        var message = (dynamic)Activator.CreateInstance(messageType);
        message.Sender = TestRunner.EndpointName;
        bus.Publish((object)message);
    }
}