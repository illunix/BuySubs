namespace BuySubs.API.Enums;

public enum ErrorCode
{
    General = 1,
    NotFound,
    InvalidCredentials,
    InvalidToken,
    ExpiredRefreshToken,
    UserWithThisEmailAlreadyExist
}