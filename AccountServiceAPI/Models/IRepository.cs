using CommonModels;

namespace Models
{
    public interface IRepository
    {
        User GetUserById(int userId);
        Task<int> AddAccountAsync(Account account);
        List<Account> GetAccountsByUserId(int userId);
    }
}
