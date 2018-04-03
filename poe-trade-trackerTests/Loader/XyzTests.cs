using Microsoft.VisualStudio.TestTools.UnitTesting;
using POE.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader.Tests
{
    [TestClass()]
    public class XyzTests
    {
        [TestMethod()]
        public void Create_SiteNotFound()
        {
            Assert.ThrowsException<System.Net.WebException>(() =>
            {
                Xyz.Create("http://poedb1.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=3");
            });
        }
        [TestMethod()]
        public void Create_FormatInvalid()
        {
            var xyz = Xyz.Create("http://poedb.tw/");
            Assert.IsFalse(xyz.IsFoundItems);
        }
        [TestMethod()]
        public void Create_Found()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=3");
            Assert.IsTrue(xyz.IsFoundItems);

            Assert.AreEqual("學富之筆 粗紋法杖", xyz.Items.First().Name);
            Assert.AreEqual("崇高石", xyz.Items.First().PriceUnit);
        }
        [TestMethod()]
        public void Create_NotFound()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=0");
            Assert.IsFalse(xyz.IsFoundItems);
        }
    }
}