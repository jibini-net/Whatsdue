using Generated;

namespace Whatsdue.Services;

public class AccountService : Account.IBackendService
{
    private readonly Account.Repository repo;
    public AccountService(Account.Repository repo)
    {
        this.repo = repo;
    }

    public async Task<Account> Get(int Id)
    {
        return await repo.dbo__Account_GetById(Id);
    }

    public async Task<List<Account>> List()
    {
        return await repo.dbo__Account_List();
    }
}
