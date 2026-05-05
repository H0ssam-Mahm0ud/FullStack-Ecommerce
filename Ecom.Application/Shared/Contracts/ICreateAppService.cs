namespace Ecom.Application.Shared.Contracts;

public interface ICreateAppService<TInput, TOutput> where TInput : class where TOutput : class
{
    Task<TOutput> CreateAsync(TInput input);
}
