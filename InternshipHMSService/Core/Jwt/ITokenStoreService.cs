using InternshipHMSModels.Models;

namespace InternshipHMSService.Core.Jwt
{
    public interface ITokenStoreService
    {
        void CreateUserToken(UserToken userToken);
        bool IsValidToken(string accessToken, string userId);
        void DeleteExpiredTokens();
        UserToken FindToken(string refreshTokenIdHash);
        void DeleteToken(string refreshTokenIdHash);
        void InvalidateUserTokens(string userId);
        void UpdateUserToken(string userId, string accessTokenHash);
    }
}