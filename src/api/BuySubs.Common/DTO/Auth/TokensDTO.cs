namespace BuySubs.Common.DTO.Auth;

public readonly record struct TokensDTO(
    string AccessToken,
    string RefreshToken
);