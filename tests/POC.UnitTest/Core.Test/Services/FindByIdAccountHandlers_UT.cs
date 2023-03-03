using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Mappers;
using Core.Services;
using Moq;

namespace Core.Test.Services;

[Trait("Category", "Unit")]
public class FindByIdAccountHandlers_UT
{
    private FindByIdAccountHandlers _findByIdAccountHandlers;
    private Mock<IAccountRepository> _repository;

    public FindByIdAccountHandlers_UT()
    {
        var configuration = new MapperConfiguration(cfg
               => cfg.AddProfile(new MappingProfile()));
        var mapper = new Mapper(configuration);

        _repository = new Mock<IAccountRepository>();
        _repository.Setup(x => x.FindById(1))
                   .Returns(new Account()
                   {
                       Id = 1,
                       Address = "Addres1",
                       Phone = "+57354854121"
                   });
        _findByIdAccountHandlers = new FindByIdAccountHandlers(_repository.Object, mapper);
    }

    [Fact]
    public async Task GivenHanldeIsCalled_WhenSimpleCall_ThenReturnAccountFiltered()
    {
        // Arrange
        var request = new FindByIdAccountRequest() { AccountId = 1 };

        // Act
        var response = await _findByIdAccountHandlers.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Addres1", response.Address);
    }

}