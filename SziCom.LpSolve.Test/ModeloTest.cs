﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Modelo m = new Modelo();
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
            Modelo m = new Modelo();
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

            m.Run(1000);

            Assert.AreEqual(20.0, x.Result, 0.1);
            Assert.AreEqual(30.0, y.Result, 0.1);
        }

        [TestMethod]
        public void SumExample()
        {
            Modelo m = new Modelo();
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
            Modelo m = new Modelo();

            Pants pant = new Pants() { Name = "Foo Pants" };
            Jackets jacket = new Jackets() { Name = "Bar Jackets" };

            var x = m.AddNewVariable<Pants>(pant, p => p.Name, (p, result) => p.OptimalValue = result);
            var y = m.AddNewVariable<Jackets>(jacket, j => j.Name, (j, result) => j.OptimalValue = result);



            var objetivo = 20000 * x + 15000 * y;

            m.AddRestriction(x >= 0, "R1");
            m.AddRestriction(y >= 0, "R2");

            m.AddRestriction(x + 1.5 * y <= 750, "R3");
            m.AddRestriction(2 * x + 3 * y <= 1500, "R3");
            m.AddRestriction(2 * x + y <= 1000, "R3");
            m.AddObjetiveFuction(objetivo, "Max", LinearOptmizationType.Maximizar);

            m.Run(1000);


            Assert.AreEqual(375, pant.OptimalValue, 0.1);
            Assert.AreEqual(250, jacket.OptimalValue, 0.1);
        }
    }
}