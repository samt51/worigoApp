using System;

namespace Worigo.DataAccess.Abstrack.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit(bool state = true);
    }
}
