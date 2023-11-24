using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text.Json.Serialization;

namespace CommonModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


        //relationship with account
        [JsonIgnore]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
