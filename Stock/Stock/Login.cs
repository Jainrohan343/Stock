using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tb_Pass.Text = "";
            tb_User.Text = "";
            tb_User.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {//check login  Username and Passward

            SqlConnection con = new SqlConnection("Data Source=LIN330168;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT* FROM [Stock].[dbo].[Login] Where UserName='" + tb_User.Text + "' and Passward='" + tb_Pass.Text + "'", con);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid UserName or Passward", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1_Click(sender, e);
            }

        }
    }
}
