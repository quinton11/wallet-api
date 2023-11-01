using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using walletApi.Models;
using System;

namespace walletApi.Services;


public class WalletService : IWalletService
{
    public WalletService() { }


    public async Task<bool> UserHasMaxWalletCount(string owner,DBContext context)
    {
        var wallets = await context.Wallets.Where(e => e.Owner == owner).ToListAsync();
        if (wallets.Count < 5) return false;
        return true;
    }

    /// <summary>
    /// Generates an account number hash based on the first 6 digits of the account number
    /// and the phone number of the owner
    /// </summary>
    /// <param name="accountNumber"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public string GenerateAccountNumberHash(string accountNumber, string phoneNumber)
    {
        string combinedData = string.Concat(accountNumber.AsSpan(0, 6), phoneNumber);

        string hash = ComputeHash(combinedData);

        // Take a portion of the hash and convert it to a 6-digit number
        int uniqueNumber = GetUniqueNumberFromHash(hash);

        //decimal with fixed width of 6 digits, pad with zeros if necessary
        string uniqueIdentifier = uniqueNumber.ToString("D6"); 

        return uniqueIdentifier;
    }

    /// <summary>
    ///  Computes a hash based on the given input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private string ComputeHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            // looping through bytes and converting to hexadecimal
            for (int i = 0; i < hashBytes.Length; i++)
            {
                var t = hashBytes[i].ToString("x2"); // convert byte to hexadecimal
                builder.Append(t);
            }

            return builder.ToString();
        }
    }

    private int GetUniqueNumberFromHash(string hash)
    {
        // Extract first 8 digits of hash
        int startIndex = 0;
        int length = 6;    
        string portion = hash.Substring(startIndex, length);

        // Convert the hexadecimal portion to an 6 digit integer.
        return int.Parse(portion, System.Globalization.NumberStyles.HexNumber) % 1000000;
    }
}