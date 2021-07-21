using NUnit.Framework;
using System;

namespace MinWithMax.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void Serch_2X3_1Returned()
        {
            int[][] mas = new int[2][];
            mas[0] = new int[] { 1, -2, 3};
            mas[1] = new int[] { -1, -2 };
            int ecexpected = -1;
            

            Assert.AreEqual(ecexpected, MinWithMax.Serch(mas));
            
            Assert.Pass();
            
        }
        [Test]
        public void Serch_2X3_8Returned()
        {
            int[][] mas = new int[2][];
            mas[0] = new int[] { -1, 12, -3 };
            mas[1] = new int[] { 1, 8 };
            int ecexpected = 8;


            Assert.AreEqual(ecexpected, MinWithMax.Serch(mas));

            Assert.Pass();

        }
        [Test]
        public void Serch_2X3_ExceptionReturned()
        {
            int[][] mas = new int[2][];

            try
            {
                MinWithMax.Serch(mas);
                Assert.Fail();
            }
            catch (Exception e)
            {

            }

            
        }


    }

}