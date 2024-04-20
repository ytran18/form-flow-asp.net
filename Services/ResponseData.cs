namespace demo.Services;

public class ResponseData<T>
{
    public Int32 Status { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }
    
    public ResponseData () {}

    public ResponseData(Int32 status, bool success, T data)
    {
        Status = status;
        Success = success;
        Data = data;
    }
}