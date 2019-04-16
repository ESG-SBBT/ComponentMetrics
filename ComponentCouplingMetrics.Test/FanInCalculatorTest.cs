using ComponentMetrics.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData;

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
            this.component1 = new Component("component1");
            this.c1 = new Class("c1");
            this.component1.Add(this.c1);

            this.component2 = new Component("component2");
            this.c2 = new Class("c2");
            this.component2.Add(this.c2);

            this.solution = new Solution();
            this.solution.Add(this.component1);
            this.solution.Add(this.component2);
        }
        
        [TestMethod]
        public void NoIncomingDependenciesOnAnyClass()
        {
            CreateCalculator();

            AssertFanIn(this.component1, 0);
            AssertFanIn(this.component2, 0);
        }

        [TestMethod]
        public void ExactlyOneIncomingDependency()
        {
            this.component1.AddDependencyOn(this.component2);
            this.c1.AddDependencyOn(this.c2);

            CreateCalculator();
            
            AssertFanIn(this.component1, 0);
            AssertFanIn(this.component2, 1);
        }

        private void CreateCalculator()
        {
            this.calculator = new FanInCalculator(this.solution);
        }

        [TestMethod]
        public void Integration()
        {
            var testSolution = new TestSolution();

            var fanIn = new FanInCalculator(testSolution).CalculateFor(testSolution.ComponentC);
            
            Assert.AreEqual(3, fanIn);
        }

        private void AssertFanIn(Component component, int expectedFanIn)
        {
            var actualFanIn = this.calculator.CalculateFor(component);
            
            Assert.AreEqual(expectedFanIn, actualFanIn);
        }
    }
}