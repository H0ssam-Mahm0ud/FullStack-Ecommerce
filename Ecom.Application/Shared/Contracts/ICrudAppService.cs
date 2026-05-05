using Ecom.Domain.Pagination;

namespace Ecom.Application.Shared.Contracts;

/// <summary>
/// Interface for a CRUD (Create, Read, Update, Delete) application service.
/// </summary>
/// <typeparam name="TInput">The input type, which must be a class.</typeparam>
/// <typeparam name="TOutput">The output type, which must be a class.</typeparam>
/// <typeparam name="TId">The identifier type, which must be a class.</typeparam>
public interface ICrudAppService<TInput, TOutput, TId, TPaginationRequest, TOutputList>
    : IGetByIdAppService<TOutput, TId>,
      IGetListPaginatedAppService<TPaginationRequest, TOutputList>,
      ICreateAppService<TInput, TOutput>,
      IUpdateAppService<TInput, TOutput, TId>,
      IDeleteAppService<TOutput, TId>

    where TInput : class
    where TOutput : class
    where TPaginationRequest : PagedResultRequestDto
    where TOutputList : class
{
}

/// <summary>
/// Interface for a CRUD (Create, Read, Update, Delete) application service.
/// </summary>
/// <typeparam name="TCreateDto">The create dto type, which must be a class.</typeparam>
/// <typeparam name="TUpdateDto">The update dto type, which must be a class.</typeparam>
/// <typeparam name="TOutput">The output type, which must be a class.</typeparam>
/// <typeparam name="TId">The identifier type, which must be a class.</typeparam>
//public interface ICrudAppService<TCreateDto, TUpdateDto, TOutput, TId, TPaginationRequest, TOutputList>
//    : IUpdateAppService<TUpdateDto, TOutput, TId>
//    , IDeleteAppService<TOutput, TId>
//    , IGetByIdAppService<TOutput, TId> 
//    , IGetListPaginatedAppService<TPaginationRequest, TOutputList> 
//    , ICreateAppService<TCreateDto, TOutput>
//    , IGetListAppService<TOutputList>

//    where TOutput : class
//    where TUpdateDto : class
//    where TCreateDto : class
//    where TPaginationRequest : PagedResultRequestDto
//    where TOutputList : class
//{

//}
