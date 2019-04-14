using System.Linq;
using ComponentCouplingMetrics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentMetrics.Entities.Test
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        [ExpectedException(typeof(Solution.InvalidProject))]
        public void GivenNull_AddProjectFails()
        {
            new Solution().Add(null);
        }

        [TestMethod]
        public void AddOneProject()
        {
            var project = new Project("p1");
            
            var solution = new Solution();
            solution.Add(project);
            
            Assert.AreSame(project, solution.Projects.Single());
        }

        [TestMethod]
        [ExpectedException(typeof(Solution.DuplicateProject))]
        public void GivenProjectWithSameName_AddProjectFails()
        {
            var solution = new Solution();
            solution.Add(new Project("p1"));
            
            solution.Add(new Project("p1"));
        }
    }
}