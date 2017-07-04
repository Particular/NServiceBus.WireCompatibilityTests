using NServiceBus;

public class ResponseHandler : IHandleMessages<ResponseMessage>
{
    public IBus Bus { get; set; }

    public void Handle(ResponseMessage message)
    {
        Verifier.ResponseReceivedFromSites.Add(message.Sender);
        Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
    }
}