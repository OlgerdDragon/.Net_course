using NUnit.Framework;
using AsyncEquation;
using System.Threading;

namespace AsyncEquation.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AsyncCalculate_1_05()
        {
            ProgramEquation.AsyncCalculate(1);
            double ecexpected = 0.5;
            Thread.Sleep(5000);
            double value = ProgramEquation.value;
            Assert.AreEqual(ecexpected, value);
            
        }
        [Test]
        public void AsyncCalculate_1000_05()
        {
            ProgramEquation.AsyncCalculate(1000);
            double ecexpected = 0.5;
            Thread.Sleep(5000);
            double value = ProgramEquation.value;
            Assert.AreEqual(ecexpected, value);

        }
        [Test]
        public void AsyncDivide_10_55()
        {
            int ecexpected = 55;
            int value = ProgramEquation.AsyncDivide(10);
            Assert.AreEqual(ecexpected, value);
        }
        [Test]
        public void AsyncDivisor_10_110()
        {
            int ecexpected = 110;
            int value = ProgramEquation.AsyncDivisor(10);
            Assert.AreEqual(ecexpected, value);
        }
    }
}