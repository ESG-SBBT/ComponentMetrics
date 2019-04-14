using System.Linq;

namespace ComponentCouplingMetrics
{
    public class FanOutCalculator
    {
        public int CalculatorFor(Component component)
        {
            return component.Classes
                .Count(cls => cls.Dependencies.Any());
        }
    }
}