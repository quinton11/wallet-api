using walletApi.Models;

namespace walletApi.Services;

public interface IWalletService{
    public Task<bool> UserHasMaxWalletCount(string owner, DBContext context);
    public string GenerateAccountNumberHash(string accountNumber, string phoneNumber);
}