using System;
using NHibernate;
namespace Data
{
    public interface IAutoDatabase : IDisposable
    {
        ISessionFactory SessionFactory { get; }
    }
}
