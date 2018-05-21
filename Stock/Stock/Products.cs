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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //check data alreay there or not
            SqlDataAdapter sda = new SqlDataAdapter();

            //insert logic
            SqlConnection con = new SqlConnection("Data Source=LIN330168;Initial Catalog=Stock;Integrated Security=True");

            con.Open();
            bool status = false;
            if (comboBox1.SelectedIndex==0)
            {
                status = true;
            }
            var sqlQuery = "";
            if (ifProductExist(con, textBox1.Text))
            {
                //update
                sqlQuery = @"UPDATE [Stock].[dbo].[Products] SET [ProductName] = '" + textBox2.Text + "',[ProductStatus] = '" + status + "' WHERE [ProductCode] ='" + textBox1.Text + "'";
            }
            else
            {
                //insert
                sqlQuery = @"INSERT INTO [Stock].[dbo].[Products]([ProductCode],[ProductName]
   ,[ProductStatus]) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + status + "')";
            }
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //write thing in datagrid
            LoadData();
        }
        
        private bool ifProductExist(SqlConnection con,string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select* from [Stock].[dbo].[Products] where [ProductCode]='"+productCode+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
        public  void LoadData()
        {
            SqlConnection con = new SqlConnection("Data Source=LIN330168;Initial Catalog=Stock;Integrated Security=True");

            //write thing in datagrid
            SqlDataAdapter sda = new SqlDataAdapter("select* from [Stock].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                string Status = item["ProductStatus"].ToString();
                if (Status == "True")
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";

                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }

            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
           
          
        }

        private void dataGridView1_MouseClick_1(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {

                comboBox1.SelectedIndex = 0;

            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//Add
            //insert logic
            SqlConnection con = new SqlConnection("Data Source=LIN330168;Initial Catalog=Stock;Integrated Security=True");
            var sqlQuery = "";
            if (ifProductExist(con, textBox1.Text))
            {
                //update
                sqlQuery = @"DELETE FROM [Stock].[dbo].[Products]
      WHERE ProductCode='" + textBox1.Text + "'";
                con.Open();

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                //Error message
                MessageBox.Show("Record Doesnot exist...");
                }
           
            //write thing in datagrid
            textBox1.Text = "";
            textBox2.Text = "";
            
            LoadData();
        }
    }
}
