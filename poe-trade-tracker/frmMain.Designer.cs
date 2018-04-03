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
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAddUrl = new System.Windows.Forms.Button();
            this.gvColItemPreview = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvColUrl = new System.Windows.Forms.DataGridViewLinkColumn();
            this.gvColItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvColItemMinPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvColTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GvUrls)).BeginInit();
            this.SuspendLayout();
            // 
            // GvUrls
            // 
            this.GvUrls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GvUrls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvUrls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gvColItemPreview,
            this.gvColUrl,
            this.gvColItems,
            this.gvColItemMinPrice,
            this.gvColTimestamp});
            this.GvUrls.Location = new System.Drawing.Point(12, 40);
            this.GvUrls.Name = "GvUrls";
            this.GvUrls.ReadOnly = true;
            this.GvUrls.RowTemplate.Height = 24;
            this.GvUrls.Size = new System.Drawing.Size(698, 369);
            this.GvUrls.TabIndex = 4;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(86, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(519, 22);
            this.txtUrl.TabIndex = 0;
            this.txtUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUrl_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Xyz URL";
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
            this.gvColItemPreview.HeaderText = "ItemName";
            this.gvColItemPreview.Name = "gvColItemPreview";
            this.gvColItemPreview.ReadOnly = true;
            this.gvColItemPreview.Width = 150;
            // 
            // gvColUrl
            // 
            this.gvColUrl.DataPropertyName = "Url";
            this.gvColUrl.HeaderText = "Url";
            this.gvColUrl.Name = "gvColUrl";
            this.gvColUrl.ReadOnly = true;
            this.gvColUrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvColUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.gvColUrl.Width = 200;
            // 
            // gvColItems
            // 
            this.gvColItems.DataPropertyName = "ItemCount";
            this.gvColItems.HeaderText = "Items";
            this.gvColItems.Name = "gvColItems";
            this.gvColItems.ReadOnly = true;
            this.gvColItems.Width = 50;
            // 
            // gvColItemMinPrice
            // 
            this.gvColItemMinPrice.DataPropertyName = "MinPrice";
            this.gvColItemMinPrice.HeaderText = "MinPrice";
            this.gvColItemMinPrice.Name = "gvColItemMinPrice";
            this.gvColItemMinPrice.ReadOnly = true;
            // 
            // gvColTimestamp
            // 
            this.gvColTimestamp.DataPropertyName = "Timestamp";
            this.gvColTimestamp.HeaderText = "Timestamp";
            this.gvColTimestamp.Name = "gvColTimestamp";
            this.gvColTimestamp.ReadOnly = true;
            this.gvColTimestamp.Width = 120;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 420);
            this.Controls.Add(this.GvUrls);
            this.Controls.Add(this.BtnAddUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUrl);
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvUrls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAddUrl;
        private System.Windows.Forms.DataGridView GvUrls;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvColItemPreview;
        private System.Windows.Forms.DataGridViewLinkColumn gvColUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvColItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvColItemMinPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvColTimestamp;
    }
}

