using MJ.Application;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeDectorApi.Job;

namespace WeDectorApi
{
    public partial class FrmApi : Form
    {
        //WeDoctorRequestApp _wedoctorApp = new WeDoctorRequestApp();
        SimpleTriggerRunner _simpleTriggger = new SimpleTriggerRunner();

        public FrmApi()
        {
            InitializeComponent();
        }

        #region 窗体基本操作

        //窗体加载
        private void FrmApi_Load(object sender, EventArgs e)
        {
            DataTable dtTime = new DataTable();
            dtTime.Columns.Add("time");

            for (int i = 1; i <= 120; i++)
            {
                DataRow row = dtTime.NewRow();
                row["time"] = i;
                dtTime.Rows.Add(row);
            }
            ddlUpdateStockTime.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlUpdateStockTime.DisplayMember = "time";
            ddlUpdateStockTime.ValueMember = "time";
            ddlUpdateStockTime.DataSource = dtTime;
            ddlUpdateStockTime.SelectedValue = 1;

            ddlOrderTime.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlOrderTime.DisplayMember = "time";
            ddlOrderTime.ValueMember = "time";
            ddlOrderTime.DataSource = dtTime.Copy();
            ddlOrderTime.SelectedValue = 1;

            ddlOrderExTime.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlOrderExTime.DisplayMember = "time";
            ddlOrderExTime.ValueMember = "time";
            ddlOrderExTime.DataSource = dtTime.Copy();
            ddlOrderExTime.SelectedValue = 1;

            ddlDeliveryTime.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlDeliveryTime.DisplayMember = "time";
            ddlDeliveryTime.ValueMember = "time";
            ddlDeliveryTime.DataSource = dtTime.Copy();
            ddlDeliveryTime.SelectedValue = 1;
        }

        //窗体最小化到托盘
        private void FrmApi_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState==FormWindowState.Minimized)
            {
                //隐藏任务栏图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                this.notifyIcon1.Visible = true;
            }
        }
        
        //窗体关闭
        private void FrmApi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        //单据托盘图标还原程序窗体
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
            }
        }

        //右键显示窗体
        private void tsmShowfrm_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
            }
        }

        //右键退出程序
        private void tsmExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        #endregion


        #region 库存更新

        private async void btnUpdateStockStart_Click(object sender, EventArgs e)
        {
            if (ddlUpdateStockTime.SelectedValue==null)
            {
                MessageBox.Show("请选择库存更新同步间隔的时间.");
            }
            else
            {
                int time = Convert.ToInt32(ddlUpdateStockTime.SelectedValue);
                await _simpleTriggger.Run_UpdateStock(time * 60);
                btnUpdateStockStart.Text = "库存更新同步启用中..";
            }
        }

        private async void btnUpdateStockStop_Click(object sender, EventArgs e)
        {
            await _simpleTriggger.Delete_UpdateStock();
            btnUpdateStockStart.Text = "库存更新同步启动";
        }


        #endregion


        #region 订单同步

        private async void btnOrderStart_Click(object sender, EventArgs e)
        {
            //_wedoctorApp.Post_SendOrderList();
            if (ddlOrderTime.SelectedValue == null)
            {
                MessageBox.Show("请选择订单更新同步间隔的时间.");
            }
            else
            {
                int time = Convert.ToInt32(ddlOrderTime.SelectedValue);
                await _simpleTriggger.Run_SendOrderList(time * 60);
                btnOrderStart.Text = "订单同步启用中..";
            }
        }

        private async void btnOrderStop_Click(object sender, EventArgs e)
        {
            await _simpleTriggger.Delete_SendOrderList();
            btnOrderStart.Text = "订单同步启动";
        }

        #endregion


        #region 订单异常同步

        private async void btnOrderExStart_Click(object sender, EventArgs e)
        {
            //_wedoctorApp.Post_SendOrderRefuse();
            if (ddlOrderExTime.SelectedValue == null)
            {
                MessageBox.Show("请选择异常订单更新同步间隔的时间.");
            }
            else
            {
                int time = Convert.ToInt32(ddlOrderExTime.SelectedValue);
                await _simpleTriggger.Run_SendOrderRefuse(time * 60);
                btnOrderExStart.Text = "订单异常同步启用中..";
            }
        }

        private async void btnOrderExStop_Click(object sender, EventArgs e)
        {
            await _simpleTriggger.Delete_SendOrderRefuse();
            btnOrderExStart.Text = "订单异常同步启动";
        }

        #endregion


        #region 订单发货同步

        private async void btnDeliveryStart_Click(object sender, EventArgs e)
        {
            //_wedoctorApp.Post_SendOrderDelivery();
            if (ddlDeliveryTime.SelectedValue == null)
            {
                MessageBox.Show("请选择订单发货更新同步间隔的时间.");
            }
            else
            {
                int time = Convert.ToInt32(ddlDeliveryTime.SelectedValue);
                await _simpleTriggger.Run_SendOrderDelivery(time * 60);
                btnDeliveryStart.Text = "订单发货同步启用中..";
            }
        }

        private async void btnDeliveryStop_Click(object sender, EventArgs e)
        {
            await _simpleTriggger.Delete_SendOrderDelivery();
            btnDeliveryStart.Text = "订单发货同步启动";
        }

        #endregion
    }
}
