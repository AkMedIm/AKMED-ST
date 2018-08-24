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
    public partial class CommandeBILL : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\BET TECHNOSPIDER\Gestion de Stock C# & SQL Server\AkMed App Design\AkMed App Design\inventaire.mdf;Integrated Security=True");

        int j;
        int tot = 0;

        public CommandeBILL()
        {
            InitializeComponent();
        }

        public void get_value(int i)
        {
            j = i;
        }

        private void CommandeBILL_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            DataSetBILL DSB = new DataSetBILL();

          /*  SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Client WHERE id ="+ j +"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(DSB.Client);*/



            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT * FROM Commande WHERE order_id= "+ j +" ";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(DSB.Commande);
            da1.Fill(dt1);

            tot = 0;
            foreach (DataRow dr1 in dt1.Rows)
            {
                tot = tot + Convert.ToInt32(dr1["Total"].ToString());
            }

            CrystalReport3 report = new CrystalReport3();
            report.SetDataSource(DSB);
            report.SetParameterValue("Total", tot.ToString());
            crystalReportViewer1.ReportSource = report;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
