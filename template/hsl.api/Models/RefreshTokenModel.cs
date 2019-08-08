using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hsl.api.Models
{
    [Table("AspNetRefreshTokens")]
    public class RefreshTokenModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public bool Revoked { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}