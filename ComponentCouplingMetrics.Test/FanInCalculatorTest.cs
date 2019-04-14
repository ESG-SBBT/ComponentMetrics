using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentCouplingMetrics.Test
{
    [TestClass]
    public class FanInCalculatorTest
    {
        private readonly Component component1;
        private readonly Class c1;
        
        private readonly Component component2;
        private readonly Class c2;
        
        private readonly Solution solution;
        private FanInCalculator calculator;

        public FanInCalculatorTest()
        {
            component1 = new Component("component1");
            c1 = new Class("c1");
            component1.Add(c1);
            
            component2 = new Component("component2");
            c2 = new Class("c2");
            component2.Add(c2);
            
            solution = new Solution();
            solution.Add(component1);
            solution.Add(component2);
        }
        
        [TestMethod]
        public void NoIncomingDependenciesOnAnyClass()
        {
            CreateCalculator();

            AssertFanIn(component1, 0);
            AssertFanIn(component2, 0);
        }

        [TestMethod]
        public void ExactlyOneIncomingDependency()
        {
            component1.AddDependencyOn(component2);
            c1.AddDependencyOn(c2);

            CreateCalculator();
            
            AssertFanIn(component1, 0);
            AssertFanIn(component2, 1);
        }

        private void CreateCalculator()
        {
            calculator = new FanInCalculator(solution);
        }

        [TestMethod]
        public void Integration()
        {
            var solution = new TestSolution();

            var fanIn = new FanInCalculator(solution).CalculateFor(solution.componentC);
            
            Assert.AreEqual(3, fanIn);
        }

        private void AssertFanIn(Component component, int expectedFanIn)
        {
            var actualFanIn = this.calculator.CalculateFor(component);
            
            Assert.AreEqual(expectedFanIn, actualFanIn);
        }
    }
}