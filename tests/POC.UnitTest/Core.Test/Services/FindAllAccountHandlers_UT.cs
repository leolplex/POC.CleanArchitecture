
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Mappers;
using Core.Services;
using Moq;

namespace Core.Test.Services;

[Trait("Category", "Unit")]
public class FindAllAccountHandlers_UT
{
    private FindAllAccountHandlers _findAllAccountHandlers;
    private Mock<IAccountRepository> _repository;

    public FindAllAccountHandlers_UT()
    {
        var configuration = new MapperConfiguration(cfg
               => cfg.AddProfile(new MappingProfile()));
        var mapper = new Mapper(configuration);

        _repository = new Mock<IAccountRepository>();
        _repository.Setup(x => x.FindAll())
                   .Returns(new List<Account>()
                   { new() {
                        Id = 1,
                        Address = "Addres1",
                        Phone ="+57354854121" }
                    });
        _findAllAccountHandlers = new FindAllAccountHandlers(_repository.Object, mapper);
    }

    [Fact]
    public async Task GivenHanldeIsCalled_WhenSimpleCall_ThenReturnAllAccounts()
    {
        // Arrange
        var request = new FindAllAccountRequest();

        // Act
        var response = await _findAllAccountHandlers.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(1, response.Count());
    }

}