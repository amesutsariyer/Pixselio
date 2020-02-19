using NUnit.Framework;
using Pixselio.Web.Controllers;
using Reorder.Core.Tests.Base;

namespace Pixselio.Business.Test.Manager
{
    public class PhotoManagerTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
            var context = CreateInMemoryContext();
            //remove all data
            context.SaveChanges();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}