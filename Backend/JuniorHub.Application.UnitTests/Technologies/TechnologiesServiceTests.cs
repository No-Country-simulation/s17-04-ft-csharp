using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using JunioHub.Application.Services;
using JuniorHub.Application.Tests.Mocks;
using JunioHub.Application.DTOs.Technology;
using AutoMapper;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Application.Tests.Technologies;

public class TechnologiesServiceTests
{
    private readonly Mock<ILogger<TechnologyService>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private TechnologyService _service;

    public TechnologiesServiceTests()
    {
        _loggerMock = new Mock<ILogger<TechnologyService>>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task AddTechnology_ShouldAddSuccessfully_WhenValidationSucceeds()
    {
        var validTechnologyAddDto = new TechnologyAddDto { Name = "Valid Technology" };
        var technologyEntity = new Technology { Id = 1, Name = "Valid Technology" };
        var technologyDto = new TechnologiesDto { Id = 1, Name = "Valid Technology" };

        _mapperMock.Setup(m => m.Map<Technology>(validTechnologyAddDto)).Returns(technologyEntity);
        _mapperMock.Setup(m => m.Map<TechnologiesDto>(technologyEntity)).Returns(technologyDto);

        var repositoryMock = RepositoryMock.GetTechnologiesRepositoryMockForAdd(technologyEntity);
        _service = new TechnologyService(repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);

        var result = await _service.AddTechnology(validTechnologyAddDto);

        Assert.True(result.Success);
        Assert.Equal(technologyDto, result.Data);
        Assert.Equal("New technology added successfully.", result.Message);

        repositoryMock.Verify(r => r.AddAsync(technologyEntity), Times.Once);
        repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AddTechnology_ShouldReturnError_WhenAddThrowsException()
    {
        var validTechnologyAddDto = new TechnologyAddDto
        {
            Name = "Valid Technology"
        };
        var exceptionMessage = "An error occurred while adding technology.";

        var repositoryMock = RepositoryMock.GetTechnologiesRepositoryMockForAddWithException(exceptionMessage);
        _service = new TechnologyService(repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);

        var result = await _service.AddTechnology(validTechnologyAddDto);

        Assert.False(result.Success);
        Assert.Contains(exceptionMessage, result.Message);

        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Technology>()), Times.Once);
    }

    [Fact]
    public async Task GetAllTechnologies_ShouldReturnData_WhenSuccessful()
    {
        var repositoryMock = RepositoryMock.GetTechnologiesRepositoryMockWithData();
        _service = new TechnologyService(repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);

        var result = await _service.GetAllTechnologies();

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
        repositoryMock.Verify(repo => repo.GetAllAsyncProjectTo<TechnologiesDto>(), Times.Once);
    }

    [Fact]
    public async Task GetAllTechnologies_ShouldReturnError_WhenExceptionThrown()
    {
        var exceptionMessage = "An error occurred";
        var repositoryMock = RepositoryMock.GetTechnologiesRepositoryMockWithException(exceptionMessage);
        _service = new TechnologyService(repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);

        var result = await _service.GetAllTechnologies();

        Assert.False(result.Success);
        Assert.Equal(exceptionMessage, result.Message);

        _loggerMock.Verify(
        logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(exceptionMessage)),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
        Times.Once);
    }
}