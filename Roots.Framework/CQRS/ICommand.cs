using MediatR;

namespace Roots.Framework.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse> where TResponse : BaseResponse
{
    public string? TenantId { get; set; }
}

public interface ICommand : IRequest
{
    public string? TenantId { get; set; }
}