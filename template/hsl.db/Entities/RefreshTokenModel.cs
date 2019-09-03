using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hsl.db.Entities;

namespace hsl.api.Models
{
    [Table("AspNetRefreshTokens")]
    public class RefreshTokenModel
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string UserId { get; set; }
        public string ClientId { get; set; }

        public virtual User User { get; set; }
    }
}
