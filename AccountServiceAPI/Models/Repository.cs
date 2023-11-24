using AccountServiceAPI;
using CommonModels;
using System;

namespace Models
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }
        public async Task<int> AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account.AccountId;
        }
        public List<Account> GetAccountsByUserId(int userId)
        {
            // Assuming _context is your database context
            return _context.Accounts.Where(a => a.UserId == userId).ToList();
        }
    }
}
