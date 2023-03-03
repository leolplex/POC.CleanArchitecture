using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using MediatR;

namespace Core.Services;

public class FindByIdAccountRequest : IRequest<AccountDTO>
{
    public double AccountId { get; set; }
}

public class FindByIdAccountHandlers : IRequestHandler<FindByIdAccountRequest, AccountDTO>
{
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;


    public FindByIdAccountHandlers(IAccountRepository repository,
                                   IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<AccountDTO> Handle(FindByIdAccountRequest request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<AccountDTO>(_repository.FindById(request.AccountId));
        return Task.FromResult(result);
    }
}