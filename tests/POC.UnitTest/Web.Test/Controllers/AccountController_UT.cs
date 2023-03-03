using System.Net;
using Core.DTOs;
using Core.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Controllers;

namespace Web.Test.Controller;

[Trait("Category", "Unit")]
public class AccountController_UT
{
    private AccountController _accountController;
    private Mock<ILogger<AccountController>> _logger;
    private Mock<IMediator> _mediator;

    public AccountController_UT()
    {
        _logger = new Mock<ILogger<AccountController>>();
        _mediator = new Mock<IMediator>();
        _accountController = new AccountController(_logger.Object, _mediator.Object);
    }

    [Fact]
    public async Task GivenFindAllAccountIsCalled_WhenSimpleCall_ThenReturnAllAccounts()
    {
        // Arrange
        _mediator.Setup(x => x.Send(It.IsAny<FindAllAccountRequest>(), CancellationToken.None)).ReturnsAsync(new List<AccountDTO>());

        // Act
        var result = await _accountController.GetAsync();

        // Assert
        var controllerResponse = result as ObjectResult;
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)controllerResponse.StatusCode);

    }

    [Fact]
    public async Task GivenFindByIdAccountIsCalled_WhenSimpleCall_ThenReturnAnAccount()
    {
        // Arrange
        _mediator.Setup(x => x.Send(It.IsAny<FindByIdAccountRequest>(), CancellationToken.None)).ReturnsAsync(new AccountDTO());

        // Act
        var result = await _accountController.GetByIdAsync(1);

        // Assert
        var controllerResponse = result as ObjectResult;
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)controllerResponse.StatusCode);
    }

    [Fact]
    public async Task GivenInsertAccountIsCalled_WhenSimpleCall_ThenReturnAccountAdded()
    {
        // Arrange
        _mediator.Setup(x => x.Send(It.IsAny<InsertAccountRequest>(), CancellationToken.None)).ReturnsAsync((new AccountDTO(), new List<AccontErrorDTO>()));

        // Act
        var result = await _accountController.InsertAsync(new InsertAccountRequest());

        // Assert
        var controllerResponse = result as ObjectResult;
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)controllerResponse.StatusCode);
    }

}