using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinoMidi.Tests
{
    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        public void TestRandomGenerator()
        {
            Vector<float> dist = CreateVector.Dense(new float[] { 0.0f, 1.0f });
            int num = Utilities.RandomSelection(dist);
            Assert.AreEqual(1, num);

            dist = CreateVector.Dense(new float[] { 0.0f, 0.2f, 0.8f, 0.0f });
            num = Utilities.RandomSelection(dist);
            CollectionAssert.Contains(new[] { 1, 2 }, num);
        }
    }
}
