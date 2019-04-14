using System.Linq;
using ComponentCouplingMetrics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentMetrics.Entities.Test
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        [ExpectedException(typeof(Project.InvalidName))]
        public void ProjectNameCannotBeEmpty()
        {
            new Project("   ");
        }
        
        [TestMethod]
        public void ProjectName()
        {
            var project = new Project("project 1");
            
            Assert.AreEqual("project 1", project.Name);
        }
        
        [TestMethod]
        public void GivenNewProject_DependenciesShouldBeEmpty()
        {
            Assert.AreEqual(0, new Project("p").Dependencies.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Project.SelfReferencesNotAllowed))]
        public void CannotAddDependencyToSelf()
        {
            var project = new Project("p");
            project.AddDependencyOn(project);
        }

        [TestMethod]
        public void AddDependencyToOtherProject()
        {
            var project1 = new Project("p1");
            var project2 = new Project("p2");
            
            project1.AddDependencyOn(project2);

            var dependency = project1.Dependencies.Single();
            Assert.AreSame(project2, dependency);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Project.InvalidClass))]
        public void GivenNull_AddClassFails()
        {
            new Project("p").Add(null);
        }
        
        [TestMethod]
        public void AddOneClass()
        {
            var cls = new Class("c1");
            
            var project = new Project("p1");
            project.Add(cls);
            
            Assert.AreSame(cls, project.Classes.Single());
        }
        
        [TestMethod]
        [ExpectedException(typeof(Project.DuplicateClass))]
        public void GivenClassWithSameName_AddClassFails()
        {
            var project = new Project("p");
            project.Add(new Class("c1"));
            
            project.Add(new Class("c1"));
        }
    }
}