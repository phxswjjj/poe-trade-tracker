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
        public void Create_UrlEmpty()
        {
            Assert.IsNull(Xyz.Create(""));
        }
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
        public void Create_FoundUniqueItem()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=3");
            Assert.IsTrue(xyz.IsFoundItems);

            Assert.AreEqual("學富之筆 粗紋法杖", xyz.Items.First().Name);
            Assert.AreEqual("崇高石", xyz.Items.First().PriceUnit);
        }
        [TestMethod()]
        public void Create_FoundNormalItem()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%A5%B3%E7%A5%9E%E7%A5%AD%E5%93%81&league=Warbands&status=1&boi=9&boc_min=1&boc_max=1");
            Assert.IsTrue(xyz.IsFoundItems);

            Assert.AreEqual("女神祭品", xyz.CurrentItemName);
        }
        [TestMethod()]
        public void Create_NotFound()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=0");
            Assert.IsFalse(xyz.IsFoundItems);
        }
        [TestMethod()]
        public void Create_NotFoundButHasQueryConditions()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E7%84%A1%E7%9B%A1%E4%B9%8B%E8%A1%A3&league=Warbands&status=1&boi=1&boc_min=1&boc_max=1");
            Assert.IsFalse(xyz.IsFoundItems);
            Assert.AreEqual("無盡之衣", xyz.ItemName);

            var display = xyz as IGridViewDisplay;
            Assert.AreEqual("無盡之衣", display.ItemName);
            Assert.AreEqual("1 ~ 1 卡蘭德的魔鏡", display.QueryPrice);

            var query = xyz.Query;
            Assert.AreEqual("無盡之衣", query.Name);
            Assert.AreEqual(1, query.MaxPrice);
            Assert.AreEqual(Loader.Data.CurrencyType.MirrorOfKalandra, query.Currency);
        }
        [TestMethod()]
        public void SetBlacklist()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=3");
            Assert.IsTrue(xyz.IsFoundItems);
            Assert.IsTrue(xyz.Items.Count > 0);

            var blacklists = xyz.Items.Select(item => item.Account).ToList();
            xyz.SetBlacklist(blacklists);
            Assert.AreEqual(0, xyz.Items.Count);
        }
        [TestMethod()]
        public void Create_NotFoundAndReload()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E7%84%A1%E7%9B%A1%E4%B9%8B%E8%A1%A3&league=Warbands&status=1&boi=1&boc_min=1&boc_max=1");
            Assert.IsFalse(xyz.IsFoundItems);
            Assert.AreEqual("無盡之衣", xyz.ItemName);

            xyz.Reload();

            var display = xyz as IGridViewDisplay;
            Assert.AreEqual("無盡之衣", display.ItemName);
            Assert.AreEqual("1 ~ 1 卡蘭德的魔鏡", display.QueryPrice);

            var query = xyz.Query;
            Assert.AreEqual("無盡之衣", query.Name);
            Assert.AreEqual(1, query.MaxPrice);
            Assert.AreEqual(Loader.Data.CurrencyType.MirrorOfKalandra, query.Currency);
        }
        [TestMethod()]
        public void Reload()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=3");
            Assert.IsTrue(xyz.IsFoundItems);

            xyz.Reload();

            Assert.IsTrue(xyz.IsFoundItems);

            Assert.AreEqual("學富之筆 粗紋法杖", xyz.Items.First().Name);
            Assert.AreEqual("崇高石", xyz.Items.First().PriceUnit);
        }
        [TestMethod()]
        public void ReloadByDiffUrl_NotFound()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=3");
            Assert.IsTrue(xyz.IsFoundItems);

            xyz.Reload("http://poedb.tw/xyz.php?name=%E7%84%A1%E7%9B%A1%E4%B9%8B%E8%A1%A3&league=Warbands&status=1&boi=1&boc_min=1&boc_max=1");
            Assert.IsFalse(xyz.IsFoundItems);
            Assert.AreEqual("無盡之衣", xyz.ItemName);
        }
        [TestMethod()]
        public void Reload_SiteNotFound()
        {
            var xyz = Xyz.Create("http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=3");
            Assert.IsTrue(xyz.IsFoundItems);

            Assert.ThrowsException<System.Net.WebException>(() =>
            {
                xyz.Reload("http://poedb1.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=3");
            });
        }
    }
}