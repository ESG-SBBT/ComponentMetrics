using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComponentMetrics.Entities;

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

            return component.Classes.Sum(IncomingDependencies);

            ImmutableList<Class> DetermineAllClassesFromOtherComponents()
            {
                return this.allClasses
                    .Except(component.Classes)
                    .ToImmutableList();
            }

            int IncomingDependencies(Class componentClass)
            {
                return allClassesFromOtherComponents.Count(c => c.Dependencies.Contains(componentClass));
            }
        }

        private void CollectAllClasses()
        {
            this.allClasses = this.solution.CollectAllClasses();
        }
    }
}