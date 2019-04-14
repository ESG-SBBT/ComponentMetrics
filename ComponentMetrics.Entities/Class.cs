using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComponentCouplingMetrics
{
    public class Class
    {
        public string Name { get; }

        public ImmutableList<Class> Dependencies => ImmutableList.CreateRange(this.dependencies);

        private readonly IList<Class> dependencies = new List<Class>();

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