using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ComponentCouplingMetrics
{
    public class FanInCalculator
    {
        private readonly Solution solution;

        public FanInCalculator(Solution solution)
        {
            this.solution = solution;
        }

        public int CalculateFor(Project component)
        {
            var allClassesFromOtherComponents = DetermineAllClassesFromOtherComponents();

            var fanIn = 0;
            
            foreach (var componentClass in component.Classes)
                fanIn += CountIncomingDependenciesOn(componentClass);
            
            return fanIn;

            ImmutableList<Class> DetermineAllClassesFromOtherComponents()
            {
                return this.solution.Projects
                    .SelectMany(p => p.Classes)
                    .Except(component.Classes)
                    .ToImmutableList();
            }

            int CountIncomingDependenciesOn(Class componentClass)
            {
                return allClassesFromOtherComponents.Count(c => c.Dependencies.Contains(componentClass));
            }
        }
    }
}