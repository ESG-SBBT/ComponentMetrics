using System;
using ComponentMetrics.Entities;

namespace ComponentCouplingMetrics
{
    public class MainSequenceDistance
    {
        private readonly Solution solution;

        public MainSequenceDistance(Solution solution)
        {
            this.solution = solution;
        }

        public double CalculateFor(Component component)
        {
            var abstractness = new AbstractnessCalculator().CalculateFor(component);
            var instability = new InstabilityCalculator(this.solution).CalculateFor(component);

            return Math.Abs(abstractness + instability - 1);
        }
    }
}