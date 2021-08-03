using MJ.Core.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MJ.DbConfigTool
{
    public partial class frmConfigTool : Form
    {
        public frmConfigTool()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox1.Text.Trim().Length < 8)
                {
                    MessageBox.Show("手动输入的key，长度不能低于8位");
                }
                else
                {
                    this.txtEncrypted.Text = DES.Encrypt(this.txtSource.Text.Trim(), this.textBox1.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox1.Text.Trim().Length < 8)
                {
                    MessageBox.Show("手动输入的key，长度不能低于8位");
                }
                else
                {
                    this.txtSource.Text = DES.Decrypt(this.txtEncrypted.Text.Trim(), this.textBox1.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void frmConfigTool_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = DES.encryKey;
        }
    }
}
