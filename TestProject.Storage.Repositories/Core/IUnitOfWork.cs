using System;
using System.Threading.Tasks;

namespace TestProject.Storage.Repositories.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}