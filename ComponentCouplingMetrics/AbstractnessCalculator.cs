using System;
using System.Linq;
using ComponentMetrics.Entities;

namespace ComponentCouplingMetrics
{
    public class AbstractnessCalculator
    {
        public double CalculateFor(Component component)
        {
            if (component == null)
                throw new MissingComponent();
            
            if (!component.Classes.Any())
                throw new EmptyComponent();

            var abstractClasses = (double)component.Classes.Count(c => c.IsAbstract);
            var concreteClasses = component.Classes.Count(c => !c.IsAbstract);

            if (concreteClasses == 0)
                return 1;
            
            return abstractClasses / concreteClasses;
        }
        
        public sealed class MissingComponent : Exception {}
        public sealed class EmptyComponent : Exception {}
    }
}