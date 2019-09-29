using ComponentMetrics.Entities;

namespace ComponentMetrics.Backend.DotNet.Entities
{
    public class Project : Component
    {
        public string SolutionDirectory { get; }
        public string Path { get; }

        public Project(string name, string solutionDirectory, string path) 
            : base(name)
        {
            this.SolutionDirectory = solutionDirectory;
            this.Path = path;
        }
    }
}