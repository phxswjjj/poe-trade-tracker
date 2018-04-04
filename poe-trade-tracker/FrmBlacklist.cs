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
    public partial class FrmBlacklist : Form
    {
        public FrmBlacklist()
        {
            InitializeComponent();
        }

        private void LoadBlacklist()
        {
            using (var session = Repo.Marcello.CreateSession())
            {
                var blacklistFile = session["blacklist.dat"];
                var blacklistCollection = blacklistFile.Collection<Repo.Blacklist, string>("blacklist", b => b.Account);
                var blacklists = blacklistCollection.All.Select(b => b.Account).ToList();
                TxtBlackList.Text = string.Join(Environment.NewLine, blacklists);
            }
        }

        private void FrmBlacklist_Load(object sender, EventArgs e)
        {
            LoadBlacklist();
        }

        private void FrmBlacklist_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string[] spliter = { Environment.NewLine };
            var blacklists = TxtBlackList.Text.Split(spliter, System.StringSplitOptions.RemoveEmptyEntries);

            using (var session = Repo.Marcello.CreateSession())
            {
                var blacklistFile = session["blacklist.dat"];
                var blacklistCollection = blacklistFile.Collection<Repo.Blacklist, string>("blacklist", b => b.Account);

                var delBlacklists = from b in blacklistCollection.All
                                    select b.Account;
                session.Transaction(() =>
                {
                    foreach (var blacklist in delBlacklists.ToList())
                        blacklistCollection.Destroy(blacklist);

                    foreach (var blacklist in blacklists)
                        blacklistCollection.Persist(new Repo.Blacklist() { Account = blacklist });
                });
            }

            this.Cursor = Cursors.Default;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var account = TxtAccount.Text.Trim();
            if (string.IsNullOrEmpty(account))
                return;
            if(TxtBlackList.Text.Length > 0)
                TxtBlackList.AppendText(Environment.NewLine);
            TxtBlackList.AppendText(account);
            TxtAccount.Text = "";
        }

        private void TxtAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                e.Handled = true;
                BtnAdd_Click(BtnAddBlacklist, e);
            }
        }
    }
}
