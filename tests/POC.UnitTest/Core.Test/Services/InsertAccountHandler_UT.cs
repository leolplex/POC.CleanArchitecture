using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Mappers;
using Core.Services;
using Core.Validations;
using Moq;

namespace Core.Test.Services;

[Trait("Category", "Unit")]
public class InsertAccountHandler_UT
{
    private InsertAccountHandler _insertAccountHandler;
    private Mock<IAccountRepository> _repository;

    private AccountValidator _validator;


    public InsertAccountHandler_UT()
    {
        var configuration = new MapperConfiguration(cfg
               => cfg.AddProfile(new MappingProfile()));
        var mapper = new Mapper(configuration);

        _validator = new AccountValidator();

        _repository = new Mock<IAccountRepository>();
        _repository.Setup(x => x.FindById(It.IsAny<double>()))
               .Returns(new Account()
               {
                   Id = 1,
                   Address = "TestAddress",
                   Phone = "5624521262"
               });


        _insertAccountHandler = new InsertAccountHandler(_repository.Object, mapper, _validator);
    }

    [Fact]
    public async Task GivenHanldeIsCalled_WhenSimpleCall_ThenReturnAccountInserted()
    {
        // Arrange
        var request = new InsertAccountRequest() { Address = "TestAddress", Phone = "5624521262" };

        // Act
        (AccountDTO account, IEnumerable<AccontErrorDTO> errors) = await _insertAccountHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("TestAddress", account.Address);
    }

}