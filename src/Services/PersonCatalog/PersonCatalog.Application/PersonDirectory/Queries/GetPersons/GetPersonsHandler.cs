﻿using PersonCatalog.Application.Data;
using PersonCatalog.Application.Services;

namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersons;

public class GetPersonsHandler(IPersonReadRepository personReadRepository, ICacheService cacheService)
    : IQueryHandler<GetPersonsQuery, GetPersonsResult>
{
    public async Task<GetPersonsResult> Handle(GetPersonsQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKey.PersonsData;
        var pageIndex = query.PaginateRequest.PageIndex;
        var pageSize = query.PaginateRequest.PageSize;

        // Intentar obtener datos en caché
        var cachedData = await cacheService.GetValueAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedResult = JsonConvert.DeserializeObject<GetPersonsResult>(cachedData);

            if (cachedResult.Persons.PageIndex == pageIndex &&
                cachedResult.Persons.PageSize == pageSize)
            {
                return cachedResult;
            }
        }
        var totalCount = await personReadRepository.CountAsync(cancellationToken);

        var persons = await personReadRepository.GetPagedAsync(pageIndex, pageSize, cancellationToken);

        var result = new GetPersonsResult(
            new PaginateResult<PersonDto>(
                pageIndex,
                pageSize,
                totalCount,
                persons.ToPersonDtoList()
            )
        );

        // Serializa los resultados a JSON y guárdalos en el caché con expiración
        var jsonResult = JsonConvert.SerializeObject(result);
        await cacheService.SetValueAsync(cacheKey, jsonResult, TimeSpan.FromMinutes(10));

        return result;
    }
}
