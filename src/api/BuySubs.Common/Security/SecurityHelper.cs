﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BuySubs.Common.Security;

public static class SecurityHelper
{
    public static string HashPassword(
        string password,
        byte[] salt
    )
        => Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
        )
    );

    public static byte[] GetRandomBytes(int length = 32)
    {
        using var randomNumberGenerator = new RNGCryptoServiceProvider();
        
        var salt = new byte[length];
        randomNumberGenerator.GetBytes(salt);

        return salt;
    }

    public static bool ValidatePassword(string password, string hash, string salt)
        => HashPassword(password, Convert.FromBase64String(salt)) == hash;
}