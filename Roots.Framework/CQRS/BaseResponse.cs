namespace Roots.Framework.CQRS;

public class BaseResponse
{
    public bool HasError { get; set; }
    public string Message { get; set; } = null;
}