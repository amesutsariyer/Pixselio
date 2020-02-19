using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Pixselio.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reorder.Core.Tests.Base
{
    public class TestBase
    {
        

        [OneTimeSetUp]
        protected void OneTimeSetupAsync()
        {
        }
       
        [SetUp]
        public void BaseSetup()
        {
            CreateInMemoryContext();
        }

        protected IdentityDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .Options;
            var context = new IdentityDbContext(options);

            return context;
        }


    }


}
