using MediatR;
using MediatRSample.Application.Notifications;

namespace MediatRSample.Application.EventHandlers;

public class LogEventHandler : INotificationHandler<PessoaCriadaNotification>, INotificationHandler<ErrorNotification>
{
    public Task Handle(PessoaCriadaNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"CRIACAO: '{notification.Id} - {notification.Nome} - {notification.Idade} - {notification.Sexo}'");
        }, cancellationToken);
    }

    public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"ERRO: '{notification.Excecao} \n {notification.PilhaErro}'");
        }, cancellationToken);
    }
}
