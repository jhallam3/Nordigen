using System.Threading.Tasks;
using NUnit.Framework;

namespace NordigenTestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {

            var CheckHTTPCLient = await new Nordigen.Utilities.HttpCall().Call();
            Assert.Pass();
        }
    }
}