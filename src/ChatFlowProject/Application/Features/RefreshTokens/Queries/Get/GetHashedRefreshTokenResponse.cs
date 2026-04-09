namespace Application.Features.RefreshTokens.Queries.Get;

public class GetHashedRefreshTokenResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public Guid UserId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByToken { get; set; }
}