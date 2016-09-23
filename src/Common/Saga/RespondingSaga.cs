using CommonMessages;
using NServiceBus.Saga;

namespace Common.Saga
{
#if(Version3 || Version4)

    public class RespondingSaga : Saga<RespondingSagaData>,
        IAmStartedByMessages<SagaRequestToRespondingMessage>
    {
        public void Handle(SagaRequestToRespondingMessage message)
        {
            Bus.Reply(new SagaResponseFromOtherMessage
                {
                    Sender = TestRunner.EndpointName
                });
        }

        public override void ConfigureHowToFindSaga()
        {
        }
    }

#elif(Version5 || Version6)

    public class RespondingSaga : Saga<RespondingSagaData>,
        IAmStartedByMessages<SagaRequestToRespondingMessage>
    {
        public void Handle(SagaRequestToRespondingMessage message)
        {
            Bus.Reply(new SagaResponseFromOtherMessage
            {
                Sender = TestRunner.EndpointName
            });
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<RespondingSagaData> mapper)
        {
        }
    }

#endif
}