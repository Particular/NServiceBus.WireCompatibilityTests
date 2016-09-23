using CommonMessages;
using NServiceBus;
using NServiceBus.Saga;

namespace Common.Saga
{
#if(Version3 || Version4)

    public class RequestingSaga : Saga<RequestingSagaData>,
        IAmStartedByMessages<SagaInitiateRequestingMessage>,
        IHandleMessages<SagaResponseFromOtherMessage>
    {
        public void Handle(SagaInitiateRequestingMessage message)
        {
            var newMessage = new SagaRequestToRespondingMessage
                {
                    Sender = TestRunner.EndpointName
                };
            Bus.Send(message.TargetEndpoint, newMessage);
        }

        public void Handle(SagaResponseFromOtherMessage message)
        {
            SagaVerifier.RequestingSagaGotTheResponse.Add(message.Sender);
            MarkAsComplete();
        }
    }

#endif
#if(Version5 || Version6)

    public class RequestingSaga : Saga<RequestingSagaData>,
        IAmStartedByMessages<SagaInitiateRequestingMessage>,
        IHandleMessages<SagaResponseFromOtherMessage>
    {
        public void Handle(SagaInitiateRequestingMessage message)
        {
            var newMessage = new SagaRequestToRespondingMessage
            {
                Sender = TestRunner.EndpointName
            };
            Bus.Send(message.TargetEndpoint, newMessage);
        }

        public void Handle(SagaResponseFromOtherMessage message)
        {
            SagaVerifier.RequestingSagaGotTheResponse.Add(message.Sender);
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<RequestingSagaData> mapper)
        {
        }
    }

#endif
}