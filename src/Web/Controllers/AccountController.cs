using Core.DTOs;
using Core.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

    private readonly ILogger<AccountController> _logger;
    private readonly IMediator _mediator;


    public AccountController(ILogger<AccountController> logger,
                             IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;

    }

    [HttpGet("~/findAllAccount")]
    public async Task<IActionResult> GetAsync()
    {
        var response = await _mediator.Send(new FindAllAccountRequest());
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpGet("~/findByIdAccount/{accountId}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] double accountId)
    {
        var response = await _mediator.Send(new FindByIdAccountRequest()
        {
            AccountId = accountId
        });
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpPost(Name = "AddAccount")]
    public async Task<IActionResult> InsertAsync([FromBody] InsertAccountRequest account)
    {
        (AccountDTO result, IEnumerable<AccontErrorDTO> validationResult) = await _mediator.Send(account);
        if (result is null)
            return StatusCode(StatusCodes.Status400BadRequest, validationResult);

        return StatusCode(StatusCodes.Status201Created, result);
    }
}
