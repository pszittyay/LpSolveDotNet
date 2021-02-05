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

            Assert.AreEqual(Tc.Count, 2);
        }

        [TestMethod]
        public void Restar()
        {
            Model m = new Model();
            var a = m.AddNewVariable<string>("", "A");
            var b = m.AddNewVariable<string>("", "b");

            var Tc = a - b;

            Assert.AreEqual(2,Tc.Count );
            
        }
    }
}