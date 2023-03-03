using Core.Services;
using Core.Validations;
using FluentValidation.TestHelper;

namespace Core.Test.Validations;

[Trait("Category", "Unit")]
public class AccountValidator_UT
{
    private AccountValidator _accountValidator;

    public AccountValidator_UT()
    {
        _accountValidator = new AccountValidator();
    }

    [Fact]
    public void GivenValidationIsCalled_WhenAddressAndPhoneIsNotSet_ThenReturnErrors()
    {
        // Arrange
        var account = new InsertAccountRequest();

        // Act
        var result = _accountValidator.TestValidate(account);

        // Assert
        result.ShouldHaveValidationErrorFor(account => account.Address);
        result.ShouldHaveValidationErrorFor(account => account.Phone);
    }

    [Fact]
    public void GivenValidationIsCalled_WhenPhoneIsNotNumber_ThenReturnError()
    {
        // Arrange
        var account = new InsertAccountRequest() { Address = "Some Address", Phone = "Some phone" };

        // Act
        var result = _accountValidator.TestValidate(account);

        // Assert
        result.ShouldNotHaveValidationErrorFor(account => account.Address);
        result.ShouldHaveValidationErrorFor(account => account.Phone);
    }

}