using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Auth;

[DynamoDBTable("Users")]
public record UserSignInDTO
{
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string? Salt { get; init; }
}