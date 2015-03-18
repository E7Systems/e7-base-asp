using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rsff.BusinessLayer;

namespace BusinessLayer_Tests
{
    [TestFixture]
    public class Tests_ProjectsDummyData
    {
        [Test]
        public void TestProjectsDummyData()
        {
            ProjectsDummyData data = new ProjectsDummyData();
            List<Project> projects = data.GetProjectsDummyDataList();
            Assert.AreEqual(50, projects.Count);

        }
    }
}
