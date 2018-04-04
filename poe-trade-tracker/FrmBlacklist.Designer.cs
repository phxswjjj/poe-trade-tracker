namespace POE
{
    partial class FrmBlacklist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TxtAccount = new System.Windows.Forms.TextBox();
            this.BtnAddBlacklist = new System.Windows.Forms.Button();
            this.TxtBlackList = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account";
            // 
            // TxtAccount
            // 
            this.TxtAccount.Location = new System.Drawing.Point(79, 9);
            this.TxtAccount.Name = "TxtAccount";
            this.TxtAccount.Size = new System.Drawing.Size(250, 22);
            this.TxtAccount.TabIndex = 1;
            this.TxtAccount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAccount_KeyPress);
            // 
            // BtnAddBlacklist
            // 
            this.BtnAddBlacklist.Location = new System.Drawing.Point(335, 7);
            this.BtnAddBlacklist.Name = "BtnAddBlacklist";
            this.BtnAddBlacklist.Size = new System.Drawing.Size(75, 23);
            this.BtnAddBlacklist.TabIndex = 2;
            this.BtnAddBlacklist.Text = "Add";
            this.BtnAddBlacklist.UseVisualStyleBackColor = true;
            this.BtnAddBlacklist.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // TxtBlackList
            // 
            this.TxtBlackList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtBlackList.Location = new System.Drawing.Point(79, 38);
            this.TxtBlackList.Multiline = true;
            this.TxtBlackList.Name = "TxtBlackList";
            this.TxtBlackList.Size = new System.Drawing.Size(250, 326);
            this.TxtBlackList.TabIndex = 3;
            // 
            // FrmBlacklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 379);
            this.Controls.Add(this.TxtBlackList);
            this.Controls.Add(this.BtnAddBlacklist);
            this.Controls.Add(this.TxtAccount);
            this.Controls.Add(this.label1);
            this.Name = "FrmBlacklist";
            this.Text = "Blacklist";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBlacklist_FormClosing);
            this.Load += new System.EventHandler(this.FrmBlacklist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtAccount;
        private System.Windows.Forms.Button BtnAddBlacklist;
        private System.Windows.Forms.TextBox TxtBlackList;
    }
}