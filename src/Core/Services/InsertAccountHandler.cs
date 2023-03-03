using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Core.Services;

public class InsertAccountRequest : IRequest<(AccountDTO, IEnumerable<AccontErrorDTO>)>
{
    public string Address { get; set; }
    public string Phone { get; set; }
}

public class InsertAccountHandler : IRequestHandler<InsertAccountRequest, (AccountDTO, IEnumerable<AccontErrorDTO>)>
{
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;
    private IValidator<InsertAccountRequest> _validator;


    public InsertAccountHandler(IAccountRepository repository,
                                IMapper mapper,
                                IValidator<InsertAccountRequest> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<(AccountDTO, IEnumerable<AccontErrorDTO>)> Handle(InsertAccountRequest request, CancellationToken cancellationToken)
    {
        ValidationResult resultValidations = await _validator.ValidateAsync(request);
        if (resultValidations.IsValid is true)
        {
            var accountMapped = _mapper.Map<Account>(request);
            _repository.Insert(accountMapped);
            await _repository.SaveAsync();
            var result = _mapper.Map<AccountDTO>(_repository.FindById(accountMapped.Id));
            return (result, null);
        }

        var resultErrors = _mapper.Map<IEnumerable<AccontErrorDTO>>(resultValidations.Errors);

        return (null, resultErrors);
    }
}