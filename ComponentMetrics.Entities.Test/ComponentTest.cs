using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentMetrics.Entities.Test
{
    [TestClass]
    public class ComponentTest
    {
        [TestMethod]
        [ExpectedException(typeof(Component.InvalidName))]
        public void ComponentNameCannotBeEmpty()
        {
            var unused = new Component("   ");
        }
        
        [TestMethod]
        public void ComponentName()
        {
            const string componentName = "test component";
            
            var component = new Component(componentName);
            
            Assert.AreEqual(componentName, component.Name);
        }
        
        [TestMethod]
        public void GivenNewComponent_DependenciesShouldBeEmpty()
        {
            Assert.AreEqual(0, new Component("p").Dependencies.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Component.SelfReferencesNotAllowed))]
        public void CannotAddDependencyToSelf()
        {
            var component = new Component("p");
            component.AddDependencyOn(component);
        }

        [TestMethod]
        public void AddDependencyToOtherComponent()
        {
            var component1 = new Component("component1");
            var component2 = new Component("component2");
            
            component1.AddDependencyOn(component2);

            var dependency = component1.Dependencies.Single();
            Assert.AreSame(component2, dependency);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Component.InvalidClass))]
        public void GivenNull_AddClassFails()
        {
            new Component("p").Add(null);
        }
        
        [TestMethod]
        public void AddOneClass()
        {
            var cls = new Class("c1");
            
            var component = new Component("test component");
            component.Add(cls);
            
            Assert.AreSame(cls, component.Classes.Single());
        }
        
        [TestMethod]
        [ExpectedException(typeof(Component.DuplicateClass))]
        public void GivenClassWithSameName_AddClassFails()
        {
            var component = new Component("p");
            component.Add(new Class("c1"));
            
            component.Add(new Class("c1"));
        }
    }
}