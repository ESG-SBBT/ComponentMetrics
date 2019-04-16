using ComponentMetrics.Entities;

namespace TestData
{
    // example structure taken from "CleanArchitecture page 122
    public class TestSolution : Solution
    {
        public readonly Component ComponentA = new Component("Ca");
        public readonly Component ComponentB = new Component("Cb");
        public readonly Component ComponentC = new Component("Cc");
        public readonly Component ComponentD = new Component("Cd");
        
        public TestSolution()
        {
            AddComponentDependencies();
            SetupClassDependencies();
            AddComponentsToSolution();
        }

        private void AddComponentDependencies()
        {
            this.ComponentA.AddDependencyOn(this.ComponentC);
            this.ComponentB.AddDependencyOn(this.ComponentC);
            this.ComponentC.AddDependencyOn(this.ComponentD);
        }

        private void SetupClassDependencies()
        {
            var q = new Class("q");
            var r = new Class("r");
            this.ComponentA.Add(q);
            this.ComponentA.Add(r);

            var s = new Class("s");
            this.ComponentB.Add(s);

            var t = new Class("t");
            var u = new Class("u");
            this.ComponentC.Add(t);
            this.ComponentC.Add(u);

            var v = new Class("v");
            this.ComponentD.Add(v);

            q.AddDependencyOn(t);
            r.AddDependencyOn(u);

            s.AddDependencyOn(u);

            u.AddDependencyOn(v);
        }

        private void AddComponentsToSolution()
        {
            Add(this.ComponentA);
            Add(this.ComponentB);
            Add(this.ComponentC);
            Add(this.ComponentD);
        }
    }
}