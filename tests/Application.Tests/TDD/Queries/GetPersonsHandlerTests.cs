namespace Application.Tests.TDD.Queries;

[Trait("Query", "GetPersons")]
public class GetPersonsHandlerTests
{

    private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly GetPersonsHandler _handler;

    public GetPersonsHandlerTests()
    {
        _personReadRepositoryMock = new Mock<IPersonReadRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _handler = new GetPersonsHandler(_personReadRepositoryMock.Object, _cacheServiceMock.Object);
    }

    [Fact]
    public async Task Given_CachedDataExists_When_HandleCalled_Should_ReturnCachedData()
    {
        // Arrange
        var query = new GetPersonsQuery(new PaginatedRequest(0, 10));
        var cachedResult = new GetPersonsResult(new PaginateResult<PersonDto>(0, 10, 1, new List<PersonDto>()));
        var cacheKey = CacheKey.PersonsData;

        _cacheServiceMock.Setup(x => x.GetValueAsync(cacheKey))
            .ReturnsAsync(JsonConvert.SerializeObject(cachedResult));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(cachedResult);
    }
}
