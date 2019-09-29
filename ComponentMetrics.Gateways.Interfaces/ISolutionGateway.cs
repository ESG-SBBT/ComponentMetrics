using System;
using ComponentMetrics.Entities;

namespace ComponentMetrics.Gateways.Interfaces
{
    public interface ISolutionGateway
    {
        Solution GetSolution(string path);
    }
    
    public class InvalidSolutionPath : Exception
    {
    }
}