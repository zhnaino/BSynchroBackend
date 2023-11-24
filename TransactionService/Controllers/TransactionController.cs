
using CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace TransactionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add_transaction")]
        public IActionResult AddTransaction( [FromBody]TransactionRequest transaction)
        {
            try
            {
       
                 var Transaction = new Transaction { AccountId = transaction.AccountId,Amount=transaction.Deposit };
                _context.Transactions.Add(Transaction);
                _context.SaveChanges();
                return Ok(new { Message = "Transaction added successfully" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("getAllTransactions")]
        public IActionResult GetAllTransactions()
        {
            try
            {
                var transactions = _context.Transactions.ToList();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
