using DatabaseDeploy;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.EndToEnd
{
    [SetUpFixture]
    public class StartupService 
    {
        [OneTimeSetUp]
        public void DatabaseDeploy()
        {
           // Program.Main(new[] { @"..\..\..\FileStore\Boutique.Database.dacpac" });
        }
    }
}
