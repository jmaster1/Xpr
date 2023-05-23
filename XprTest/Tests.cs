using System;
using NUnit.Framework;

namespace XprTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SimpleTest()
        {
            float result = new Xpr.xpr.Xpr("1+1").eval();
            Assert.True(true);
        }
    }
}