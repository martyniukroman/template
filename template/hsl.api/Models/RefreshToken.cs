using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hsl.api.Models
{
    [Table("AspNetRefreshTokens")]
    public class RefreshToken
    {
        [Key] [StringLength(450)] public string Id { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        [Required] [StringLength(450)] public string Token { get; set; }

        [StringLength(450)] public string UserId { get; set; }

        [ForeignKey("UserId")] public User User { get; set; }
    }
}
