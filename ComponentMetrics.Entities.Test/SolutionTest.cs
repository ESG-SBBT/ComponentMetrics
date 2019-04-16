using System.Collections.Immutable;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData;

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

        [TestMethod]
        public void CollectAllClasses()
        {
            var solution = new TestSolution();

            var allClasses = solution.CollectAllClasses()
                .Select(c => c.Name)
                .ToImmutableList();
            
            Assert.AreEqual(6, allClasses.Count);
            Assert.IsTrue(allClasses.Contains("q"));
            Assert.IsTrue(allClasses.Contains("r"));
            Assert.IsTrue(allClasses.Contains("s"));
            Assert.IsTrue(allClasses.Contains("t"));
            Assert.IsTrue(allClasses.Contains("u"));
            Assert.IsTrue(allClasses.Contains("v"));
        }
    }
}