using NServiceBus;

public class EventHandler :
    IHandleMessages<Version_3_x.Messages.MyEvent>,
    IHandleMessages<Version_4_x.Messages.MyEvent>,
    IHandleMessages<Version_5_0.Messages.MyEvent>,
    IHandleMessages<Version_5_1.Messages.MyEvent>,
    IHandleMessages<Version_5_2.Messages.MyEvent>,
    IHandleMessages<Version_6_4.Messages.MyEvent>,
    IHandleMessages<Version_7_0.Messages.MyEvent>
{
    public void Handle(Version_3_x.Messages.MyEvent message)
    {
        PubSubVerifier.EventReceivedFrom.Add(message.Sender);
    }

    public void Handle(Version_4_x.Messages.MyEvent message)
    {
        PubSubVerifier.EventReceivedFrom.Add(message.Sender);
    }

    public void Handle(Version_5_0.Messages.MyEvent message)
    {
        PubSubVerifier.EventReceivedFrom.Add(message.Sender);
    }

    public void Handle(Version_5_1.Messages.MyEvent message)
    {
        PubSubVerifier.EventReceivedFrom.Add(message.Sender);
    }

    public void Handle(Version_5_2.Messages.MyEvent message)
    {
        PubSubVerifier.EventReceivedFrom.Add(message.Sender);
    }
    
    public void Handle(Version_6_4.Messages.MyEvent message)
    {
        PubSubVerifier.EventReceivedFrom.Add(message.Sender);
    }

    public void Handle(Version_7_0.Messages.MyEvent message)
    {
        PubSubVerifier.EventReceivedFrom.Add(message.Sender);
    }
}