using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Auth.SignUpCommand;

[DynamoDBTable("Users")]
public record UserWithThisEmailAlreadyExistDTO
{
    public string? Email { get; init; }
}