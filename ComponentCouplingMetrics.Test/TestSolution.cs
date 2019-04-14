namespace ComponentCouplingMetrics.Test
{
    // example structure taken from "CleanArchitecture page 122
    public class TestSolution : Solution
    {
        private readonly Component componentA = new Component("Ca");
        private readonly Component componentB = new Component("Cb");
        public readonly Component componentC = new Component("Cc");
        private readonly Component componentD = new Component("Cd");
        
        public TestSolution()
        {
            AddComponentDependencies();
            SetupClassDependencies();
            AddComponentsToSolution();
        }

        private void AddComponentDependencies()
        {
            componentA.AddDependencyOn(componentC);
            componentB.AddDependencyOn(componentC);
            componentC.AddDependencyOn(componentD);
        }

        private void SetupClassDependencies()
        {
            var q = new Class("q");
            var r = new Class("r");
            componentA.Add(q);
            componentA.Add(r);

            var s = new Class("s");
            componentB.Add(s);

            var t = new Class("t");
            var u = new Class("u");
            componentC.Add(t);
            componentC.Add(u);

            var v = new Class("v");
            componentD.Add(v);

            q.AddDependencyOn(t);
            r.AddDependencyOn(u);

            s.AddDependencyOn(u);

            u.AddDependencyOn(v);
        }

        private void AddComponentsToSolution()
        {
            Add(componentA);
            Add(componentB);
            Add(componentC);
            Add(componentD);
        }
    }
}