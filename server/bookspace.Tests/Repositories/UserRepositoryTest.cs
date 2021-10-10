using bookspace.Api.Data;
using bookspace.Api.Entities;
using bookspace.Api.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bookspace.Tests.Repositories
{
    public class UserRepositoryTest : RepositoryTest
    {
        public UserRepositoryTest()
        {
            Seed();
        }

        private void Seed()
        {
            using (var context = new BookspaceContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var user1 = new User { Id = 1, Email = "test@mail.com", RoleId = null, Name = "test", Password = "12345678" };
                context.Add(user1);

                var role1 = new Role { Id = 1, Name = "Administrator" };
                var role2 = new Role { Id = 2, Name = "Standard" };
                context.Add(role1);
                context.Add(role2);

                context.SaveChanges();
            }
        }

        [Fact]
        public async void GetById_ShouldReturnUser()
        {
            using (var context = new BookspaceContext(ContextOptions))
            {
                // Arrange
                var unitOfWork = new UnitOfWork(context);

                // Act
                var user = await unitOfWork.UserRepository.GetById(1);

                // Assert
                Assert.Equal(1, user.Id);
            }
        }

        [Fact]
        public async void Insert_ShouldAddNewBook()
        {
            using (var context = new BookspaceContext(ContextOptions))
            {
                // Arrange
                var unitOfWork = new UnitOfWork(context);
                var role1 = context.Roles.First();
                var user = new User { Id = 2, Email = "test@mail.com", RoleId = role1.Id, Name = "test", Password = "12345678" };

                // Act
                await unitOfWork.UserRepository.Insert(user);
                await unitOfWork.SaveChangesAsync();
                var userInDb = await unitOfWork.UserRepository.GetById(user.Id);

                // Assert
                Assert.Equal(user.Id, userInDb.Id);
                Assert.Equal(userInDb.Role.Id, role1.Id);
            }
        }

        [Fact]
        public async void GetByEmail_ShouldReturnEmail()
        {
            using (var context = new BookspaceContext(ContextOptions))
            {
                // Arrange
                var unitOfWork = new UnitOfWork(context);
                var user = new User { Id = 2, Email = "email@mail.com", RoleId = null, Name = "test", Password = "12345678" };

                // Act
                await unitOfWork.UserRepository.Insert(user);
                await unitOfWork.SaveChangesAsync();
                var userInDb = await unitOfWork.UserRepository.GetByEmail(user.Email);

                // Assert
                Assert.Equal(userInDb.Email, user.Email);
            }
        }
    }
}