using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentCouplingMetrics.Test
{
    [TestClass]
    public class FanInCalculatorTest
    {
        [TestMethod]
        public void NoIncomingDependenciesOnAnyClass()
        {
            var p1 = new Project("p1");
            p1.Add(new Class("c1"));
            
            var p2 = new Project("p2");
            p2.Add(new Class("c2"));
            
            var solution = new Solution();
            solution.Add(p1);
            solution.Add(p2);

            var calculator = new FanInCalculator(solution);
            
            Assert.AreEqual(0, calculator.CalculateFor(p1));
            Assert.AreEqual(0, calculator.CalculateFor(p2));
        }

        [TestMethod]
        public void ExactlyOneIncomingDependency()
        {
            var p1 = new Project("p1");
            var c1 = new Class("c1");
            p1.Add(c1);
            
            var p2 = new Project("p2");
            var c2 = new Class("c2");
            p2.Add(c2);
            
            p1.AddDependencyOn(p2);
            c1.AddDependencyOn(c2);
            
            var solution = new Solution();
            solution.Add(p1);
            solution.Add(p2);

            var calculator = new FanInCalculator(solution);
            
            Assert.AreEqual(0, calculator.CalculateFor(p1));
            Assert.AreEqual(1, calculator.CalculateFor(p2));
        }
    
        [TestMethod]
        public void Integration()
        {
            var solution = new TestSolution();

            var fanIn = new FanInCalculator(solution).CalculateFor(solution.componentC);
            
            Assert.AreEqual(3, fanIn);
        }
    }
}