namespace walletApi.Models;


public class ResponseHandler {

    public int ResponseCode {get;set;}
    public string Message {get;set;}
    public object? Data {get; set;}

    public ResponseHandler(int responseCode,string message, object? data =null){
        ResponseCode=responseCode;
        Message=message;
        if(data!=null) Data=data;
    }
}