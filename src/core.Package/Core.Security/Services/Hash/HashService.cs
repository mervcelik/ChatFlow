namespace Core.Security.Services.Hash;

public class HashService : IHashService
{
    public string Hash(string value)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(value);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public bool Verify(string value, string hash)
    {
        var newHash = Hash(value);
        return newHash == hash;
    }
}