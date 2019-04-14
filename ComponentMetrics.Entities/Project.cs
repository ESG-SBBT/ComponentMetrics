using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ComponentCouplingMetrics
{
    public class Project
    {
        public string Name { get; }

        public ImmutableList<Project> Dependencies => ImmutableList.CreateRange(this.dependencies);
        public ImmutableList<Class> Classes => ImmutableList.CreateRange(this.classes);

        private readonly IList<Project> dependencies = new List<Project>();
        private readonly IList<Class> classes = new List<Class>();

        public Project(string name)
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
        }

        public void AddDependencyOn(Project project)
        {
            if (project == this)
                throw new SelfReferencesNotAllowed();

            this.dependencies.Add(project);
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