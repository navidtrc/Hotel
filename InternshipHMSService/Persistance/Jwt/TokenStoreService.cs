using InternshipHMSModels.Models;
using InternshipHMSService.Core.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternshipHMSService.Persistance.Jwt
{
    public class TokenStoreService : ITokenStoreService
    {
        //TODO: replace it with `public IDbSet<UserToken> Tokens {set;get;}`
        private static readonly IList<UserToken> _tokens = new List<UserToken>();

        private readonly ISecurityService _securityService;
        public TokenStoreService(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public void CreateUserToken(UserToken userToken)
        {
            InvalidateUserTokens(userToken.OwnerUserId);
            _tokens.Add(userToken);
        }

        public void UpdateUserToken(string userId, string accessTokenHash)
        {
            var token = _tokens.FirstOrDefault(x => x.OwnerUserId == userId);
            if (token != null)
            {
                token.AccessTokenHash = accessTokenHash;
            }
        }

        public void DeleteExpiredTokens()
        {
            var now = DateTime.UtcNow;
            var userTokens = _tokens.Where(x => x.RefreshTokenExpiresUtc < now).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        public void DeleteToken(string refreshTokenIdHash)
        {
            var token = FindToken(refreshTokenIdHash);
            if (token != null)
            {
                _tokens.Remove(token);
            }
        }

        public UserToken FindToken(string refreshTokenIdHash)
        {
            return _tokens.FirstOrDefault(x => x.RefreshTokenIdHash == refreshTokenIdHash);
        }

        public void InvalidateUserTokens(string userId)
        {
            var userTokens = _tokens.Where(x => x.OwnerUserId == userId).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        public bool IsValidToken(string accessToken, string userId)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);
            var userToken = _tokens.FirstOrDefault(x => x.AccessTokenHash == accessTokenHash && x.OwnerUserId == userId);
            return userToken?.AccessTokenExpirationDateTime >= DateTime.UtcNow;
        }

        
    }
}