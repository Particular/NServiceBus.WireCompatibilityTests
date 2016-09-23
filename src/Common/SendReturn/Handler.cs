using CommonMessages;
using NServiceBus;

namespace Common.SendReturn
{
    public class Handler : IHandleMessages<SendReturnMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(SendReturnMessage sendReturnMessage)
        {
            Bus.Return(5);
        }
    }
}