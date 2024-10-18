using MediatR;

namespace Roots.Framework.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : BaseResponse
{
    public string? TenantId { get; set; }
}

public interface IQuery : IRequest
{
    public string? TenantId { get; set; }
}