using AutoMapper;
using MeepleHub.Application.Commands.UserGames.CreateUserGame;
using MeepleHub.Application.Common.Exceptions;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Enums;
using Moq;

namespace MeepleHub.UnitTests.Commands.UserGames
{
    public class CreateUserGameCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WhenUserDoesNotExist_ThrowNotFoundException()
        {
            // Arrange

            const int userId = 999;
            const int gameId = 1;

            var userGameRepositoryMock = new Mock<IUserGameRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var gameRepositoryMock = new Mock<IGameRepository>();
            var mapperMock = new Mock<IMapper>();


            userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync((User?)null);

            var handler = new CreateUserGameCommandHandler(
                userGameRepositoryMock.Object,
                mapperMock.Object,
                userRepositoryMock.Object,
                gameRepositoryMock.Object
            );
            var command = new CreateUserGameCommand
            {
                UserId = userId,
                GameId = gameId,
            };


            // Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal("User with identifier '999' was not found.", exception.Message);
            userGameRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<UserGame>()), Times.Never);
            userGameRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_WhenGameDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            const int userId = 1;
            const int gameId = 999;

            var userGameRepositoryMock = new Mock<IUserGameRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var gameRepositoryMock = new Mock<IGameRepository>();
            var mapperock = new Mock<IMapper>();

            userRepositoryMock
                .Setup(repository => repository.GetByIdAsync(userId))
                .ReturnsAsync(new User { Id = userId });

            gameRepositoryMock
                .Setup(repository => repository.GetByIdAsync(gameId))
                .ReturnsAsync((Game?)null);

            var handler = new CreateUserGameCommandHandler(
                userGameRepositoryMock.Object,
                mapperock.Object,
                userRepositoryMock.Object,
                gameRepositoryMock.Object);

            var command = new CreateUserGameCommand
            {
                UserId = userId,
                GameId = gameId
            };

            // Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => handler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal("Game with identifier '999' was not found.", exception.Message);
            userGameRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<UserGame>()), Times.Never);
            userGameRepositoryMock.Verify(repository => repository.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_WhenUserAndGameExist_CreatesUserGame()
        {
            // Arrange
            const int userId = 1;
            const int gameId = 4;

            var userGameRepositoryMock = new Mock<IUserGameRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var gameRepositoryMock = new Mock<IGameRepository>();
            var mapperMock = new Mock<IMapper>();

            userRepositoryMock
                .Setup(repository => repository.GetByIdAsync(userId))
                .ReturnsAsync(new User { Id = userId });

            gameRepositoryMock
                .Setup(repository => repository.GetByIdAsync(gameId))
                .ReturnsAsync(new Game { Id = gameId});

            var expectedDto = new UserGameDto
            {
                UserId = userId,
                GameId = gameId
            };

            mapperMock.Setup(mapper => mapper.Map<UserGameDto>(It.IsAny<UserGame>())).Returns(expectedDto);

            var handler = new CreateUserGameCommandHandler(
                userGameRepositoryMock.Object,
                mapperMock.Object,
                userRepositoryMock.Object,
                gameRepositoryMock.Object);

            var command = new CreateUserGameCommand
            {
                UserId = userId,
                GameId = gameId,
                Condition = 0,
                PersonalRating = 2,
                PurchasePrice = 20.00m,
                Notes = "test création ok",
            };


            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            userGameRepositoryMock.Verify(repository => repository.AddAsync(It.Is<UserGame>(userGame => userGame.UserId == userId && userGame.GameId == gameId)), Times.Once);
            userGameRepositoryMock.Verify(repository => repository.SaveChangesAsync(), Times.Once);
            Assert.Same(expectedDto, response.UserGame);
        }
    }
}
