namespace ComponentCouplingMetrics
{
    public class InstabilityCalculator
    {
        private readonly FanInCalculator fanInCalculator;
        private readonly FanOutCalculator fanOutCalculator;

        public InstabilityCalculator(Solution solution)
        {
            this.fanInCalculator = new FanInCalculator(solution);
            this.fanOutCalculator = new FanOutCalculator();
        }

        public double CalculateFor(Component component)
        {
            var fanIn = fanInCalculator.CalculateFor(component);
            var fanOut = fanOutCalculator.CalculatorFor(component);
            
            return fanOut / (fanIn + fanOut);
        }
    }
}