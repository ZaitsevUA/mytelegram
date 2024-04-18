namespace MyTelegram.Domain.Sagas.Events;

public class SignInStartedEvent(RequestInfo requestInfo) : RequestAggregateEvent2<SignInSaga, SignInSagaId>(requestInfo);
