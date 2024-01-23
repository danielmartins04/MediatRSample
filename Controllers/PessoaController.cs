using MediatR;
using MediatRSample.Application.Commands;
using MediatRSample.Application.Models;
using MediatRSample.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MediatRSample.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IRepository<Pessoa> _repository;

    public PessoaController(IMediator mediator, IRepository<Pessoa> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Post(CadastraPessoaCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
