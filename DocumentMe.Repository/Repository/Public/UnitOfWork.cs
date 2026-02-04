using DocumentMe.DataAccessLayer.Database;
using DocumentMe.Repository.IRepository.Public;
using Microsoft.EntityFrameworkCore.Storage;

namespace DocumentMe.Repository.Repository.Public
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DocumentMeDBContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(DocumentMeDBContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                return;

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeTransactionAsync();
        }
    }
}
