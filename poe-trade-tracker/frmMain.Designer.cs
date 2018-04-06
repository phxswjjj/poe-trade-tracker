namespace POE
{
    partial class FrmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GvUrls = new System.Windows.Forms.DataGridView();
            this.TxtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAddUrl = new System.Windows.Forms.Button();
            this.gvColItemPreview = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvColUrl = new System.Windows.Forms.DataGridViewLinkColumn();
            this.gvColItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvColPriceRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvColQueryPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GvUrls)).BeginInit();
            this.SuspendLayout();
            // 
            // GvUrls
            // 
            this.GvUrls.AllowUserToAddRows = false;
            this.GvUrls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GvUrls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvUrls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gvColItemPreview,
            this.gvColUrl,
            this.gvColItems,
            this.GvColPriceRange,
            this.GvColQueryPrice});
            this.GvUrls.Location = new System.Drawing.Point(12, 40);
            this.GvUrls.Name = "GvUrls";
            this.GvUrls.ReadOnly = true;
            this.GvUrls.RowTemplate.Height = 24;
            this.GvUrls.Size = new System.Drawing.Size(763, 369);
            this.GvUrls.TabIndex = 4;
            this.GvUrls.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvUrls_CellClick);
            // 
            // TxtUrl
            // 
            this.TxtUrl.Location = new System.Drawing.Point(86, 12);
            this.TxtUrl.Name = "TxtUrl";
            this.TxtUrl.Size = new System.Drawing.Size(519, 22);
            this.TxtUrl.TabIndex = 0;
            this.TxtUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUrl_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "URL";
            // 
            // BtnAddUrl
            // 
            this.BtnAddUrl.Location = new System.Drawing.Point(611, 10);
            this.BtnAddUrl.Name = "BtnAddUrl";
            this.BtnAddUrl.Size = new System.Drawing.Size(75, 23);
            this.BtnAddUrl.TabIndex = 2;
            this.BtnAddUrl.Text = "Add";
            this.BtnAddUrl.UseVisualStyleBackColor = true;
            this.BtnAddUrl.Click += new System.EventHandler(this.BtnAddUrl_Click);
            // 
            // gvColItemPreview
            // 
            this.gvColItemPreview.DataPropertyName = "ItemName";
            this.gvColItemPreview.Frozen = true;
            this.gvColItemPreview.HeaderText = "Item Name";
            this.gvColItemPreview.Name = "gvColItemPreview";
            this.gvColItemPreview.ReadOnly = true;
            // 
            // gvColUrl
            // 
            this.gvColUrl.DataPropertyName = "Url";
            this.gvColUrl.HeaderText = "Url";
            this.gvColUrl.Name = "gvColUrl";
            this.gvColUrl.ReadOnly = true;
            this.gvColUrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvColUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // gvColItems
            // 
            this.gvColItems.DataPropertyName = "ItemCount";
            this.gvColItems.HeaderText = "Items";
            this.gvColItems.Name = "gvColItems";
            this.gvColItems.ReadOnly = true;
            this.gvColItems.Width = 50;
            // 
            // GvColPriceRange
            // 
            this.GvColPriceRange.DataPropertyName = "PriceRange";
            this.GvColPriceRange.HeaderText = "Sell Price";
            this.GvColPriceRange.Name = "GvColPriceRange";
            this.GvColPriceRange.ReadOnly = true;
            // 
            // GvColQueryPrice
            // 
            this.GvColQueryPrice.DataPropertyName = "QueryPrice";
            this.GvColQueryPrice.HeaderText = "Query Price";
            this.GvColQueryPrice.Name = "GvColQueryPrice";
            this.GvColQueryPrice.ReadOnly = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 420);
            this.Controls.Add(this.GvUrls);
            this.Controls.Add(this.BtnAddUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtUrl);
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Text = "Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GvUrls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TxtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAddUrl;
        private System.Windows.Forms.DataGridView GvUrls;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvColItemPreview;
        private System.Windows.Forms.DataGridViewLinkColumn gvColUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvColItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn GvColPriceRange;
        private System.Windows.Forms.DataGridViewTextBoxColumn GvColQueryPrice;
    }
}

