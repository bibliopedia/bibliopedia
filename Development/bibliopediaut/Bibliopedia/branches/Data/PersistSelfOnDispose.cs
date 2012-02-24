using System;
using Data.Extensions;
using FluentNHibernate.Data;
using NHibernate;

namespace Data
{
    public abstract class PersistSelfOnDispose : Entity, IDisposable
    {

        private ISession _session;

        protected PersistSelfOnDispose() { }
        protected PersistSelfOnDispose(ISession session) { _session = session; }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        public virtual ISession GetSession()
        {
            return _session;
        }

        public virtual void SetSession(ISession session)
        {
            _session = session;
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (!_disposed)
            {
                if (_session != null) _session.TransactedSave(this);
                _disposed = true;
            }
        }

    }
}