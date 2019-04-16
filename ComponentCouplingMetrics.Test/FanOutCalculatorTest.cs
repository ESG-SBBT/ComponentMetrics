using ComponentMetrics.Entities;
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
            this.solution = new TestSolution();
            this.fanOut = new FanOutCalculator();
        }
        
        [TestMethod]
        public void GivenNoOutgoingDependencies_FanOutIsZero()
        {
            AssertFanOut(this.solution.ComponentD, 0);
        }

        [TestMethod]
        public void GivenOneClassWithOneOutgoingDependency_FanOutIsOne()
        {
            AssertFanOut(this.solution.ComponentB, 1);
        }

        [TestMethod]
        public void GivenTwoClassesWithOutgoingDependencies_FanOutIsTwo()
        {
            AssertFanOut(this.solution.ComponentA, 2);
        }

        private void AssertFanOut(Component component, int expectedFanOut)
        {
            var actualFanOut = this.fanOut.CalculatorFor(component);
            
            Assert.AreEqual(expectedFanOut, actualFanOut);
        }
        
    }
}