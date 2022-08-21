using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Auth;

[DynamoDBTable("Users")]
public record UserSignUpDTO
{
    public required string? Email { get; init; }
    public required string? Password { get; init; }
    public required string? Salt { get; init; }
}