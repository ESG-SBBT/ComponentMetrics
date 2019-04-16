using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData;

namespace ComponentCouplingMetrics.Test
{
    [TestClass]
    public class InstabilityCalculatorTest
    {
        [TestMethod]
        public void Instability()
        {
            var solution = new TestSolution();
            var instability = new InstabilityCalculator(solution);

            Assert.AreEqual(0.25, instability.CalculateFor(solution.ComponentC));
        }
    }
}