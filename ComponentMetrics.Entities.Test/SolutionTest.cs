using System.Linq;
using ComponentCouplingMetrics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentMetrics.Entities.Test
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        [ExpectedException(typeof(Solution.InvalidComponent))]
        public void GivenNull_AddComponentFails()
        {
            new Solution().Add(null);
        }

        [TestMethod]
        public void AddOneComponent()
        {
            var component = new Component("test component");
            
            var solution = new Solution();
            solution.Add(component);
            
            Assert.AreSame(component, solution.Components.Single());
        }

        [TestMethod]
        [ExpectedException(typeof(Solution.DuplicateComponent))]
        public void GivenComponentWithSameName_AddComponentFails()
        {
            var solution = new Solution();
            solution.Add(new Component("c1"));
            
            solution.Add(new Component("c1"));
        }
    }
}