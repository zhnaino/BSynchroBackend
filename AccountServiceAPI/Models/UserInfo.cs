using System.Transactions;

namespace Models
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
