using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ComponentMetrics.Backend.DotNet.Entities;

namespace ComponentMetrics.Backend.DotNet
{
    public static class DotnetCli
    {
        public static IEnumerable<Project> ListProjects(string solutionPath)
        {
            var projects = new List<Project>();
                
            RunSolutionCommand(solutionPath, "list", output =>
            {
                var projectNames = output
                    .Split('\n').Skip(2).Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(projectPath => new Project(Path.GetDirectoryName(projectPath), Path.GetDirectoryName(solutionPath), projectPath));
                    
                projects.AddRange(projectNames);
            });

            return projects;
        }

        public static IEnumerable<string> ListProjectDependencies(Project project)
        {
            var dependencies = new List<string>();
            
            RunCommand(startInfo =>
            {
                startInfo.Arguments = "list " + project.Path + " reference";
                startInfo.WorkingDirectory = project.SolutionDirectory;
            }, output =>
            {
                var references = output
                    .Split('\n').Skip(2).Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(reference => reference.Replace(@"\", "/"))
                    .Select(reference => Path.Combine(Path.GetFileName(Path.GetDirectoryName(reference)), Path.GetFileName(reference)));
                
                dependencies.AddRange(references);
            });

            return dependencies;
        }

        private static void RunSolutionCommand(string solutionPath, string command, Action<string> processOutput)
        {
            RunCommand(startInfo =>
            {
                startInfo.Arguments = "sln " + Path.GetFileName(solutionPath) + " " + command;
                startInfo.WorkingDirectory = Path.GetDirectoryName(solutionPath);
            }, processOutput);
        }

        private static void RunCommand(Action<ProcessStartInfo> setup, Action<string> processOutput)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "dotnet";
                setup(process.StartInfo);
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                
                processOutput(process.StandardOutput.ReadToEnd());
                
                process.WaitForExit();
            }
        }
    }
}