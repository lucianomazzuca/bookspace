using bookspace.Api.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookspace.Tests.Repositories
{
    public abstract class RepositoryTest : IDisposable
    {
        private readonly DbConnection _connection;
        protected DbContextOptions<BookspaceContext> ContextOptions { get; }

        public RepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<BookspaceContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;

            //ContextOptions = new DbContextOptionsBuilder<BookspaceContext>()
            //        .UseInMemoryDatabase("TestDatabase")
            //        .Options;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }

        public void Dispose() => _connection.Dispose();
    }
}
