using Core.Enities;
using Web.Core.Interfaces;

namespace Core.Interfaces
{
    public interface IUnitofwork : IDisposable
    {
        IGenericRepository<Chat> Chats { get; }
        IGenericRepository<User> Users { get; }
        int Complete();

    }
}
