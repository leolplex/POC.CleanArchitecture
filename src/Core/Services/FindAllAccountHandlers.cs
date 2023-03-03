using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using MediatR;

namespace Core.Services;

public class FindAllAccountRequest : IRequest<IEnumerable<AccountDTO>>
{
}

public class FindAllAccountHandlers : IRequestHandler<FindAllAccountRequest, IEnumerable<AccountDTO>>
{
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;


    public FindAllAccountHandlers(IAccountRepository repository,
                                  IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<IEnumerable<AccountDTO>> Handle(FindAllAccountRequest request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<IEnumerable<AccountDTO>>(_repository.FindAll());
        return Task.FromResult(result);
    }
}