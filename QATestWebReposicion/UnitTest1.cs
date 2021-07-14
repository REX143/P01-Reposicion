using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace QATestWebReposicion
{
    [TestClass]
    public class UnitTest1
    {
     
        [TestMethod]
        public void TestReporteador()
        {
            string result = WebReposicion.Transversal.GeneradorDataTable.dtreporteGenerado();
            Assert.AreEqual("OK",result);
        }
    }
}
