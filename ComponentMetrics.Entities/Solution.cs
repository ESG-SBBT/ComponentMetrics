using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ComponentCouplingMetrics
{
    public class Solution
    {
        public ImmutableList<Component> Components => ImmutableList.CreateRange(this.components);

        private readonly IList<Component> components = new List<Component>();

        public void Add(Component component)
        {
            if (component == null)
                throw new InvalidComponent();

            if (this.components.Any(p => p.Name == component.Name))
                throw new DuplicateComponent();
            
            this.components.Add(component);
        }

        public ImmutableList<Class> CollectAllClasses()
        {
            return this.Components
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