using Amazon.DynamoDBv2.DataModel;

namespace BuySubs.Common.DTO.Auth;

[DynamoDBTable("Users")]
public record UserWithThisEmailAlreadyExistDTO
{
    public string? Email { get; init; }
}