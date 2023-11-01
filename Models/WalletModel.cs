using Microsoft.EntityFrameworkCore;
using walletApi.Utils;

namespace walletApi.Models;


public class Wallet{
    public int Id{get;set;}
    public string Name{get;set;}

    public AccountType Type{get;set;}

    public string AccountNumber{get;set;}

    public AccountScheme AccountScheme{get;set;}

    public DateTime CreatedAt{get;set;}

    public string Owner{get;set;}

    public Wallet(string name, AccountType type, string accountNumber, AccountScheme accountScheme,string owner){
        Name=name;
        Type=type;
        AccountNumber=accountNumber;
        AccountScheme=accountScheme;
        Owner = owner;
    }
}

