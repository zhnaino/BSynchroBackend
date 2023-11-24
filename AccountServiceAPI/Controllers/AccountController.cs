using Models;
using CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace AccountService.Controllers
{
    [Route("api/account")] 
    [ApiController]
    public class AccountController : ControllerBase
    {
       
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRepository _repository;
        public AccountController(IRepository repository, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _repository = repository;
        }
        [HttpPost("open")]
        public async Task<IActionResult> OpenAccount(AccountRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }
            if (request.UserId <= 0 || request.InitialDeposit < 0)
            {
                return BadRequest("Invalid request");
            }
            var user = _repository.GetUserById(request.UserId);

            if (user != null)
            {
                var account = new Account { UserId = user.UserId };
                account.Balance += request.InitialDeposit;
                int accountId = await _repository.AddAccountAsync(account);
               
                if (request.InitialDeposit != 0)
                {
                    var transactionsApi = _httpClientFactory.CreateClient();
                    var transactionsApiUrl = "https://localhost:7094/api/Transaction/add_transaction";
                    var transaction = new TransactionRequest
                    {
                        AccountId = accountId,
                        Deposit = request.InitialDeposit
                    };
                    
                    var transactionsApiResponse = await transactionsApi.PostAsJsonAsync(transactionsApiUrl, transaction);

                    if (!transactionsApiResponse.IsSuccessStatusCode)
                    {
                        return BadRequest("Failed to add the initial credit.");
                    }
                }
            }
            else
                return BadRequest("User not Exist");
            return Ok(new { Message = "Account opened successfully" });

        }
        [HttpGet("userinfo")]
        public async Task<IActionResult> UserInfoAsync(int userId)
        {
            var user = _repository.GetUserById(userId);

            if (user != null)
            {
                List<Account> accounts = _repository.GetAccountsByUserId(userId);
                var transactionsApi = _httpClientFactory.CreateClient();
                var transactionsApiUrl = "https://localhost:7094/api/Transaction/getAllTransactions";

                var response = await transactionsApi.GetAsync(transactionsApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var transactions = await response.Content.ReadFromJsonAsync<List<Transaction>>();

                    // Map transactions to accounts
                    var accountTransactions = transactions
                        .GroupBy(t => t.AccountId)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    // Map accounts to user
                    var userAccounts = accounts.Select(account => new
                    {
                        AccountId = account.AccountId,
                        Balance = account.Balance,
                        Transactions = accountTransactions.ContainsKey(account.AccountId)
                            ? accountTransactions[account.AccountId]
                            : new List<Transaction>()
                    }).ToList();

                    var userInfo = new
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        Accounts = userAccounts
                    };

                    return Ok(userInfo);
                }
                else
                {
                    return StatusCode(500, "Failed to retrieve transactions from the Transaction service.");
                }
            }
            else
            {
                return NotFound(new { Message = "User not found" });
            }
        }

    }
}
