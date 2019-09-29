using System.IO;
using System.Linq;
using ComponentMetrics.Backend.DotNet.Entities;
using ComponentMetrics.Entities;
using ComponentMetrics.Gateways.Interfaces;

namespace ComponentMetrics.Backend.DotNet
{
    public class SolutionGateway : ISolutionGateway
    {
        public Solution GetSolution(string path)
        {
            if (!File.Exists(path))
                throw new InvalidSolutionPath();

            return new DotNetSolution(path);
        }
    }
}