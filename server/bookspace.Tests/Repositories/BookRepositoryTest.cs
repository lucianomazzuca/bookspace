using bookspace.Api.Data;
using bookspace.Api.Entities;
using bookspace.Api.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bookspace.Tests.Repositories
{
    public class BookRepositoryTest : RepositoryTest
    {
        public BookRepositoryTest()
        {
            Seed();
        }

        private void Seed()
        {
            using (var context = new BookspaceContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var book1 = new Book() { Id = 1, Name = "Mistborn", AuthorId = null};
                context.Add(book1);

                var author1 = new Author() { Id = 1, Name = "Brandon Sanderson" };
                var author2 = new Author() { Id = 2, Name = "J. R. R. Tolkien" };
                context.Add(author1);
                context.Add(author2);

                var genre1 = new Genre() { Id = 1, Name = "Fantasy" };
                var genre2 = new Genre() { Id = 2, Name = "Adventure" };
                var genre3 = new Genre() { Id = 3, Name = "Action" };
                context.Add(genre1);
                context.Add(genre2);
                context.Add(genre3);

                context.SaveChanges();
            }
        }

        [Fact]
        public async void GetById_ShouldReturnBook()
        {
            using (var context = new BookspaceContext(ContextOptions))
            {
                // Arrange
                var repository = new BaseRepository(context);

                // Act
                var book = await repository.GetById(1);

                // Assert
                Assert.Equal(1, book.Id);
            }
        }

        [Fact]
        public async void Insert_ShouldAddNewBook()
        {
            using (var context = new BookspaceContext(ContextOptions))
            {
                // Arrange
                var repository = new BaseRepository(context);
                var genre1 = context.Genres.First();
                var authorId = 1;
                var book = new Book() { Id = 2, Name = "Lord of the Rings", AuthorId = authorId, Genres = new List<Genre>() };
                book.Genres.Add(genre1);

                // Act
                await repository.Insert(book);
                var bookInDb = await repository.GetById(book.Id);

                // Assert
                Assert.Equal(book.Id, bookInDb.Id);
                Assert.Equal(bookInDb.Genres.First().Id, genre1.Id);
                Assert.Equal(bookInDb.Author.Id, authorId);
            }
        }

        [Fact]
        public async void Update_ShouldUpdateExistingBook()
        {
            using (var context = new BookspaceContext(ContextOptions))
            {
                // Arrange
                var repository = new BaseRepository(context);
                var genre1 = context.Genres.First();
                var authorId = 1;
                var book = new Book() { Id = 2, Name = "Lord of the Rings", AuthorId = authorId, Genres = new List<Genre>() };
                book.Genres.Add(genre1);
                await repository.Insert(book);

                // update book attributes
                var genre2 = context.Genres.Find(2);
                var author2 = context.Authors.Find(2);
                book.Genres = new List<Genre>() { genre2 };
                book.AuthorId = author2.Id;
                book.Name = "Updated";

                // Act
                await repository.Update(book);
                var bookInDb = await repository.GetById(book.Id);

                // Assert
                Assert.Equal(book.Name, bookInDb.Name);
                Assert.Equal(bookInDb.Genres.First().Id, genre2.Id);
                Assert.Equal(bookInDb.Author.Id, author2.Id);
            }
        }
    }
}

