using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData;

namespace ComponentCouplingMetrics.Test
{
    [TestClass]
    public class FanOutCalculatorTest
    {
        private readonly TestSolution solution;
        private readonly FanOutCalculator fanOut;

        public FanOutCalculatorTest()
        {
            solution = new TestSolution();
            fanOut = new FanOutCalculator();
        }
        
        [TestMethod]
        public void GivenNoOutgoingDependencies_FanOutIsZero()
        {
            AssertFanOut(solution.componentD, 0);
        }

        [TestMethod]
        public void GivenOneClassWithOneOutgoingDependency_FanOutIsOne()
        {
            AssertFanOut(solution.componentB, 1);
        }

        [TestMethod]
        public void GivenTwoClassesWithOutgoingDependencies_FanOutIsTwo()
        {
            AssertFanOut(solution.componentA, 2);
        }

        private void AssertFanOut(Component component, int expectedFanOut)
        {
            var actualFanOut = fanOut.CalculatorFor(component);
            
            Assert.AreEqual(expectedFanOut, actualFanOut);
        }
        
    }
}