using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ComponentMetrics.Entities
{
    public class Solution
    {
        public IEnumerable<Component> Components => ImmutableList.CreateRange(this.components);

        private readonly IList<Component> components = new List<Component>();

        public void Add(Component component)
        {
            if (component == null)
                throw new InvalidComponent();

            if (this.components.Any(p => p.Name == component.Name))
                throw new DuplicateComponent();
            
            this.components.Add(component);
        }

        public IEnumerable<Class> CollectAllClasses()
        {
            return Components
                .SelectMany(p => p.Classes)
                .ToImmutableList();
        }

        public sealed class InvalidComponent : Exception
        {
        }

        public sealed class DuplicateComponent : Exception
        {
        }
    }
}