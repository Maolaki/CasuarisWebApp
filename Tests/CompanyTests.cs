using Moq;
using UnionService.Application.UseCases;
using UnionService.Domain.Interfaces;
using UnionService.Domain.Entities;
using System.Linq.Expressions;

namespace UnionService.Tests
{
    public class AddCompanyHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly AddCompanyHandler _handler;
        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly Mock<IRepository<Company>> _mockCompanyRepository;

        public AddCompanyHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockCompanyRepository = new Mock<IRepository<Company>>();

            _mockUnitOfWork.Setup(uow => uow.Users).Returns(_mockUserRepository.Object);
            _mockUnitOfWork.Setup(uow => uow.Companies).Returns(_mockCompanyRepository.Object);

            _handler = new AddCompanyHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateCompany_Fail()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync((User)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User with Id 1 does not exist.", exception.Message);
        }

        [Fact]
        public async Task CreateCompany_Access()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            var user = new User { Id = 1, Username = "Test User" };
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Companies.Create(It.IsAny<Company>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Exactly(2));
            Assert.Equal(MediatR.Unit.Value, result);
        }

        [Fact]
        public async Task ChangeCompany_Fail()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync((User)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User with Id 1 does not exist.", exception.Message);
        }

        [Fact]
        public async Task ChangeCompany_Access()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            var user = new User { Id = 1, Username = "Test User" };
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Companies.Create(It.IsAny<Company>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Exactly(2));
            Assert.Equal(MediatR.Unit.Value, result);
        }

        [Fact]
        public async Task RemoveCompany_Fail()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync((User)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User with Id 1 does not exist.", exception.Message);
        }

        [Fact]
        public async Task RemoveCompany_Access()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            var user = new User { Id = 1, Username = "Test User" };
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Companies.Create(It.IsAny<Company>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Exactly(2));
            Assert.Equal(MediatR.Unit.Value, result);
        }

        [Fact]
        public async Task AddCompanyWorker_Fail()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync((User)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User with Id 1 does not exist.", exception.Message);
        }

        [Fact]
        public async Task AddCompanyWorker_Access()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            var user = new User { Id = 1, Username = "Test User" };
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Companies.Create(It.IsAny<Company>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Exactly(2));
            Assert.Equal(MediatR.Unit.Value, result);
        }

        [Fact]
        public async Task RemoveCompanyWorker_Fail()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync((User)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User with Id 1 does not exist.", exception.Message);
        }

        [Fact]
        public async Task RemoveCompanyWorker_Access()
        {
            // Arrange
            var command = new AddCompanyCommand(1, "Test Company", "A description", null);
            var user = new User { Id = 1, Username = "Test User" };
            _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                                   .ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Companies.Create(It.IsAny<Company>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Exactly(2));
            Assert.Equal(MediatR.Unit.Value, result);
        }

    }
}
