using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SziCom.LpSolve;

namespace UnitTestProject1
{
    [TestClass]
    public class ModeloTest
    {
        [TestMethod]
        public void SimpleExample()
        {
            Model m = new Model();
            var x = m.AddNewVariable<string>("", "X");
            var y = m.AddNewVariable<string>("", "Y");

            var r1 = x + 2.0 * y <= 80;
            var r2 = 3 * x + 2 * y <= 120;

            var r3 = x >= 0;
            var r4 = y >= 0;

            var objetivo = 20000 * x + 15000 * y;

            m.AddRestriction(r1, "R1");
            m.AddRestriction(r2, "R1");
            m.AddRestriction(r3, "R1");
            m.AddRestriction(r4, "R1");

            m.AddObjetiveFuction(objetivo, "Max", LinearOptmizationType.Maximizar);

            m.Run();

            Assert.AreEqual(20.0, x.Result, 0.1);
            Assert.AreEqual(30.0, y.Result, 0.1);
        }

        [TestMethod]
        public void ScalingExample()
        {
            Model m = new Model();
            var x = m.AddNewVariable<string>("", "X");
            var y = m.AddNewVariable<string>("", "Y");

            var r1 = x + 2.0 * y <= 80;
            var r2 = 3 * x + 2 * y <= 120;

            var r3 = x >= 0;
            var r4 = y >= 0;

            var objetivo = 20000 * x + 15000 * y;

            m.AddRestriction(r1, "R1");
            m.AddRestriction(r2, "R1");
            m.AddRestriction(r3, "R1");
            m.AddRestriction(r4, "R1");

            m.AddObjetiveFuction(objetivo, "Max", LinearOptmizationType.Maximizar);

            m.Run();

            Assert.AreEqual(20.0, x.Result, 0.1);
            Assert.AreEqual(30.0, y.Result, 0.1);
        }

        [TestMethod]
        public void SumExample()
        {
            Model m = new Model();
            var x = m.AddNewVariables<string>(new List<string>() { "" }, (t) => "X");
            var y = m.AddNewVariable<string>("Hola", "Y", (t, resultado) => System.Diagnostics.Debug.WriteLine(t + resultado));

            var r1 = m.SumWhere(t => true, x) + 2.0 * y <= 80;
            var r2 = 3 * m.Sum(x) + 2 * y <= 120;

            foreach (var item in x)
            {
                m.AddRestriction(item >= 0, "R1");
            }

            var r4 = y >= 0;

            var objetivo = 20000 * m.Sum(x) + 15000 * y;

            m.AddRestriction(r1, "R1");
            m.AddRestriction(r2, "R1");

            m.AddRestriction(r4, "R1");

            m.AddObjetiveFuction(objetivo, "Max", LinearOptmizationType.Maximizar);

            m.Run();

            foreach (var item in x)
            {
                Assert.AreEqual(20.0, item.Result, 0.1);
            }

            Assert.AreEqual(30.0, y.Result, 0.1);
        }

        [TestMethod]
        public void VariableExample()
        {
            Model m = new Model();

            Pants pant = new Pants() { Name = "Foo Pants" };
            Jackets jacket = new Jackets() { Name = "Bar Jackets" };

            var x = m.AddNewVariable<Pants>(pant, p => p.Name, (p, result) => p.OptimalValue = result, (p, till, from) => { p.Till = till; p.From = from; });
            var y = m.AddNewVariable<Jackets>(jacket, j => j.Name, (j, result) => j.OptimalValue = result);



            var objetive = 50 * x + 40 * y;

            m.AddRestriction(x >= 0, "Positive Pant");
            m.AddRestriction(y >= 0, "Positive Jackets");

            m.AddRestriction(x + 1.5 * y <= 750, "Contton Textile");
            m.AddRestriction(2 * x + y <= 1000, "Polyester");
            m.AddObjetiveFuction(objetive, LinearOptmizationType.Maximizar);
            var r  = m.Run(100);


            Assert.AreEqual(375, pant.OptimalValue, 0.1);
            Assert.AreEqual(250, jacket.OptimalValue, 0.1);

            Assert.AreEqual(28750, r.OptimizationFunctionResult, 0.1);
        }
    }
}