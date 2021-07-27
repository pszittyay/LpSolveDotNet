using Microsoft.VisualStudio.TestTools.UnitTesting;
using SziCom.LpSolve;

namespace UnitTestProject1
{
    [TestClass]
    public class VariableTest
    {
        [TestMethod]
        public void Sumar()
        {
            Model m = new Model();
            var a = m.AddNewVariable<string>("", "A");
            var b = m.AddNewVariable<string>("", "b");

            Term Tc = a + b;

            Assert.AreEqual(2, Tc.Count);
        }

        [TestMethod]
        public void Restar()
        {
            Model m = new Model();
            var a = m.AddNewVariable<string>("", "A");
            var b = m.AddNewVariable<string>("", "b");

            var Tc = a - b;

            Assert.AreEqual(2, Tc.Count);

        }
        [TestMethod]
        public void Boolean()
        {
            Model m = new Model();
            var a = m.AddNewVariable<string>("", "Bool", true);
            var b = m.AddNewVariable<string>("", "b");

            var Tc = a - b;
            
            Assert.AreEqual(2, Tc.Count);
            Assert.IsTrue(a.Binary);
        }
    }
}