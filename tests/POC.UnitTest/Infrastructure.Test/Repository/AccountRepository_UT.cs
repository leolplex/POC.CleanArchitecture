using Core.Entities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Test.Repository;

[Trait("Category", "Unit")]
public class AccountRepository_UT
{
    private AccountRepository _accountRepository;
    private readonly Mock<IPOCDbContext> mockedDbContext;
    private Mock<DbSet<Account>> mockAccount;

    public AccountRepository_UT()
    {


        mockedDbContext = new Mock<IPOCDbContext>();

        mockAccount = new Mock<DbSet<Account>>();
        mockedDbContext.Setup(c => c.Accounts).Returns(mockAccount.Object);
        populateDatabaseAccountMock(
            new List<Account>()
            {
                 new() { Address = "some address", Id = 1, Phone = "+57241" }
            }.AsQueryable());
        _accountRepository = new AccountRepository(mockedDbContext.Object);
    }

    [Fact]
    public void GivenFindAllIsCalled_WhenSimpleCall_ThenReturnAllAccounts()
    {
        // Arrange && Act
        var result = _accountRepository.FindAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("some address", result.FirstOrDefault().Address);
    }

    [Fact]
    public void GivenFindByIdCalled_WhenSimpleCall_ThenReturnAllAccounts()
    {
        // Arrange && Act
        var result = _accountRepository.FindById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("some address", result.Address);
    }

    [Fact]
    public async Task GivenInsertCalled_WhenSimpleCall_ThenReturnAllAccountsAsync()
    {
        // Arrange && Act
        _accountRepository.Insert(new Account() { Address = "other address", Id = 2, Phone = "+578888" });
        await _accountRepository.SaveAsync();

        // Assert        
        mockedDbContext.Verify(x => x.SaveChanges(CancellationToken.None),
                                                  Times.Once());
    }

    private void populateDatabaseAccountMock(IQueryable<Account> accountDataMock)
    {
        mockAccount = new Mock<DbSet<Account>>();
        mockAccount.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accountDataMock.Provider);
        mockAccount.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accountDataMock.Expression);
        mockAccount.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accountDataMock.ElementType);
        mockAccount.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(accountDataMock.GetEnumerator());
        mockedDbContext.Setup(c => c.Accounts).Returns(mockAccount.Object);
    }
}