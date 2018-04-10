using Quartz;
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
        IScheduler scheduler = null;
        List<string> blacklists = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void AddMonitorList(string url, string itemName)
        {
            var gvUrls = (DataGridView)this.Controls.Find("GvUrls", false).First();
            var list = gvUrls.DataSource as BindingList<Loader.IGridViewDisplay>;
            if (list == null)
                list = new BindingList<Loader.IGridViewDisplay>();

            var xyz = Loader.Xyz.Create(url, itemName);
            if (this.blacklists != null)
                xyz.SetBlacklist(blacklists);

            if (xyz != null && xyz.IsValid)
                list.Add(xyz);

            list = new BindingList<Loader.IGridViewDisplay>(list.OrderBy(item => item.ItemName).ThenBy(item => item.QueryPrice).ToList());

            gvUrls.AutoGenerateColumns = false;
            gvUrls.DataSource = list;
        }
        private void AddMonitorList(string url)
        {
            AddMonitorList(url, "");
        }

        private void PreloadQueryHistory()
        {
            using (var session = Repo.Marcello.CreateSession())
            {
                var queryFile = session["query.dat"];
                var queryCollection = queryFile.Collection<Repo.Query, string>("query", q => q.Url);

                foreach (var query in queryCollection.All)
                    AddMonitorList(query.Url, query.ItemName);
            }
        }
        private void LoadBlacklist()
        {
            using (var session = Repo.Marcello.CreateSession())
            {
                var blacklistFile = session["blacklist.dat"];
                var blacklistCollection = blacklistFile.Collection<Repo.Blacklist, string>("blacklist", b => b.Account);
                blacklists = blacklistCollection.All.Select(b => b.Account).ToList();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();

            var job = JobBuilder.Create<SimpleJob>()
                .WithIdentity("Tracker", "POE")
                .SetJobData(new JobDataMap() { { "GvUrls", (DataGridView)this.Controls.Find("GvUrls", false).First() } })
                .RequestRecovery()
                .Build();

            var jobTrigger = TriggerBuilder.Create()
                .WithCronSchedule("0 0/1 * * * ? *")
                .Build();

            scheduler.ScheduleJob(job, jobTrigger);

            scheduler.Start();

            LoadBlacklist();

            PreloadQueryHistory();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.B)
            {
                var frm = new FrmBlacklist();
                frm.ShowDialog(this);

                LoadBlacklist();
                
                var gvUrls = (DataGridView)this.Controls.Find("GvUrls", false).First();
                switch(gvUrls.DataSource)
                {
                    case BindingList<Loader.IGridViewDisplay> list:
                        foreach (var display in list)
                        {
                            switch (display)
                            {
                                case Loader.ILoader loader:
                                    loader.SetBlacklist(blacklists);
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (scheduler != null)
            {
                scheduler.PauseAll();
                scheduler.Shutdown(true);
            }

            var gvUrls = (DataGridView)this.Controls.Find("GvUrls", false).First();

            var list = gvUrls.DataSource as BindingList<Loader.IGridViewDisplay>;
            if (list == null)
                list = new BindingList<Loader.IGridViewDisplay>();

            using (var session = Repo.Marcello.CreateSession())
            {
                var queryFile = session["query.dat"];
                var queryCollection = queryFile.Collection<Repo.Query, string>("query", q => q.Url);

                var delUrls = from q in queryCollection.All
                              select q.Url;
                session.Transaction(() =>
                {
                    foreach (var url in delUrls.ToList())
                        queryCollection.Destroy(url);

                    foreach (var display in list)
                        queryCollection.Persist(new Repo.Query(display));
                });
            }

            this.Cursor = Cursors.Default;
        }

        private void BtnAddUrl_Click(object sender, EventArgs e)
        {
            var url = TxtUrl.Text.Trim();
            AddMonitorList(url);

            TxtUrl.Text = "";
        }

        private void TxtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                e.Handled = true;
                BtnAddUrl_Click(BtnAddUrl, e);
            }
        }

        private void GvUrls_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            var gvUrls = (DataGridView)sender;
            if (gvUrls.Columns[e.ColumnIndex] is DataGridViewLinkColumn)
            {
                var list = gvUrls.DataSource as BindingList<Loader.IGridViewDisplay>;
                if (list == null || list.Count <= e.RowIndex)
                    return;
                var item = list[e.RowIndex];
                System.Diagnostics.Process.Start(item.Url);
            }
        }

        [DisallowConcurrentExecution]
        private class SimpleJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                var gvUrls = (DataGridView)context.JobDetail.JobDataMap["GvUrls"];
                var list = gvUrls.DataSource as BindingList<Loader.IGridViewDisplay>;
                if (list == null)
                    return;

                foreach (var display in list)
                {
                    switch (display)
                    {
                        case Loader.ILoader loader:
                            loader.Reload();
                            Console.WriteLine($"{display.ItemName} reloaded at {display.Timestamp}");
                            break;
                        default:
                            Console.WriteLine($"{display.ItemName} is not ILoader");
                            break;
                    }
                }

                if (gvUrls.InvokeRequired)
                    gvUrls.Invoke((Action)(() => gvUrls.Refresh()));
            }
        }
    }
}
