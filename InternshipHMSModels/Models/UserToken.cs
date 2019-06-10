using System;

namespace InternshipHMSModels.Models
{
    public class UserToken
    {
        public int Id { get; set; }

        public string OwnerUserId { get; set; }

        public string AccessTokenHash { get; set; }

        public DateTime AccessTokenExpirationDateTime { get; set; }

        public string RefreshTokenIdHash { get; set; }

        public string Subject { get; set; }

        public DateTime RefreshTokenExpiresUtc { get; set; }

        public string RefreshToken { get; set; }
    }
}