namespace ehERP
{
    partial class ehHome
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.selector = new System.Windows.Forms.Panel();
            this.pnlPay = new System.Windows.Forms.Button();
            this.pnlRec = new System.Windows.Forms.Button();
            this.pnlDash = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.acRec1 = new ehERP.acRec();
            this.ehHomePnl1 = new ehERP.ehHomePnl();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(70)))), ((int)(((byte)(78)))));
            this.panel1.Controls.Add(this.selector);
            this.panel1.Controls.Add(this.pnlPay);
            this.panel1.Controls.Add(this.pnlRec);
            this.panel1.Controls.Add(this.pnlDash);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 626);
            this.panel1.TabIndex = 0;
            // 
            // selector
            // 
            this.selector.BackColor = System.Drawing.Color.Tomato;
            this.selector.ForeColor = System.Drawing.SystemColors.ControlText;
            this.selector.Location = new System.Drawing.Point(0, 160);
            this.selector.Name = "selector";
            this.selector.Size = new System.Drawing.Size(5, 37);
            this.selector.TabIndex = 2;
            // 
            // pnlPay
            // 
            this.pnlPay.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPay.FlatAppearance.BorderSize = 0;
            this.pnlPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnlPay.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPay.ForeColor = System.Drawing.Color.White;
            this.pnlPay.Image = global::ehERP.Properties.Resources.Cash_in_Hand_32px;
            this.pnlPay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlPay.Location = new System.Drawing.Point(0, 238);
            this.pnlPay.Name = "pnlPay";
            this.pnlPay.Size = new System.Drawing.Size(199, 39);
            this.pnlPay.TabIndex = 2;
            this.pnlPay.Text = "     A/C Payable";
            this.pnlPay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pnlPay.UseVisualStyleBackColor = true;
            this.pnlPay.Click += new System.EventHandler(this.pnlPay_Click);
            // 
            // pnlRec
            // 
            this.pnlRec.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRec.FlatAppearance.BorderSize = 0;
            this.pnlRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnlRec.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec.ForeColor = System.Drawing.Color.White;
            this.pnlRec.Image = global::ehERP.Properties.Resources.icons8_transaction_filled_32_1_;
            this.pnlRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlRec.Location = new System.Drawing.Point(0, 199);
            this.pnlRec.Name = "pnlRec";
            this.pnlRec.Size = new System.Drawing.Size(199, 39);
            this.pnlRec.TabIndex = 1;
            this.pnlRec.Text = "     Transactions";
            this.pnlRec.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pnlRec.UseVisualStyleBackColor = true;
            this.pnlRec.Click += new System.EventHandler(this.pnlRec_Click);
            // 
            // pnlDash
            // 
            this.pnlDash.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDash.FlatAppearance.BorderSize = 0;
            this.pnlDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnlDash.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDash.ForeColor = System.Drawing.Color.White;
            this.pnlDash.Image = global::ehERP.Properties.Resources._352300_32;
            this.pnlDash.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlDash.Location = new System.Drawing.Point(0, 160);
            this.pnlDash.Name = "pnlDash";
            this.pnlDash.Size = new System.Drawing.Size(199, 39);
            this.pnlDash.TabIndex = 0;
            this.pnlDash.Text = "     Dashboard";
            this.pnlDash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pnlDash.UseVisualStyleBackColor = true;
            this.pnlDash.Click += new System.EventHandler(this.pnlDash_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(199, 160);
            this.panel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Tomato;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(199, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(830, 10);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(199, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(830, 39);
            this.panel4.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::ehERP.Properties.Resources.icons8_Circled_Down_Left_30;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(788, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 39);
            this.button1.TabIndex = 3;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // acRec1
            // 
            this.acRec1.BackColor = System.Drawing.Color.White;
            this.acRec1.Location = new System.Drawing.Point(199, 49);
            this.acRec1.Name = "acRec1";
            this.acRec1.Size = new System.Drawing.Size(1167, 705);
            this.acRec1.TabIndex = 3;
            // 
            // ehHomePnl1
            // 
            this.ehHomePnl1.BackColor = System.Drawing.Color.White;
            this.ehHomePnl1.Location = new System.Drawing.Point(199, 49);
            this.ehHomePnl1.Name = "ehHomePnl1";
            this.ehHomePnl1.Size = new System.Drawing.Size(1167, 705);
            this.ehHomePnl1.TabIndex = 4;
            // 
            // ehHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1029, 626);
            this.Controls.Add(this.ehHomePnl1);
            this.Controls.Add(this.acRec1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ehHome";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel selector;
        private System.Windows.Forms.Button pnlPay;
        private System.Windows.Forms.Button pnlRec;
        private System.Windows.Forms.Button pnlDash;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
        private acRec acRec1;
        private ehHomePnl ehHomePnl1;
    }
}

