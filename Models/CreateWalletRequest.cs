using System.ComponentModel.DataAnnotations;
using walletApi.Utils;

namespace walletApi.Models;

public class CreateWalletRequest{

    [MinLength(3), MaxLength(10)] public string Name{get;set;}

    public AccountType Type{get;set;}

    [MinLength(10)] public string AccountNumber{get;set;}

    public AccountScheme AccountScheme{get;set;}

    [MinLength(10),MaxLength(10)] public string Owner{get;set;}

     public CreateWalletRequest(string name, AccountType type, string accountNumber, AccountScheme accountScheme,string owner){
        Name=name;
        Type=type;
        AccountNumber=accountNumber;
        AccountScheme=accountScheme;
        Owner = owner;
    }
}