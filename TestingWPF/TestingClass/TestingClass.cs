using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestingWPF;

namespace TestingClass
{
    [TestFixture]
    class TestingClass
    {
        [Test]
        public void testFormatName(){
            var test = new MainWindow();
            Assert.AreEqual("PRUEBA", test.obtieneTextoFormateado("prueba"));
        }
        
    }
}
