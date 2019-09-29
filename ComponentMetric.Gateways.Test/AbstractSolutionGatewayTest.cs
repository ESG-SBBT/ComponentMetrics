using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using ComponentMetrics.Entities;
using ComponentMetrics.Gateways.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData;

namespace ComponentMetric.Gateways.Test
{
    public abstract class AbstractSolutionGatewayTest
    {
        protected ISolutionGateway Gateway { get; }

        protected AbstractSolutionGatewayTest(ISolutionGateway gateway)
        {
            this.Gateway = gateway;
        }

        protected abstract Solution GetTestSolution();

        [TestMethod]
        [ExpectedException(typeof(InvalidSolutionPath))]
        public void GivenUnknownFile_GetSolutionFails()
        {
            this.Gateway.GetSolution("some unknown file");
        }

        [TestMethod]
        public void GivenValidPath_GetSolutionsReturnsCorrectData()
        {
            var solution = GetTestSolution();

            AssertSolutionIsCorrectlyLoaded(solution);
        }

        private void AssertSolutionIsCorrectlyLoaded(Solution actualSolution)
        {
            Solution expectedSolution = new TestSolution();

            AssertComponents(expectedSolution.Components.ToList(), actualSolution.Components.ToList());
            
            
            
            void AssertComponents(IList<Component> expectedComponents, IList<Component> actualComponents)
            {
                Assert.AreEqual(expectedComponents.Count, actualComponents.Count);

                foreach (var expectedComponent in expectedComponents)
                    AssertComponent(expectedComponent, actualComponents.Single(component => component.Name == expectedComponent.Name));
                
                
                void AssertComponent(Component expectedComponent, Component actualComponent)
                {
                    AssertClasses(expectedComponent.Classes.ToList(), actualComponent.Classes.ToList());
                    AssertDependencies(expectedComponent.Dependencies.ToList(), actualComponent.Dependencies.ToList());
            
                    
                    void AssertClasses(IList<Class> expectedClasses, IList<Class> actualClasses)
                    {
                        Assert.AreEqual(expectedClasses.Count, actualClasses.Count);

                        foreach (var expectedClass in expectedClasses)
                            AssertClass(expectedClass, actualClasses.Single(cls => cls.FullName == expectedClass.FullName));

                        void AssertClass(Class expectedClass, Class actualClass)
                        {
                            Assert.AreEqual(expectedClass.IsAbstract, actualClass.IsAbstract);
                            AssertClassDependencies(expectedClass.Dependencies, actualClass.Dependencies);

                            void AssertClassDependencies(ImmutableList<Class> expectedDependencies, ImmutableList<Class> actualDependencies)
                            {
                                Assert.AreEqual(expectedDependencies.Count, actualDependencies.Count);

                                foreach (var expectedDependency in expectedDependencies)
                                    Assert.IsTrue(actualDependencies.Any(d => d.FullName == expectedDependency.FullName));
                            }
                        }
                    }
                    
                    void AssertDependencies(IList<Component> expectedDependencies, IList<Component> actualDependencies)
                    {
                        Assert.AreEqual(expectedDependencies.Count, actualDependencies.Count);

                        foreach (var expectedDependency in expectedDependencies)
                            Assert.IsTrue(actualDependencies.Any(d => d.Name == expectedDependency.Name));
                    }
                }
            }
        }
    }
}