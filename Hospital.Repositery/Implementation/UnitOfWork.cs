using Hospital.Repositery.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositery.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    { 
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
       _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        { 
            IGenericRepository<T> repository = new GenricRepositery<T>(_context);
            return repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
} 





