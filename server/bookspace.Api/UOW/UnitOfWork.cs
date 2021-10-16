using bookspace.Api.Data;
using bookspace.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.UOW
{
    public class UnitOfWork : IDisposable
    {
        private readonly BookspaceContext context;
        private BookRepository _bookRepository;
        private UserRepository _userRepository;
        private GenreRepository _genreRepository;
        private AuthorRepository _authorRepository;

        public UnitOfWork(BookspaceContext context)
        {
            this.context = context;
        }

        public BookRepository BookRepository
        {
            get
            {
                if (this._bookRepository == null)
                {
                    this._bookRepository = new BookRepository(context);
                }
                return _bookRepository;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(context);
                }
                return _userRepository;
            }
        }

        public GenreRepository GenreRepository
        {
            get
            {
                if (this._genreRepository == null)
                {
                    this._genreRepository = new GenreRepository(context);
                }
                return _genreRepository;
            }
        }

        public AuthorRepository AuthorRepository
        {
            get
            {
                if (this._authorRepository == null)
                {
                    this._authorRepository = new AuthorRepository(context);
                }
                return _authorRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
