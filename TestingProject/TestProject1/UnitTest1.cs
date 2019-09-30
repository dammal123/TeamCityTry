using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] args = new string[0];
            PrzekrojDrogi.PrzekrojConsole.Main(args);
            string pathDirectory = Directory.GetCurrentDirectory();
            for (int i = 1; i < 4; i++)
            {
                string file = pathDirectory + "\\" + DateTime.Now.ToString("d") + "_" + string.Format("testImage{0}.png",i);
                Assert.IsTrue(File.Exists(file));
            }
        }
    }
}
