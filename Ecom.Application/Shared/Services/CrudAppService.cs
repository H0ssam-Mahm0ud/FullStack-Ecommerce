using Ecom.Application.Results;
using Ecom.Application.Shared.Contracts;
using Ecom.Domain.Contracts.Generic;
using Ecom.Domain.Filters;
using Ecom.Domain.Pagination;
using Ecom.Domain.Entities;
using Ecom.Application.Shared.Extensions;

namespace Ecom.Application.Shared.Services;

public abstract class CrudAppService<TEntity, TInput, TDto, TId, TPaginationRequest>
    : ICrudAppService<TInput, Result<TDto>, TId, TPaginationRequest, Result<PagedResultDto<TDto>>>
    where TEntity : class, IEntity<TId>
    where TInput : class
    where TDto : class
    where TPaginationRequest : PagedAndSortedAndSearchResultRequestDto
{
    protected readonly IGenericRepository<TEntity, TId> _baseRepository;

    protected CrudAppService(IGenericRepository<TEntity, TId> baseRepository)
    {
        _baseRepository = baseRepository;
    }


    #region Mapping Methods
    protected abstract TDto ToDto(TEntity entity);
    protected abstract TEntity ToEntity(TInput input);
    protected abstract void UpdateEntity(TInput input, TEntity entity);
    #endregion


    #region CRUD Methods
    public virtual async Task<Result<List<TDto>>> GetAllAsync()
    {
        var entities = await _baseRepository.GetAllAsync();
        var dtos = entities.Select(ToDto).ToList();
        return Result<List<TDto>>.Success(dtos);
    }

    public virtual async Task<Result<PagedResultDto<TDto>>> GetAllPaginatedAsync(
        TPaginationRequest paginationRequest)
    {
        var query = _baseRepository.Table;

        (int totalCount, List<TEntity> entities) = await query
            .SearchBy(paginationRequest.SearchingTerm)
            .OrderBy(paginationRequest.Sorting ?? "Id")
            .ToPagedResultAsync(paginationRequest);

        var dtos = entities.Select(ToDto).ToList();

        return Result<PagedResultDto<TDto>>.Success(new PagedResultDto<TDto>
        {
            TotalCount = totalCount,
            Items = dtos
        });
    }


    public virtual async Task<Result<TDto>> GetByIdAsync(TId id)
    {
        var entity = await _baseRepository.GetByIdAsync(id);
        if (entity == null)
        {
            return Result<TDto>.NotFound($"{typeof(TEntity).Name} with ID {id} not found");
        }
        return Result<TDto>.Success(ToDto(entity));
    }


    public virtual async Task<Result<TDto>> CreateAsync(TInput input)
    {
        var entity = ToEntity(input);
        bool success = await _baseRepository.InsertAsync(entity);
        if (!success)
        {
            return Result<TDto>.Error($"Failed to create {typeof(TEntity).Name}");
        }
        return Result<TDto>.Success(ToDto(entity));
    }

    public virtual async Task<Result<TDto>> UpdateAsync(TInput input, TId id)
    {
        var existing = await _baseRepository.GetByIdAsync(id);
        if (existing == null)
        {
            return Result<TDto>.NotFound($"{typeof(TEntity).Name} with ID {id} not found");
        }

        UpdateEntity(input, existing);
        bool success = await _baseRepository.UpdateAsync(existing);

        if (!success)
        {
            return Result<TDto>.Error($"Failed to update {typeof(TEntity).Name}");
        }
        return Result<TDto>.Success(ToDto(existing));
    }

    public virtual async Task<Result<TDto>> DeleteAsync(TId id)
    {
        var entity = await _baseRepository.GetByIdAsync(id);
        if (entity == null)
        {
            return Result<TDto>.NotFound($"{typeof(TEntity).Name} with ID {id} not found");
        }

        bool success = await _baseRepository.DeleteAsync(entity);
        if (!success)
        {
            return Result<TDto>.Error($"Failed to delete {typeof(TEntity).Name}");
        }
        return Result<TDto>.Success(ToDto(entity));
    }
    #endregion
}
