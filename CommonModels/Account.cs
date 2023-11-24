using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommonModels
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public double Balance { get; set; }

        // relationship with user and transaction
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
