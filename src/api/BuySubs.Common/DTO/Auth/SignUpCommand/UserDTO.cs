using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Auth.SignUpCommand;

[DynamoDBTable("Users")]
public record UserDTO
{
    public required string? Email { get; init; }
    public required string? Password { get; init; }
    public required byte[]? Salt { get; init; }
}