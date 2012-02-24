using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Data;
using Iesi.Collections.Generic;

namespace Data.FluentsNeeds
{
    public class AutoDetectStrategies : IAutoMappingAlteration
    {
        private readonly Assembly Assembly;
        private readonly Func<Type, bool> DomainFilter;

        private static IList<Type> _domainTargets;

        public AutoDetectStrategies(Assembly assembly, Func<Type, bool> domainFilter)
        {
            Assembly = assembly;
            DomainFilter = domainFilter;

            _domainTargets = EntityPropertyAndCollectionTypes();
        }

        private IList<Type> EntityPropertyAndCollectionTypes()
        {
            ISet<Type> types = new HashedSet<Type>();
            foreach(var type in Assembly.GetTypes())
            {
                if (!DomainFilter(type)) continue;
                foreach(var property in type.GetProperties())
                {

                    var referred = new List<Type>(
                        property.PropertyType.IsGenericType
                            ? property.PropertyType.GetGenericArguments()
                            : new[] {property.PropertyType});
                    
                    referred.Remove(type); // remove self-reference

                    types.AddAll(referred);
                }
            }
            return new List<Type>(types);
        }

        #region IAutoMappingAlteration Members

        public void Alter(AutoPersistenceModel model)
        {
            // if any type has a property of this type
            // that is of a type (or has a collection of a type)
            // that is abstract 
            // is not generic
            // and inherits from entity
            // and has inheritors in the domain
            // then include that type
            // foreach property in type
            foreach (var strategyType in Strategies())
            {
                model.IncludeBase(strategyType);
            }
        }

        #endregion

        private IEnumerable<Type> Strategies()
        {
            foreach (Type type in Assembly.GetTypes())
            {
                if (!type.IsAbstract) continue;
                if (type.IsGenericType) continue;
                if (!type.IsSubclassOf(typeof(Entity))) continue;

                // does a valid member of the domain inherit from this class 
                Type typeClosure = type;
                if (type.
                        Assembly.
                        GetTypes(). // get all types
                        Where(t =>
                              t.IsSubclassOf(typeClosure) && // inherits from type
                              DomainFilter(typeClosure)).Count() == 0) continue;

                //  does another class refer to this type, or have a collection that refers to this type              
                if (_domainTargets.Contains(typeClosure)) yield return typeClosure;
            }
        }


        // resharper's version
                   // return from type in Assembly.GetTypes() 
                   //where type.IsAbstract 
                   //where type.IsSubclassOf(typeof (Entity)) 
                   //let curr = type 
                   //where type.
                   //      Assembly.GetTypes(). // get all types
                   //      Where(t =>
                   //            t.IsSubclassOf(curr) && // inherits from type
                   //            DomainFilter(curr)).Count() != 0 
                   //from otherType in Assembly.
                   //     GetTypes().
                   //     Where(otherType => otherType != curr).
                   //     Where(otherType => 
                   //           curr.GetProperties().
                   //               Any(property => 
                   //                   (property.PropertyType == curr) || 
                   //                   (property.PropertyType.GetGenericArguments().Contains(curr)))) select type;

    }
}