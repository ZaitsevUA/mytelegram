namespace MyTelegram.Domain.Sagas.Events;

public class SignUpRequiredEvent(RequestInfo requestInfo)
    : RequestAggregateEvent2<SignInSaga, SignInSagaId>(requestInfo);
