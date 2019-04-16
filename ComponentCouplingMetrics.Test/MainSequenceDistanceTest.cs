using ComponentMetrics.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentCouplingMetrics.Test
{
    [TestClass]
    public class MainSequenceDistanceTest
    {
        private readonly Solution solution = new Solution();

        [TestMethod]
        public void GivenMaximallyStableAndConcreteComponent_ReturnsOne()
        {
            var componentA = new Component("componentA");
            var classA = new Class("a");
            componentA.Add(classA);

            var componentB = new Component("componentB");
            var classB = new Class("b");
            classB.AddDependencyOn(classA);
            componentB.Add(classB);

            this.solution.Add(componentA);
            this.solution.Add(componentB);

            AssertMaximallyStable(componentA);
            AssertMaximallyConcrete(componentA);

            AssertDistanceFromMainSequence(componentA, 1);
        }

        [TestMethod]
        public void GivenMaximallyUnstableAndAbstractComponent_ReturnsOne()
        {
            var componentA = new Component("componentA");
            var classA = new Class("a");
            componentA.Add(classA);

            var componentB = new Component("componentB");
            var classB = Class.Abstract("b");
            classB.AddDependencyOn(classA);
            componentB.Add(classB);

            this.solution.Add(componentA);
            this.solution.Add(componentB);

            AssertMaximallyUnstable(componentB);
            AssertMaximallyAbstract(componentB);

            AssertDistanceFromMainSequence(componentA, 1);
        }

        [TestMethod]
        public void GivenMaximallyStableAndAbstractComponent_ReturnsZero()
        {
            var componentA = new Component("componentA");
            var classA = Class.Abstract("a");
            componentA.Add(classA);

            var componentB = new Component("componentB");
            var classB = new Class("b");
            classB.AddDependencyOn(classA);
            componentB.Add(classB);

            this.solution.Add(componentA);
            this.solution.Add(componentB);

            AssertMaximallyStable(componentA);
            AssertMaximallyAbstract(componentA);

            AssertDistanceFromMainSequence(componentA, 0);
        }

        [TestMethod]
        public void GivenMaximallyUnstableAndConcreteComponent_ReturnsZero()
        {
            var componentA = new Component("componentA");
            var classA = Class.Abstract("a");
            componentA.Add(classA);

            var componentB = new Component("componentB");
            var classB = new Class("b");
            classB.AddDependencyOn(classA);
            componentB.Add(classB);

            this.solution.Add(componentA);
            this.solution.Add(componentB);

            AssertMaximallyUnstable(componentB);
            AssertMaximallyConcrete(componentB);

            AssertDistanceFromMainSequence(componentA, 0);
        }

        private void AssertDistanceFromMainSequence(Component component, double expectedDistance)
        {
            var actualDistance = new MainSequenceDistance(this.solution).CalculateFor(component);

            Assert.AreEqual(expectedDistance, actualDistance);
        }

        private void AssertMaximallyStable(Component component)
        {
            AssertInstability(component, 0);
        }

        private void AssertMaximallyUnstable(Component component)
        {
            AssertInstability(component, 1);
        }

        private void AssertMaximallyConcrete(Component component)
        {
            AssertAbstractness(component, 0);
        }

        private void AssertMaximallyAbstract(Component component)
        {
            AssertAbstractness(component, 1);
        }

        private void AssertInstability(Component component, double instability)
        {
            Assert.AreEqual(instability, new InstabilityCalculator(this.solution).CalculateFor(component));
        }

        private void AssertAbstractness(Component component, double expectedAbstractness)
        {
            Assert.AreEqual(expectedAbstractness, new AbstractnessCalculator().CalculateFor(component));
        }
    }
}