using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ComponentCouplingMetrics
{
    public class FanInCalculator
    {
        private readonly Solution solution;
        private IEnumerable<Class> allClasses;

        public FanInCalculator(Solution solution)
        {
            this.solution = solution;
            
            CollectAllClasses();
        }

        public int CalculateFor(Component component)
        {
            var allClassesFromOtherComponents = DetermineAllClassesFromOtherComponents();

            var fanIn = 0;
            
            foreach (var componentClass in component.Classes)
                fanIn += CountIncomingDependenciesOn(componentClass);
            
            return fanIn;

            ImmutableList<Class> DetermineAllClassesFromOtherComponents()
            {
                return this.allClasses
                    .Except(component.Classes)
                    .ToImmutableList();
            }

            int CountIncomingDependenciesOn(Class componentClass)
            {
                return allClassesFromOtherComponents.Count(c => c.Dependencies.Contains(componentClass));
            }
        }

        private void CollectAllClasses()
        {
            this.allClasses = this.solution.Components
                .SelectMany(p => p.Classes);
        }
    }
}