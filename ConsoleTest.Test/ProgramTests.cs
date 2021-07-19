
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleTests;

namespace ConsoleTest.Test
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ChekingSimple_4_TrueReturned()
        {
            int n = 4;
            bool expected = true;

            bool actual = MyProgram.ChekingSimple(n);

            Assert.AreEqual(expected, actual);
        }
        public void ChekingRecurse_4_TrueReturned()
        {
            int n = 4;
            bool expected = true;

            bool actual = MyProgram.ChekingRecurse(n);

            Assert.AreEqual(expected, actual);
        }
        public void ChekingSimple_5_TrueReturned()
        {
            int n = 5;
            bool expected = true;

            bool actual = MyProgram.ChekingSimple(n);

            Assert.AreEqual(expected, actual);
        }
        public void ChekingRecurse_5_TrueReturned()
        {
            int n = 5;
            bool expected = true;

            bool actual = MyProgram.ChekingRecurse(n);

            Assert.AreEqual(expected, actual);
        }

        public void ChekingSimple_2_TrueReturned()
        {
            int n = 2;
            bool expected = true;

            bool actual = MyProgram.ChekingSimple(n);

            Assert.AreEqual(expected, actual);
        }
        public void ChekingRecurse_2_TrueReturned()
        {
            int n = 2;
            bool expected = true;

            bool actual = MyProgram.ChekingRecurse(n);

            Assert.AreEqual(expected, actual);
        }

        public void ChekingSimple_0_TrueReturned()
        {
            int n = 0;
            bool expected = true;

            bool actual = MyProgram.ChekingSimple(n);

            Assert.AreEqual(expected, actual);
        }
        public void ChekingRecurse_0_TrueReturned()
        {
            int n = 0;
            bool expected = true;

            bool actual = MyProgram.ChekingRecurse(n);

            Assert.AreEqual(expected, actual);
        }

        public void ChekingSimple_6_FalseReturned()
        {
            int n = 6;
            bool expected = false;

            bool actual = MyProgram.ChekingSimple(n);

            Assert.AreEqual(expected, actual);
        }
        public void ChekingRecurse_6_FalseReturned()
        {
            int n = 6;
            bool expected = false;

            bool actual = MyProgram.ChekingRecurse(n);

            Assert.AreEqual(expected, actual);
        }
        public void ChekingSimple_3_FalseReturned()
        {
            int n = 3;
            bool expected = false;

            bool actual = MyProgram.ChekingSimple(n);

            Assert.AreEqual(expected, actual);
        }
        public void ChekingRecurse_3_FalseReturned()
        {
            int n = 3;
            bool expected = false;

            bool actual = MyProgram.ChekingRecurse(n);

            Assert.AreEqual(expected, actual);
        }
    }
}
