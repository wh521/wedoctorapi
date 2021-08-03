
namespace WeDectorApi
{
    partial class FrmApi
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmApi));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmShowfrm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdateStockStart = new System.Windows.Forms.Button();
            this.ddlUpdateStockTime = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateStockStop = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOrderStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlOrderTime = new System.Windows.Forms.ComboBox();
            this.btnOrderStart = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOrderExStop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ddlOrderExTime = new System.Windows.Forms.ComboBox();
            this.btnOrderExStart = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDeliveryStop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlDeliveryTime = new System.Windows.Forms.ComboBox();
            this.btnDeliveryStart = new System.Windows.Forms.Button();
            this.notifyMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.notifyMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "微医云-ERP-API";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // notifyMenu
            // 
            this.notifyMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.notifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowfrm,
            this.tsmExit});
            this.notifyMenu.Name = "notifyMenu";
            this.notifyMenu.Size = new System.Drawing.Size(109, 52);
            // 
            // tsmShowfrm
            // 
            this.tsmShowfrm.Name = "tsmShowfrm";
            this.tsmShowfrm.Size = new System.Drawing.Size(108, 24);
            this.tsmShowfrm.Text = "显示";
            this.tsmShowfrm.Click += new System.EventHandler(this.tsmShowfrm_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(108, 24);
            this.tsmExit.Text = "退出";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // btnUpdateStockStart
            // 
            this.btnUpdateStockStart.Location = new System.Drawing.Point(352, 15);
            this.btnUpdateStockStart.Name = "btnUpdateStockStart";
            this.btnUpdateStockStart.Size = new System.Drawing.Size(183, 23);
            this.btnUpdateStockStart.TabIndex = 1;
            this.btnUpdateStockStart.Text = "库存更新同步启动";
            this.btnUpdateStockStart.UseVisualStyleBackColor = true;
            this.btnUpdateStockStart.Click += new System.EventHandler(this.btnUpdateStockStart_Click);
            // 
            // ddlUpdateStockTime
            // 
            this.ddlUpdateStockTime.FormattingEnabled = true;
            this.ddlUpdateStockTime.Location = new System.Drawing.Point(220, 14);
            this.ddlUpdateStockTime.Name = "ddlUpdateStockTime";
            this.ddlUpdateStockTime.Size = new System.Drawing.Size(121, 23);
            this.ddlUpdateStockTime.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUpdateStockStop);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ddlUpdateStockTime);
            this.panel1.Controls.Add(this.btnUpdateStockStart);
            this.panel1.Location = new System.Drawing.Point(-1, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(847, 49);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "微医云库存更新同步间隔分钟";
            // 
            // btnUpdateStockStop
            // 
            this.btnUpdateStockStop.Location = new System.Drawing.Point(548, 15);
            this.btnUpdateStockStop.Name = "btnUpdateStockStop";
            this.btnUpdateStockStop.Size = new System.Drawing.Size(183, 23);
            this.btnUpdateStockStop.TabIndex = 6;
            this.btnUpdateStockStop.Text = "库存更新同步停止";
            this.btnUpdateStockStop.UseVisualStyleBackColor = true;
            this.btnUpdateStockStop.Click += new System.EventHandler(this.btnUpdateStockStop_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOrderStop);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.ddlOrderTime);
            this.panel2.Controls.Add(this.btnOrderStart);
            this.panel2.Location = new System.Drawing.Point(-1, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(847, 49);
            this.panel2.TabIndex = 6;
            // 
            // btnOrderStop
            // 
            this.btnOrderStop.Location = new System.Drawing.Point(548, 15);
            this.btnOrderStop.Name = "btnOrderStop";
            this.btnOrderStop.Size = new System.Drawing.Size(183, 23);
            this.btnOrderStop.TabIndex = 6;
            this.btnOrderStop.Text = "订单同步停止";
            this.btnOrderStop.UseVisualStyleBackColor = true;
            this.btnOrderStop.Click += new System.EventHandler(this.btnOrderStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "微医云获取订单同步间隔分钟";
            // 
            // ddlOrderTime
            // 
            this.ddlOrderTime.FormattingEnabled = true;
            this.ddlOrderTime.Location = new System.Drawing.Point(220, 14);
            this.ddlOrderTime.Name = "ddlOrderTime";
            this.ddlOrderTime.Size = new System.Drawing.Size(121, 23);
            this.ddlOrderTime.TabIndex = 3;
            // 
            // btnOrderStart
            // 
            this.btnOrderStart.Location = new System.Drawing.Point(352, 15);
            this.btnOrderStart.Name = "btnOrderStart";
            this.btnOrderStart.Size = new System.Drawing.Size(183, 23);
            this.btnOrderStart.TabIndex = 1;
            this.btnOrderStart.Text = "订单同步启动";
            this.btnOrderStart.UseVisualStyleBackColor = true;
            this.btnOrderStart.Click += new System.EventHandler(this.btnOrderStart_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnOrderExStop);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.ddlOrderExTime);
            this.panel3.Controls.Add(this.btnOrderExStart);
            this.panel3.Location = new System.Drawing.Point(-1, 154);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(847, 49);
            this.panel3.TabIndex = 7;
            // 
            // btnOrderExStop
            // 
            this.btnOrderExStop.Location = new System.Drawing.Point(548, 15);
            this.btnOrderExStop.Name = "btnOrderExStop";
            this.btnOrderExStop.Size = new System.Drawing.Size(183, 23);
            this.btnOrderExStop.TabIndex = 6;
            this.btnOrderExStop.Text = "订单异常同步停止";
            this.btnOrderExStop.UseVisualStyleBackColor = true;
            this.btnOrderExStop.Click += new System.EventHandler(this.btnOrderExStop_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "微医云异常订单同步间隔分钟";
            // 
            // ddlOrderExTime
            // 
            this.ddlOrderExTime.FormattingEnabled = true;
            this.ddlOrderExTime.Location = new System.Drawing.Point(220, 14);
            this.ddlOrderExTime.Name = "ddlOrderExTime";
            this.ddlOrderExTime.Size = new System.Drawing.Size(121, 23);
            this.ddlOrderExTime.TabIndex = 3;
            // 
            // btnOrderExStart
            // 
            this.btnOrderExStart.Location = new System.Drawing.Point(352, 15);
            this.btnOrderExStart.Name = "btnOrderExStart";
            this.btnOrderExStart.Size = new System.Drawing.Size(183, 23);
            this.btnOrderExStart.TabIndex = 1;
            this.btnOrderExStart.Text = "订单异常同步启动";
            this.btnOrderExStart.UseVisualStyleBackColor = true;
            this.btnOrderExStart.Click += new System.EventHandler(this.btnOrderExStart_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnDeliveryStop);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.ddlDeliveryTime);
            this.panel4.Controls.Add(this.btnDeliveryStart);
            this.panel4.Location = new System.Drawing.Point(0, 233);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(847, 55);
            this.panel4.TabIndex = 8;
            // 
            // btnDeliveryStop
            // 
            this.btnDeliveryStop.Location = new System.Drawing.Point(548, 15);
            this.btnDeliveryStop.Name = "btnDeliveryStop";
            this.btnDeliveryStop.Size = new System.Drawing.Size(183, 23);
            this.btnDeliveryStop.TabIndex = 6;
            this.btnDeliveryStop.Text = "订单发货同步停止";
            this.btnDeliveryStop.UseVisualStyleBackColor = true;
            this.btnDeliveryStop.Click += new System.EventHandler(this.btnDeliveryStop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "微医云订单发货同步间隔分钟";
            // 
            // ddlDeliveryTime
            // 
            this.ddlDeliveryTime.FormattingEnabled = true;
            this.ddlDeliveryTime.Location = new System.Drawing.Point(220, 14);
            this.ddlDeliveryTime.Name = "ddlDeliveryTime";
            this.ddlDeliveryTime.Size = new System.Drawing.Size(121, 23);
            this.ddlDeliveryTime.TabIndex = 3;
            // 
            // btnDeliveryStart
            // 
            this.btnDeliveryStart.Location = new System.Drawing.Point(352, 14);
            this.btnDeliveryStart.Name = "btnDeliveryStart";
            this.btnDeliveryStart.Size = new System.Drawing.Size(183, 23);
            this.btnDeliveryStart.TabIndex = 1;
            this.btnDeliveryStart.Text = "订单发货同步启动";
            this.btnDeliveryStart.UseVisualStyleBackColor = true;
            this.btnDeliveryStart.Click += new System.EventHandler(this.btnDeliveryStart_Click);
            // 
            // FrmApi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 298);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmApi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微医云-ERP-API";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmApi_FormClosing);
            this.Load += new System.EventHandler(this.FrmApi_Load);
            this.SizeChanged += new System.EventHandler(this.FrmApi_SizeChanged);
            this.notifyMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip notifyMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmShowfrm;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.Button btnUpdateStockStart;
        private System.Windows.Forms.ComboBox ddlUpdateStockTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateStockStop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOrderStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlOrderTime;
        private System.Windows.Forms.Button btnOrderStart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOrderExStop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddlOrderExTime;
        private System.Windows.Forms.Button btnOrderExStart;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnDeliveryStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlDeliveryTime;
        private System.Windows.Forms.Button btnDeliveryStart;
    }
}