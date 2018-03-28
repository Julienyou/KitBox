using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KitBoxApp.Tests
{
    [TestFixture()]
    class UtilsTest
    {
        [Test()]
        public void TestGetCornersLength()
        {
            string color = "Blanc";
            int minlength = 90;
            string result = Utils.GetCornersLength(color, minlength);

            Assert.AreEqual("92", result);

        }
    }
}
