namespace Infrastructure.Repository;

using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

public class AccountRepository : IAccountRepository
{
    private readonly IPOCDbContext _context;

    public AccountRepository()
    {
        _context = new POCDBContext();
    }

    public AccountRepository(IPOCDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Account> FindAll()
    {
        return _context.Accounts.ToList();
    }

    public Account FindById(double accountId)
    {
        return _context.Accounts.FirstOrDefault(x => x.Id == accountId);
    }

    public void Insert(Account entity)
    {
        _context.Accounts.Add(entity);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChanges();
    }
}