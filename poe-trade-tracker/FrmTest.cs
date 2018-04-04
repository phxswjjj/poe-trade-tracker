using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            var url = "http://poedb.tw/xyz.php?name=%E5%AD%B8%E5%AF%8C%E4%B9%8B%E7%AD%86&league=Warbands&status=1&boi=9&boc_min=1&boc_max=2";

            var xyz = Loader.Xyz.Create(url);
            var html = string.Join(Environment.NewLine, xyz.Raws);

            var parser = new HtmlParser();
            var dom = parser.Parse(html);

            var itemTrs = dom.QuerySelectorAll("table#xyz_items > tbody > tr");
            var itemTr = itemTrs.First();

            var nameSpan = itemTr.QuerySelector("span[class=\"item_unique\"]");
            var name = nameSpan.TextContent;

            var priceSpan = itemTr.QuerySelector("span[data-name=\"price\"]");

            var price = int.Parse(priceSpan.GetAttribute("data-value"));

            var priceUnitImg = priceSpan.QuerySelector("img");
            var priceUnit = priceUnitImg.GetAttribute("title");

            var accountHref = itemTr.QuerySelector("a.xyz_user2");
            var href = accountHref.GetAttribute("href");
            var hrefUri = new Uri(href);
            var account = hrefUri.Segments.Last();

            var s = $"name={name}, price={price}, priceUnit={priceUnit}";
            richTextBox1.Text = account;
        }
    }
}
