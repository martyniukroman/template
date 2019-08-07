using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hsl.api.Models
{
    [Table("AspNetRefreshTokens")]
    public class TokenModel
    {
        [Key] [StringLength(450)] public string Id { get; set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }
        public string Token { get; set; }
        public DateTime CratedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public DateTime LastModifiedUtc { get; set; }
        public string UserId { get; set; }
        public string ClientId { get; set; }
    }
}
