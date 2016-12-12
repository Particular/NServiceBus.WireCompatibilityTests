using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

namespace Common.Saga
{
    public class RespondingSaga : Saga<RespondingSagaData>,
        IAmStartedByMessages<SagaRequestToRespondingMessage>
    {
        public Task Handle(SagaRequestToRespondingMessage message, IMessageHandlerContext context)
        {
            return context.Reply(new SagaResponseFromOtherMessage
            {
                Sender = TestRunner.EndpointName
            });
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<RespondingSagaData> mapper)
        {
        }
    }
}