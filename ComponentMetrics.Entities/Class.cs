using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComponentMetrics.Entities
{
    public class Class
    {
        public string Name { get; }
        public bool IsAbstract { get; private set; }

        public ImmutableList<Class> Dependencies => ImmutableList.CreateRange(this.dependencies);

        private readonly IList<Class> dependencies = new List<Class>();

        public static Class Abstract(string name)
        {
            return new Class(name)
            {
                IsAbstract = true
            };   
        }
        
        public Class(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidName();

            Name = name;
        }

        public void AddDependencyOn(Class cls)
        {
            if (cls == this)
                throw new SelfReferencesNotAllowed();

            this.dependencies.Add(cls);
        }

        public sealed class InvalidName : Exception
        {
        }

        public sealed class SelfReferencesNotAllowed : Exception
        {
        }
    }
}