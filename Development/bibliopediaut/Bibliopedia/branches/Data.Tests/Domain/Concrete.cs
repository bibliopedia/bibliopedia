using System;
using FluentNHibernate.Data;

namespace Data.Tests.Domain
{
    public class Concrete : Base
    {
        public virtual string Value { get; set; }
        public virtual void Operation(int parameter)
        {
            if (Value != String.Empty) Value += String.Empty;
        }
    }
}