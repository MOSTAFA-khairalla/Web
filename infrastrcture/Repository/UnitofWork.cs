using Core.Enities;
using Core.Interfaces;
using infrastrcture.Context;
using Web.Core.Interfaces;

namespace infrastrcture.Repository
{
    public class UnitOfWork : IUnitofwork
    {
        private readonly ApplicationDbContext _context;

      
        public IGenericRepository<Chat> Chats { get; private set; }
        public IGenericRepository<User> Users { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Chats = new GenericRepository<Chat>(_context);
            Users = new GenericRepository<User>(_context);
           
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
