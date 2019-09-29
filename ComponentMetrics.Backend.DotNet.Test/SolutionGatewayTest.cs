using System.IO;
using ComponentMetric.Gateways.Test;
using ComponentMetrics.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComponentMetrics.Backend.DotNet.Test
{
    [TestClass]
    public class SolutionGatewayTest : AbstractSolutionGatewayTest
    {
        public SolutionGatewayTest()
            : base(new SolutionGateway())
        {
        }

        protected override Solution GetTestSolution()
        {
            return this.Gateway.GetSolution(Path.Combine("..", "..", "..", "TestSolution", "TestSolution.sln"));
        }
    }
}