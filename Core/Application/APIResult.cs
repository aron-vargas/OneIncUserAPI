namespace OneIncUserAPI.Core.Application;

public class APIResult
{
    DateTime RequestedOn { get; set; } = DateTime.UtcNow;
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public object? Data { get; set; } = null;
    public int StatusCode { get; set; } = 200;
}
