namespace InternshipHMSService.Core.Jwt
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
    }
}