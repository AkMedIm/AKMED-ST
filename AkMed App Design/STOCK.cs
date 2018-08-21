using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AkMed_App_Design
{
    public partial class STOCK : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\BET TECHNOSPIDER\Gestion de Stock C# & SQL Server\AkMed App Design\AkMed App Design\inventaire.mdf;Integrated Security=True");

        public STOCK()
        {
            InitializeComponent();
        }

        private void STOCK_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from stock";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
           int i = 0;

            SqlCommand cmd1 = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Achats";
            cmd.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;


            foreach (DataRow dr in dt1.Rows)
            {

                i = i + Convert.ToInt32(dr["productTotal"].ToString());
            }

            TOTAL_DH.Text = i.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (Convert.ToInt32(row.Cells[2].Value) == 0)
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.OrangeRed;
                    row.DefaultCellStyle.Font = new Font("Arial", 12.5F, GraphicsUnit.Pixel);
                }
                else
                {
                    if (Convert.ToInt32(row.Cells[2].Value) < 400 && Convert.ToInt32(row.Cells[2].Value) != 0)
                         {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.Firebrick;
                    row.DefaultCellStyle.Font = new Font("Arial", 12.5F, GraphicsUnit.Pixel);

                        }
                         else
                         {
                             row.DefaultCellStyle.BackColor = Color.White;
                             row.DefaultCellStyle.ForeColor = Color.Black;
                         }

                }
           
                

              
              /*   if (row.Cells[9] < DateTime.Now())
                 {
                 }
                 else
                 {

                 }*/

            }
        }
    }
}
