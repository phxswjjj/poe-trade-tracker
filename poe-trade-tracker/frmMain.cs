﻿using Quartz;
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

        public FrmMain()
        {
            InitializeComponent();
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
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(scheduler != null)
                scheduler.Shutdown(false);
        }

        private void BtnAddUrl_Click(object sender, EventArgs e)
        {
            var gvUrls = (DataGridView)this.Controls.Find("GvUrls", false).First();
            var list = gvUrls.DataSource as BindingList<Loader.IGridViewDisplay>;
            if (list == null)
                list = new BindingList<Loader.IGridViewDisplay>();

            var url = TxtUrl.Text.Trim();
            var xyz = Loader.Xyz.Create(url);

            if (xyz != null && xyz.IsValid)
                list.Add(xyz);

            gvUrls.AutoGenerateColumns = false;
            gvUrls.DataSource = list;

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

                foreach(var display in list)
                {
                    switch(display)
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

                if(gvUrls.InvokeRequired)
                    gvUrls.Invoke((Action)(() => gvUrls.Refresh()));
            }
        }
    }
}
