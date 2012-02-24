// Learn more about F# at http://fsharp.net

module DataModelExerciser

// throw an exception that points to the property that broke in the debugger

// foreach type t in the domain
  // foreach object instance in getInstancesForType t
      // tryPersisting instance;

// let getInstancesForType t =
  // foreach property p in getPersistentProperties t
    // foreach object v in getApplicableValues p 
      // let instance = createInstanceWithValue t p v

// let getInstance t =
  // (t)activator.createinstance(t);

// let rec getPersistentProperties t
  // for each property p in t
    // yield p

// let createInstanceWithValue t p v = 
    // let c = getInstance t;
    // c.p <- v

// let tryPersist instance
   // use nhibernate.session.save

// let rec getApplicableValues p
  // <-- active pattern -- >
  // match p with 
  // | p where p is DatabaseIntrinsicType -> domainVisitor.DatabaseIntrinsicTypeValues
  // | p where p is ClassProperty -> domainVisitor.ClassValues p.PropertyType
  // | p where p is ValueTypeProperty -> domainVisitor.Values p.PropertyType

// let rec databaseIntrinsicTypeValues =
  // match p with
  // string, date, byte[], xml, ?

// let rec stringValues = 
  // seq { null; String.Empty } // should be very easy to override

// let rec classValues t = 
    // yield null;
    // yield getInstance t
    // foreach assignable type in the domain (including subclasses, not classType)
      // yieldAll getInstancesForType t
