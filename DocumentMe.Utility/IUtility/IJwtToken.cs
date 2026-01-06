namespace DocumentMe.Utility.IUtility
{
    public interface IJwtToken
    {
        string GenerateJWT(string userId, string email);
    }
}
