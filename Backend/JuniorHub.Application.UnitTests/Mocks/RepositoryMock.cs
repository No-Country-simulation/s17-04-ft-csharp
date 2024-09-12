using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Entities;
using Moq;

namespace JuniorHub.Application.Tests.Mocks;

public static class RepositoryMock
{
    public static Mock<ITechnologyRepository> GetTechnologiesRepositoryMockWithData()
    {
        var repositoryMock = new Mock<ITechnologyRepository>();
        var technologiesDtoList = new List<TechnologiesDto>
        {
            new TechnologiesDto { Id = 1, Name = "C#" },
            new TechnologiesDto { Id = 2, Name = "JavaScript" }
        };

        repositoryMock
            .Setup(repo => repo.GetAllAsyncProjectTo<TechnologiesDto>())
            .ReturnsAsync(technologiesDtoList);

        return repositoryMock;
    }

    public static Mock<ITechnologyRepository> GetTechnologiesRepositoryMockWithException(string exceptionMessage)
    {
        var repositoryMock = new Mock<ITechnologyRepository>();

        repositoryMock
            .Setup(repo => repo.GetAllAsyncProjectTo<TechnologiesDto>())
            .ThrowsAsync(new Exception(exceptionMessage));

        return repositoryMock;
    }

    public static Mock<ITechnologyRepository> GetTechnologiesRepositoryMockForAdd(Technology technologyEntity)
    {
        var repositoryMock = new Mock<ITechnologyRepository>();

        repositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Technology>()))
            .ReturnsAsync(technologyEntity);

        repositoryMock
            .Setup(repo => repo.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        return repositoryMock;
    }

    public static Mock<ITechnologyRepository> GetTechnologiesRepositoryMockForAddWithException(string exceptionMessage)
    {
        var repositoryMock = new Mock<ITechnologyRepository>();

        repositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Technology>()))
            .ThrowsAsync(new Exception(exceptionMessage));

        return repositoryMock;
    }
}