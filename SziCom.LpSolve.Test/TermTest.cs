﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SziCom.LpSolve;

namespace UnitTestProject1
{
    [TestClass]
    public class TermTest
    {
        [TestMethod]
        public void Sumar()
        {
            Modelo m = new Modelo();
            var a = m.AddNewVariable<string>("", "A");
            var b = m.AddNewVariable<string>("", "b");

            Term Ta = new Term(a);
            Term Tb = new Term(b);

            Term Tc = Ta + Tb;

            Assert.AreEqual(Tc.Count, 2);
        }

        [TestMethod]
        public void Restar()
        {
            Modelo m = new Modelo();
            var a = m.AddNewVariable<string>("", "A");
            var b = m.AddNewVariable<string>("", "b");

            Term Ta = new Term(a);
            Term Tb = new Term(b);

            var Tc = Ta - Tb;

            Assert.AreEqual(2, Tc.Count);
            Assert.AreEqual(Tc[b], -1);
        }

        [TestMethod]
        public void Multiplicar()
        {
            Modelo m = new Modelo();
            var a = m.AddNewVariable<string>("", "A");

            Term Ta = new Term(a);

            var Tc = Ta * 5;

            Assert.AreEqual(Tc.Count, 1);
            Assert.AreEqual(Tc[a], 5);
        }

        [TestMethod]
        public void Dividir()
        {
            Modelo m = new Modelo();
            var a = m.AddNewVariable<string>("", "A");

            Term Ta = new Term(a);

            var Tc = Ta / 5;

            Assert.AreEqual(1, Tc.Count);
            Assert.AreEqual(0.2, Tc[a]);
        }
    }
}