using System;
using System.Diagnostics.Contracts;

namespace Data
{
    [ContractClass(typeof(IIdentifiable))]
    public interface IIdentifiable
    {
        void AssignIdentity();
        object ComputeIdentity();
        string Id { get; }
    }

    [Serializable]
    public abstract class StringEntity : 
        IEquatable<StringEntity>,
        IIdentifiable
    {
        public virtual string Id { get; protected set; }

        public abstract object ComputeIdentity();

        public virtual void AssignIdentity()
        {
            Id = (string)ComputeIdentity();
        }

        /// <summary>
        /// Indicates whether the current <see cref="T:FluentNHibernate.Data.Entity" /> is equal to another <see cref="T:FluentNHibernate.Data.Entity" />.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="obj" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="obj">An Entity to compare with this object.</param>
        public virtual bool Equals(StringEntity obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return obj.Id == Id;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:FluentNHibernate.Data.Entity" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:FluentNHibernate.Data.Entity" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((StringEntity)obj);
        }

        /// <summary>
        /// Serves as a hash function for a Entity. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            var id = Id == null ? ComputeIdentity() : Id;

            Contract.Requires(id != null, "Identiy is null");

            return (id.GetHashCode() * 397) ^ GetType().GetHashCode();
        }

        public static bool operator ==(StringEntity left, StringEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StringEntity left, StringEntity right)
        {
            return !Equals(left, right);
        }
    }
}