using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcces.Entities
{
    public class User : Common.BaseEntity
    {
        [Index(IsUnique = true)]
        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(129)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public ICollection<History> HistoryEntries { get; set; }
    }
}
