using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using walletApi.Models;
using walletApi.Services;

namespace walletApi.Controllers;

[ApiController]
[Route("api/wallet")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;
    private readonly DBContext _context;

    public WalletController(IWalletService walletService, DBContext context)
    {
        _walletService = walletService;
        _context= context;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateWallet([FromBody] CreateWalletRequest wal)
    {
        var maxedCountReach = await _walletService.UserHasMaxWalletCount(wal.Owner, _context);
        if (maxedCountReach) return new ObjectResult(new
        { error = "User has reached the maximum number of wallets allowed - 5" })
        { StatusCode = 429 };

        var hash = _walletService.GenerateAccountNumberHash(wal.AccountNumber, wal.Owner);

        bool exists = await _context.Wallets.AnyAsync(w => w.AccountNumber == hash);
        if (exists) return Conflict(new { error = "A wallet with the specified account number already exists" });

        Wallet w = new(wal.Name, wal.Type, hash, wal.AccountScheme, wal.Owner);
        await _context.Wallets.AddAsync(w);
        await _context.SaveChangesAsync();

        return Ok(new ResponseHandler(responseCode: 200, message: "Wallet Created Successfully", data: w));
    }

    // delete endpoint
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteWallet(int id)
    {

        var wallet = await _context.Wallets.FindAsync(id);
        if (wallet == null) return NotFound(
            new ResponseHandler(responseCode: 404, message: "Failed to Delete wallet- Wallet does not exist"));

        _context.Wallets.Remove(wallet);
        await _context.SaveChangesAsync();
        return Ok(new ResponseHandler(responseCode: 200, message: "Wallet Deleted Successfully", data: wallet));


    }


    [HttpGet]
    public async Task<ActionResult> GetWallets()
    {
        var wallets = await _context.Wallets.ToListAsync();
        return Ok(new ResponseHandler(responseCode: 200, message: "Wallets Fetched Successfully", data: wallets));

    }


    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetWallet(int id)
    {
        var wallet = await _context.Wallets.FindAsync(id);
        if (wallet == null) return NotFound(
            new ResponseHandler(responseCode: 404, message: "Failed to fetch wallet- Wallet does not exist"));

        return Ok(new ResponseHandler(responseCode: 200, message: "Wallet Fetched Successfully", data: wallet));


    }
}