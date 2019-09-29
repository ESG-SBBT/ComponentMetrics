using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ComponentMetrics.Entities
{
    public class Component
    {
        public string Name { get; }

        public ImmutableList<Component> Dependencies => ImmutableList.CreateRange(this.dependencies);
        public IEnumerable<Class> Classes => ImmutableList.CreateRange(this.classes);

        private readonly IList<Component> dependencies = new List<Component>();
        private readonly IList<Class> classes = new List<Class>();

        public Component(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidName();

            Name = name;
        }

        public void Add(Class cls)
        {
            if (cls == null)
                throw new InvalidClass();
            
            if (this.classes.Any(c => c.Name == cls.Name))
                throw new DuplicateClass();
            
            this.classes.Add(cls);
            cls.Namespace = this.Name;
        }

        public void AddDependencyOn(Component component)
        {
            if (component == this)
                throw new SelfReferencesNotAllowed();

            this.dependencies.Add(component);
        }

        public sealed class SelfReferencesNotAllowed : Exception
        {
        }

        public sealed class InvalidName : Exception
        {
        }

        public sealed class DuplicateClass : Exception
        {
        }

        public sealed class InvalidClass : Exception
        {
        }
    }
}