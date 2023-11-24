namespace Test_Mandiri.Responses;
public class Response<T>
{
    public bool IsError { get; set; } = false;
    public string? Message { get; set; }
    public T? Data { get; set; }
}