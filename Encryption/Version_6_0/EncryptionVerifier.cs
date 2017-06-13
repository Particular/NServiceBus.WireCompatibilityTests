using System.Text;
using System.Threading.Tasks;
using NServiceBus.MessageMutator;

public class EncryptionVerifier : IMutateIncomingTransportMessages
{

    public Task MutateIncoming(MutateIncomingTransportMessageContext context)
    {
        var messageString = Encoding.Default.GetString(context.Body);
        Asserter.IsTrue(!messageString.Contains("Secret"), "Message property was not encrypted");

        return Task.FromResult(0);
    }
}