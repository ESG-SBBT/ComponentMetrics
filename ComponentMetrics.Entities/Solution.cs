using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ComponentCouplingMetrics
{
    public class Solution
    {
        public ImmutableList<Project> Projects => ImmutableList.CreateRange(this.projects);

        private readonly IList<Project> projects = new List<Project>();

        public void Add(Project project)
        {
            if (project == null)
                throw new InvalidProject();

            if (this.projects.Any(p => p.Name == project.Name))
                throw new DuplicateProject();
            
            this.projects.Add(project);
        }

        public sealed class InvalidProject : Exception
        {
        }

        public sealed class DuplicateProject : Exception
        {
        }
    }
}