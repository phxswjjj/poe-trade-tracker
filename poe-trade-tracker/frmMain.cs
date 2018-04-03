using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void BtnAddUrl_Click(object sender, EventArgs e)
        {
            var gvUrls = (DataGridView)this.Controls.Find("GvUrls", false).First();
            var list = gvUrls.DataSource as BindingList<Loader.IGridViewDisplay>;
            if (list == null)
                list = new BindingList<Loader.IGridViewDisplay>();

            var url = txtUrl.Text.Trim();
            var xyz = Loader.Xyz.Create(url);

            if (xyz.IsValid)
                list.Add(xyz);

            gvUrls.AutoGenerateColumns = false;
            gvUrls.DataSource = list;

            txtUrl.Text = "";
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                e.Handled = true;
                BtnAddUrl_Click(BtnAddUrl, e);
            }
        }
    }
}
