using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Event;
using Data;
using NHibernate.Persister.Entity;
using NHibernate.Event.Default;
using NHibernate;

namespace Jstor.FluentsNeeds
{
    public class IdentifiableEventListeners : ISaveOrUpdateEventListener
    {
        private readonly ISaveOrUpdateEventListener Default;

        private static readonly ISaveOrUpdateEventListener DefaultUpdate = new DefaultUpdateEventListener();

        public IdentifiableEventListeners(ISaveOrUpdateEventListener defaultEventHandler)
        {
            if (defaultEventHandler == null) throw new ArgumentException("defaultEventHandler");
            Default = defaultEventHandler;
        }

        public void OnSaveOrUpdate(SaveOrUpdateEvent @event)
        {
            var identifiable = @event.Entity as IIdentifiable;

            if (identifiable == null)
            {
                Default.OnSaveOrUpdate(@event);
                return;
            }

            if (identifiable.Id == null)
            {
                identifiable.AssignIdentity();
            }

            if (Default == null) return;

            if ((Default is DefaultSaveEventListener) || (Default is DefaultSaveOrUpdateEventListener))
                SaveIdentifiable(@event, identifiable);
            else 
                Default.OnSaveOrUpdate(@event);
        }

        public void SaveIdentifiable(SaveOrUpdateEvent @event, IIdentifiable identifiable)
        {
            var other = @event.Session.Get(@event.Entity.GetType(), identifiable.Id);
            if (other == null)
                Default.OnSaveOrUpdate(@event);
            else
            {
                @event.ResultId = identifiable.Id;
            }

        }

    }
}