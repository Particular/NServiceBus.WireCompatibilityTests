using System.Text;
// ReSharper disable RedundantUsingDirective
using NServiceBus;
using NServiceBus.MessageMutator;
using NServiceBus.Unicast.Transport;
// ReSharper restore RedundantUsingDirective

public class EncryptionVerifier : IMutateIncomingTransportMessages
{

    public void MutateIncoming(TransportMessage transportMessage)
    {
        var messageString = Encoding.Default.GetString(transportMessage.Body);
        Asserter.IsTrue(!messageString.Contains("Secret"), "Message property was not encrypted");
    }
}