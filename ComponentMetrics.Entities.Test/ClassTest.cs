using System.Linq;
using ComponentCouplingMetrics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentMetrics.Entities.Test
{
    [TestClass]
    public class ClassTest
    {
        [TestMethod]
        [ExpectedException(typeof(Class.InvalidName))]
        public void ClassNameCannotBeEmpty()
        {
            new Class("   ");
        }
        
        [TestMethod]
        public void ClassName()
        {
            var cls = new Class("class 1");
            
            Assert.AreEqual("class 1", cls.Name);
        }
        
        [TestMethod]
        public void GivenNewClass_DependenciesShouldBeEmpty()
        {
            Assert.AreEqual(0, new Class("c").Dependencies.Count);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Class.SelfReferencesNotAllowed))]
        public void CannotAddDependencyToSelf()
        {
            var cls = new Class("c");
            cls.AddDependencyOn(cls);
        }
        
        [TestMethod]
        public void AddDependencyToOtherClass()
        {
            var cls1 = new Class("c1");
            var cls2 = new Class("c2");
            
            cls1.AddDependencyOn(cls2);

            var dependency = cls1.Dependencies.Single();
            Assert.AreSame(cls2, dependency);
        }
    }
}