using BuildingBlock.Pagination;

namespace Application.Tests.BDD.StepDefinitions;

[Binding]
public class GetPersonsStepDefinitions
{
    private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly GetPersonsHandler _handler;
    private GetPersonsResult _result;

    public GetPersonsStepDefinitions()
    {
        _personReadRepositoryMock = new Mock<IPersonReadRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _handler = new GetPersonsHandler(_personReadRepositoryMock.Object, _cacheServiceMock.Object);
    }

    [Given(@"a cache with the persons data for page (.*) and page size (.*)")]
    public void GivenACacheWithThePersonsDataForPageAndPageSize(int pageIndex, int pageSize)
    {
        var cachedResult = new GetPersonsResult(
            new PaginateResult<PersonDto>(pageIndex, pageSize, 10, new List<PersonDto>())
        );
        var jsonResult = JsonConvert.SerializeObject(cachedResult);
        _cacheServiceMock.Setup(x => x.GetValueAsync(It.IsAny<string>())).ReturnsAsync(jsonResult);
    }

    [Given(@"an empty cache")]
    public void GivenAnEmptyCache()
    {
        _cacheServiceMock.Setup(x => x.GetValueAsync(It.IsAny<string>())).ReturnsAsync((string)null);
    }

    [Given(@"a repository with (.*) persons for page (.*) and page size (.*)")]
    public void GivenARepositoryWithPersonsForPageAndPageSize(int totalCount, int pageIndex, int pageSize)
    {
        _personReadRepositoryMock.Setup(x => x.CountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(totalCount);
        _personReadRepositoryMock.Setup(x => x.GetPagedAsync(pageIndex, pageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Person>());
    }

    [When(@"I request persons for page (.*) and page size (.*)")]
    public async Task WhenIRequestPersonsForPageAndPageSize(int pageIndex, int pageSize)
    {
        var query = new GetPersonsQuery(new PaginatedRequest { PageIndex = pageIndex, PageSize = pageSize });   
        _result = await _handler.Handle(query, CancellationToken.None);
    }

    [Then(@"I should receive persons from the cache")]
    public void ThenIShouldReceivePersonsFromTheCache()
    {
        _result.Should().NotBeNull();
        _result.Persons.Should().NotBeNull();
    }

    [Then(@"I should receive persons from the repository")]
    public void ThenIShouldReceivePersonsFromTheRepository()
    {
        _result.Should().NotBeNull();
        _result.Persons.Should().NotBeNull();
    }
}