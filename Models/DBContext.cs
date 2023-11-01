using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using walletApi.Utils;

namespace walletApi.Models;


public class DBContext: DbContext{


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wallet>().HasIndex(e=>e.AccountNumber).IsUnique();
        modelBuilder.Entity<Wallet>().Property(d=>d.Type).HasConversion(new EnumToStringConverter<AccountType>());
        modelBuilder.Entity<Wallet>().Property(d=>d.AccountScheme).HasConversion(new EnumToStringConverter<AccountScheme>());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Wallet> Wallets {get;set;}

    public DBContext(DbContextOptions options): base(options){}
}