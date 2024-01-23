using MediatR;
using MediatRSample.Application.Commands;
using MediatRSample.Application.Models;
using MediatRSample.Application.Notifications;
using MediatRSample.Repositories;

namespace MediatRSample.Application.Handlers;

public class CadastrarPessoaCommandHandler : IRequestHandler<CadastraPessoaCommand, string>
{
    private readonly IRepository<Pessoa> _repository;
    private readonly IMediator _mediator;

    public CadastrarPessoaCommandHandler(IRepository<Pessoa> repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<string> Handle(CadastraPessoaCommand request, CancellationToken cancellationToken)
    {
        var pessoa = new Pessoa { Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo };

        try
        {
            await _repository.Add(pessoa);
            await _mediator.Publish(new PessoaCriadaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });
            return await Task.FromResult("successfully created person");
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new PessoaCriadaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });
            await _mediator.Publish(new ErrorNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
            return await Task.FromResult("error when creating person");
        }
    }
}
