using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommonModels
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public double Amount { get; set; }

        //relationship with account
        [JsonIgnore]
         public virtual Account Account { get; set; }
    }
}
