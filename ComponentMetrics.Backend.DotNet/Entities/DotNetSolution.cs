using System.Linq;
using ComponentMetrics.Entities;

namespace ComponentMetrics.Backend.DotNet.Entities
{
    public class DotNetSolution : Solution
    {
        public DotNetSolution(string path)
        {
            var projects = DotnetCli.ListProjects(path).ToList();
            
            foreach (var project in projects)
            {
                var dependencies = DotnetCli.ListProjectDependencies(project);
                
                foreach (var dep in dependencies)
                    project.AddDependencyOn(projects.Single(p => p.Path == dep));
                
                Add(project);    
            }
        }
    }
}