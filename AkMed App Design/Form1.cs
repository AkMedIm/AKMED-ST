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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\BET TECHNOSPIDER\Gestion de Stock C# & SQL Server\AkMed App Design\AkMed App Design\inventaire.mdf;Integrated Security=True");
        DataTable dt = new DataTable();
        int tot = 0;
        public Form1()
        {
            InitializeComponent();
        }

        int counter = 0;
        int compteur = 0;
        int len = 0;
        string txt;



        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'inventaireDataSet1.v_Sales' table. You can move, or remove it, as needed.
            this.v_SalesTableAdapter.Fill(this.inventaireDataSet1.v_Sales);
            // TODO: This line of code loads data into the 'inventaireDataSet.Commande' table. You can move, or remove it, as needed.
            this.commandeTableAdapter.Fill(this.inventaireDataSet.Commande);
//________________________________________________

            /** The Connection **/
//________________________________________________


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

//------------------------------------------------
       //--> Calling Functoins <-- //

            WaveHand.Hide();
            //Animating text
            txt = Welcoming.Text;
            len = Text.Length;
            Welcoming.Text = "";
            //Display Users function call
            displayusers();
            //DisplayUnites function call
            DisplayUnites();
            //Unity combobox
            Unitycombobox();
            //ProductList datagridview
            ProductsList();
            //filling the product combo in product panel
            ProdCombo();
            //filling Dealer Combobox
            FournisseurCombo();
            //Fournisseurs DataGridView
            FournisseurGridView();
            //filling the product combo in ventes panel
            ProductCombo();
//----------------------------------------------------------
            dt.Clear();
            dt.Columns.Add("Produit");
            dt.Columns.Add("Prix");
            dt.Columns.Add("Quantité");
            dt.Columns.Add("Total");
//----------------------------------------------------------
            //==> TOP 5 Products
          /*  SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top(5) product, SUM(Total)  from Commande GROUP BY  product  order by SUM(Total) desc";
            cmd.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(cmd);
            da9.Fill(dt9);
            dataGridView5.DataSource = dt9;*/
        }


        /*Timer Tick 1*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            utilisateurname.Hide();
            utilisateurfname.Hide();
            if(counter >= 11)
            {
                timer1.Stop();
                WaveHand.Show();
               utilisateurname.Show();
               utilisateurfname.Show();

            }
            else
            {
                Welcoming.Text = txt.Substring(0, counter);
                //utilisateurname.Show();
            }
            

        }

      



        /** START DRAG WINDOW CODE **/

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        Point lastPoint;
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        /** END DRAG WINDOW CODE **/

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     
        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        //UTILISATEURS

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            UserLine.Show();
            UnitesLine.Hide();
            VentesLine.Hide();
            StatLine.Hide();
            DisLine.Hide();
            AchatsLine.Hide();
            ProduitLine.Hide();
            Utilisateurs.BringToFront();
        }

        //UNITES

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            UserLine.Hide();
            UnitesLine.Show();
            VentesLine.Hide();
            StatLine.Hide();
            DisLine.Hide();
            AchatsLine.Hide();
            ProduitLine.Hide();
            Unites.BringToFront();
        }

        //PRODUITS

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            UserLine.Hide();
            UnitesLine.Hide();
            VentesLine.Hide();
            StatLine.Hide();
            DisLine.Hide();
            AchatsLine.Hide();
            ProduitLine.Show();
            Produits.BringToFront();

        }


        //Achats


        private void pictureBox8_Click(object sender, EventArgs e)
        {
            UserLine.Hide();
            UnitesLine.Hide();
            VentesLine.Hide();
            StatLine.Hide();
            DisLine.Hide();
            AchatsLine.Show();
            ProduitLine.Hide();
            Achats.BringToFront();
        }

        //Statistiques

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            int i = 0;
            DateTime today = DateTime.Today;
            TODAYDATE.Text = today.ToString("yyyy-MM-dd");
          //  TODAYDATE.Text = DateTime.Now.ToLongDateString();

            UserLine.Hide();
            UnitesLine.Hide();
            VentesLine.Hide();
            StatLine.Show();
            DisLine.Hide();
            AchatsLine.Hide();
            ProduitLine.Hide();
            Statistiques.BringToFront();


            //==> TOP 5 PRODUCTS

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top(5) product, SUM(Total)  from Commande GROUP BY  product  order by SUM(Total) desc";
            cmd.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(cmd);
            da9.Fill(dt9);
            dataGridView5.DataSource = dt9;


            //==> NUMBER OF ORDERS DONE TODAY


            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT COUNT( DISTINCT id), COUNT(DISTINCT product), COUNT( DISTINCT order_id), SUM(Total)  FROM Commande  WHERE ComDate ='" + TODAYDATE.Text + "'";
            cmd1.ExecuteNonQuery();
            DataTable dt11 = new DataTable();
            SqlDataAdapter da11 = new SqlDataAdapter(cmd1);
            da11.Fill(dt11);
           // dataGridView6.DataSource = dt11;
            NumCommande.Text = dt11.Rows[0][0].ToString();
            NumProducts.Text = dt11.Rows[0][1].ToString();
            ClientsToday.Text = dt11.Rows[0][2].ToString();
            TotalAmountToday.Text=dt11.Rows[0][3].ToString();

            //==> NUMBER OF CLIENTS AND PRODUCTS MONTH


            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT COUNT(DISTINCT product), COUNT( DISTINCT order_id)  FROM Commande  WHERE month(ComDate) ='" + DateTime.Now.Month+ "'";
            cmd2.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);
            NumProdMonth.Text= dt3.Rows[0][0].ToString();
            NumClientMonth.Text = dt3.Rows[0][1].ToString();

            //==> Linear Chart datatable

           /*  SqlCommand cmd3 = con.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "select Distinct ComDate, SUM(Total) from Commande group by ComDate";
            cmd3.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd3);
            da4.Fill(dt4);*/
            /*chart2.DataSource = v_SalesTableAdapter;

            

            chart2.Series["Series1"].XValueMember= "ComDate";
            chart2.Series["Series1"].YValueMembers =" Total";


            chart2.DataBind();*/


            //==> 

        /*    SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT COUNT(product) FROM Commande WHERE ComDate ='" + TODAYDATE.Text + "'";
            cmd2.ExecuteNonQuery();
            DataTable dt10 = new DataTable();
            SqlDataAdapter da10 = new SqlDataAdapter(cmd2);
            da11.Fill(dt10);
           // dataGridView6.DataSource = dt10;*/
          //  NumCommande.Text = dataGridView6.Rows[0].Cells[0].Value.ToString();


            MonthNow1.Text = DateTime.Now.ToString("MMMM");
            MonthNow2.Text = DateTime.Now.ToString("MMMM");



            //==> CHART


/*            SqlCommand cmd4 = con.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT Total FROM Commande ";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);*/

        }

        //Ventes

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            UserLine.Hide();
            UnitesLine.Hide();
            VentesLine.Show();
            StatLine.Hide();
            DisLine.Hide();
            AchatsLine.Hide();
            ProduitLine.Hide();
            Ventes.BringToFront();
        }

        //Fournisseurs

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            UserLine.Hide();
            UnitesLine.Hide();
            VentesLine.Hide();
            StatLine.Hide();
            DisLine.Show();
            AchatsLine.Hide();
            ProduitLine.Hide();
            Fournisseurs.BringToFront();
        }
        //LOGOUT
        private void logout_Click(object sender, EventArgs e)
        {
         
        }
        private void logout_Click_1(object sender, EventArgs e)
        {
            Login.Show();
            About.SendToBack();
            Contact.SendToBack();
            Header.Show();
             UserLine.Hide();
              UnitesLine.Hide();
              VentesLine.Hide();
              StatLine.Hide();
              DisLine.Hide();
              AchatsLine.Hide();
              ProduitLine.Hide();
            AboutLine.Hide();
                ContactLine.Hide();
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            About.BringToFront();
            AboutLine.Show();
            ContactLine.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Contact.BringToFront();
            AboutLine.Hide();
            ContactLine.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WelcomePage.BringToFront();
            About.SendToBack();
            Contact.SendToBack();
            Header.BringToFront();
            UserLine.Hide();
            UnitesLine.Hide();
            VentesLine.Hide();
            StatLine.Hide();
            DisLine.Hide();
            AchatsLine.Hide();
            ProduitLine.Hide();
            AboutLine.Hide();
            ContactLine.Hide();
        }

        private void logbox_TextChanged(object sender, EventArgs e)
        {

        }

         //_______________________________

         /** Coding Login Panel  **/

        //________________________________


        private void Connection_Click(object sender, EventArgs e)
        {
            
            int i = 0;
            string Mode;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from registration where login='" + LoginBox.Text + "' and motdepasse='" + PassBox.Text + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
          

            foreach (DataRow dr in dt.Rows)
            {
                Mode= dr["UserMode"].ToString();
                UserMod.Text = Mode.ToString();
                utilisateurname.Text = dr["nom"].ToString();
                utilisateurfname.Text=dr["prenom"].ToString();
            }

            

            if (i == 0)
            {
                WrongFill.Visible = true;
               // MessageBox.Show("This username password does not match");
            }
    

            else if(UserMod.Text=="Admin") 
            {

                Login.Hide();
                LoginBox.Clear();
                PassBox.Clear();
                WelcomePage.BringToFront();
                Header.Show();
                Waiting.Show();
                WaitingTimer.Start();
                WrongFill.Visible = false;
            }
            else
            {
               // button1.Enabled = false;
               // SupprimerCommande.Enabled = false;
               // commander.Enabled = false;
                ajoutstock.Enabled = false;
                Ajouterbutton.Enabled = false;
                Annulation.Enabled = false;
                Moduser.Enabled = false;
                Suppbutton.Enabled = false;
                DelDealer.Enabled = false;
                SaveDealer.Enabled = false;
                SupprimerUnite.Enabled = false;
                AjouterUnite.Enabled = false;
                Delbutton.Enabled = false;
                Modbutton.Enabled = false;
                AjoutButton.Enabled = false;


                Login.Hide();
                LoginBox.Clear();
                PassBox.Clear();
                WelcomePage.BringToFront();
                Header.Show();
                Waiting.Show();
                WaitingTimer.Start();
                WrongFill.Visible = false;

            }
            
        }

        private void WaitingTimer_Tick(object sender, EventArgs e)
        {

           

            compteur++;
            if(compteur >= 5)
            {
                WaitingTimer.Stop();
                Waiting.Hide();
                timer1.Start();
            }
        }

        
        
        /** displayUsers Function  **/
        public void displayusers()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from registration";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        //** End function



        /** Ajouter et Afichage Les Utilisaterurs **/

        private void Ajouterbutton_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from registration where login = '" + LogBox.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into registration values('" + PrenomBox.Text + "','" + NomBox.Text + "','" + LogBox.Text + "','" + PassWordBox.Text + "','" + MailBox.Text + "','" + PhoneBox.Text + "','" +Comptetype.SelectedItem.ToString()+ "')";
                cmd1.ExecuteNonQuery();
                PrenomBox.Text = "";
                NomBox.Text = "";
                LogBox.Text = "";
                PassWordBox.Text = "";
                MailBox.Text = "";
                PhoneBox.Text = "";
                displayusers();
                MessageBox.Show("Utilisateur a été ajouté avec succès!");
            }
            else
            {
                MessageBox.Show("Ce login est déjá été utilisé, choisi un autre");
            }
        }

        private void Suppbutton_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from registration where id =" + id + "";
            cmd.ExecuteNonQuery();
            displayusers();
            MessageBox.Show("Supprimé avec succès!");
        }

//----------------------------------------------------------------------------



        /** DisplayUnites Function **/
        public void DisplayUnites()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from unites";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        //____________________________




        /** Ajouter et Afficher Les Unités **/
        private void AjouterUnite_Click(object sender, EventArgs e)
        {
            int count = 0;

            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select  * from unites where unit = '" + UniteBox.Text + "'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            count = Convert.ToInt32(dt1.Rows.Count.ToString());
            if (count == 0)
            {

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into unites values('" + UniteBox.Text + "')";
                cmd.ExecuteNonQuery();
                DisplayUnites();

            }

            else
            {
                MessageBox.Show("This Unit is already added!");
            }
        }

        private void SupprimerUnite_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete  from unites where id=" + id + "";
            cmd.ExecuteNonQuery();
            DisplayUnites();
        }

        
        //private void Unitylabel_Click(object sender, EventArgs e){}


     /** Ajouter dans la liste des produit **/

        /** - filling the Unity combobox- **/
        public void Unitycombobox()
        {
            UnitycomboBox.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from unites";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                UnitycomboBox.Items.Add(dr["unit"].ToString());
            }

        }
        //_______________________________________
        /** - filling the product list datagridview- **/
        public void ProductsList()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from productList";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView3.DataSource = dt;
        }
        //_________________________________________

        private void AjoutButton_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into productList values('" + ProductBox.Text + "','" + UnitycomboBox.SelectedItem.ToString() + "')";
            cmd.ExecuteNonQuery();
            ProductBox.Text = "";
            ProductsList();
            MessageBox.Show("record inserted successfully");
        }
        //________________________________


        /** Modification d'un Produit **/
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductModif.Visible = true;
            int i = Convert.ToInt32(dataGridView3.SelectedCells[0].Value.ToString());

            UnModcomboBox.Items.Clear();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from unites";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                UnModcomboBox.Items.Add(dr2["unit"].ToString());
            }

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from productList where id=" + i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                produitBox.Text = dr["productName"].ToString();
               UnModcomboBox.SelectedItem = dr["units"].ToString();
            }
        }

        private void Modbutton_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(dataGridView3.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update productList set productName='" + produitBox.Text + "',units='" + UnModcomboBox.SelectedItem.ToString() + "'where id='" + i + "'";
            cmd.ExecuteNonQuery();
            ProductsList();
            ProductModif.Visible = false;
            MessageBox.Show("Táche accomplie avec succés!");
        }

        private void Delbutton_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView3.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from productList where id=" + id + "";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Le produit est retiré de la liste!");
            ProductsList();
        }

        //___________________________________________________

       /**-- End Product Panel -**/



        /** Start Achats Panel **/
        //*- filling the product combo function
        public void ProdCombo()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from  productList";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SelectProductcomboBox.Items.Add(dr["productName"].ToString());
            }
        }

       
        //END



        //*- filling Fournisseur combo function
        public void FournisseurCombo()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Fournisseur";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                FournisseurcomboBox.Items.Add(dr["dealerName"].ToString());
            }

        }
        //END


        /**________Ajouter Dans Le Stock_______**/

        // also updating the stock
        private void SelectProductcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from productList where productName='" + SelectProductcomboBox.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                unité.Text = dr["units"].ToString();
            }
        }

        private void PrixUnitaireBox_Leave(object sender, EventArgs e)
        {
            TotalBox.Text = Convert.ToString(Convert.ToInt32(ProductQuantityBox.Text) * Convert.ToInt32 (PrixUnitaireBox.Text));
        }
    
      

        private void ajoutstock_Click(object sender, EventArgs e)
        {
            int i;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from stock where productName='" + SelectProductcomboBox.Text + "'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            i = Convert.ToInt32(dt1.Rows.Count.ToString());
            
            
            if(i==0)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Achats values('" + SelectProductcomboBox.Text + "','" + ProductQuantityBox.Text + "','" + unité.Text + "','" + PrixUnitaireBox.Text + "','" + TotalBox.Text + "','" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "','" + FournisseurcomboBox.Text + "','" + TypeAchatcomboBox.Text + "','" + dateTimePicker2.Value.ToString("MM-dd-yyyy") + "','" + ProfitBox.Text + "')";
                cmd.ExecuteNonQuery();

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "insert into stock values('" + SelectProductcomboBox.Text + "','" + ProductQuantityBox.Text + "','" + unité.Text + "')";
                cmd3.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "insert into Achats values('" + SelectProductcomboBox.Text + "','" + ProductQuantityBox.Text + "','" + unité.Text + "','" + PrixUnitaireBox.Text + "','" + TotalBox.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + FournisseurcomboBox.Text + "','" + TypeAchatcomboBox.Text + "','" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','" + ProfitBox.Text + "')";
                cmd2.ExecuteNonQuery();

                SqlCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                cmd4.CommandText = "update stock set productQuantity = productQuantity + '" + ProductQuantityBox.Text + "' where productName='" + SelectProductcomboBox.Text + "'";
                cmd4.ExecuteNonQuery();
            }

            SelectProductcomboBox.Text = "";
            ProductQuantityBox.Text = "";
            unité.Text = "";
            PrixUnitaireBox.Text = "";
            TotalBox.Text = "";
            FournisseurcomboBox.Text = "";
            TypeAchatcomboBox.Text = "";
            ProfitBox.Text = "";



            MessageBox.Show("Terminé !");

        }
        /**--END ACHATS PANEL--**/
        /**--START FOURNISSEURS PANEL--**/



        //Ajouter Fournisseur
        private void SaveDealer_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Fournisseur values('" + NomFournisseur.Text + "','" + société.Text + "','" + téléphone.Text + "','" + ville.Text + "','" + adresse.Text + "')";
            cmd.ExecuteNonQuery();
            NomFournisseur.Text = "";
            société.Text = "";
            téléphone.Text = "";
            ville.Text = "";
            adresse.Text = "";
            FournisseurGridView();
            MessageBox.Show("Inséré avec succès!");
        }

        //Supprimer Fournisseur
        private void DelDealer_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView4.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Fournisseur where id =" + id + "";
            cmd.ExecuteNonQuery();
            FournisseurGridView();
            MessageBox.Show("Supprimer avec succès!");
        }

        
        //Fournisseurs Gridview function
        private void FournisseurGridView()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Fournisseur";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView4.DataSource = dt;

        }
        /**--END FOURNISSEURS PANEL--**/
        /**--START VENTES PANEL--**/

            //Afichage du prix unitaire correspond au produit
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from Achats where productName='" + VenteProduct.Text + "'";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                Prixunit.Text = dr["productPrice"].ToString();
            }
        }

            //filling the product combobox
        public void ProductCombo()
        {
            VenteProduct.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from  productList";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                VenteProduct.Items.Add(dr["productName"].ToString());
            }
        }

            //le champs total
        private void QuantitéBox_Leave(object sender, EventArgs e)
        {
            try
            {
                Boxtotal.Text = Convert.ToString(Convert.ToInt32(QuantitéBox.Text) * Convert.ToInt32(Prixunit.Text));
            }
            catch (Exception ex)
            {

               MessageBox.Show("remplez les champs !");
            }
        }

            //Passer la commande
        private void Commander_Click(object sender, EventArgs e)
        {
            int stock = 0;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from stock where productName='" + VenteProduct.Text + "'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                stock = Convert.ToInt32(dr1["productQuantity"].ToString());
            }
            if (Convert.ToInt32(QuantitéBox.Text) > stock)
            {
                MessageBox.Show("Quantité Insuffisante");
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["Produit"] = VenteProduct.Text;
                dr["Prix"] = Prixunit.Text;
                dr["Quantité"] = QuantitéBox.Text;
                dr["Total"] = Boxtotal.Text;
                dt.Rows.Add(dr);
                VenteGridView.DataSource = dt;
                tot = tot + Convert.ToInt32(dr["Total"].ToString());
                labelTotal.Text = tot.ToString();
                MessageBox.Show("Operation complete!");

                
                
                
                VenteProduct.SelectedItem = null;
                Prixunit.Clear();
                QuantitéBox.Clear();
                Boxtotal.Clear();
            }


        }

        private void SupprimerCommande_Click(object sender, EventArgs e)
        {
            try
            {
                tot = 0;
                dt.Rows.RemoveAt(Convert.ToInt32(VenteGridView.CurrentCell.RowIndex.ToString()));
                foreach (DataRow dr1 in dt.Rows)
                {
                    tot = tot + Convert.ToInt32( dr1["Total"].ToString());
                }
                labelTotal.Text = tot.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("WTF!");
                throw;
            }
        }




        // Modification de L'utilisateur ( USER PANEL)

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UserModif.Visible = true;
            UserAddPanel.Visible = false;
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from registration where id=" + i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Modprenom.Text = dr["prenom"].ToString();
                Modnom.Text = dr["nom"].ToString();
                Modlogin.Text = dr["login"].ToString();
                Modipasse.Text = dr["motdepasse"].ToString();
                ModEmail.Text = dr["email"].ToString();
                Modtel.Text = dr["contact"].ToString();
                ModCompte.SelectedItem = dr["UserMode"].ToString();
                
            }
        }

        private void Moduser_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update registration set prenom='" + Modprenom.Text + "',nom='" + Modnom.Text + "',login='" + Modlogin.Text + "',motdepasse='" + Modipasse.Text + "',email='" + ModEmail.Text + "',contact='" + Modtel.Text + "',UserMode='" + ModCompte.SelectedItem.ToString()+ "'where id='" + i + "'";
            cmd.ExecuteNonQuery();
            ProductsList();
            UserModif.Visible = false;
            UserAddPanel.Visible = true;
            displayusers();
            MessageBox.Show("Táche accomplie avec succés!");
        }

        private void Annulation_Click(object sender, EventArgs e)
        {
            UserModif.Visible=false;
            UserAddPanel.Visible = true;

        }

        private void AffichageSTOCK_Click(object sender, EventArgs e)
        {
            STOCK st = new STOCK();
            st.Show();
        }

        private void pictureBox33_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //  Enregistrer et Imprimer

        private void EnregistrerETimprimer_Click(object sender, EventArgs e)
        {
            //string orderid="";

            //SqlCommand cmd = con.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "insert into Client values ('" +PrenomClient.Text+ "','" +NomClient.Text+ "','" + TypePaiement.SelectedItem.ToString() + "','" + DatePaiement.Value.ToString("yyyy-MM-dd") + "')";
            //cmd.ExecuteNonQuery();

            //SqlCommand cmd1 = con.CreateCommand();
            //cmd1.CommandType = CommandType.Text;
            //cmd1.CommandText = "select top(1)* from Commande order by id desc";
            //cmd1.ExecuteNonQuery();
            //DataTable dt9 = new DataTable();
            //SqlDataAdapter da9 = new SqlDataAdapter(cmd1);
            //da9.Fill(dt9);
            //foreach (DataRow dr9 in dt9.Rows)
            //{
            //    orderid = dr9["id"].ToString();
            //}

            //foreach (DataRow dr in dt.Rows)
            //{
            //    int qty = 0;
            //    string pname = "";

            //SqlCommand cmd2 = con.CreateCommand();
            //cmd2.CommandType = CommandType.Text;
            //cmd2.CommandText = "insert into Commande values ('" + orderid.ToString() + "','" + dr["Produit"].ToString() + "','" + dr["Prix"].ToString() + "','" + dr["Quantité"].ToString() + "','" + dr["Total"].ToString() + "' , '" + DatePaiement.Value.ToString("yyyy-MM-dd") + "' , '" + PrenomClient.Text + "','" + NomClient.Text + "','" + TypePaiement.SelectedItem.ToString() + "')";
            //cmd2.ExecuteNonQuery();


            //qty = Convert.ToInt32(dr["Quantité"].ToString());
            //pname = dr["Produit"].ToString();
                    

            //SqlCommand cmd3 = con.CreateCommand();
            //cmd3.CommandType = CommandType.Text;
            //cmd3.CommandText = "update stock set productQuantity = productQuantity -" + qty + " where productName='" + pname.ToString() + "'";
            //cmd3.ExecuteNonQuery();
            //}

            
            //PrenomClient.Clear();
            //NomClient.Clear();
            //TypePaiement.SelectedItem = null;
            //dt.Clear();
            //VenteGridView.DataSource = dt;
            //MessageBox.Show("Récord ajouté avec succes!");
            //CommandeBILL bill = new CommandeBILL();
            //bill.get_value(Convert.ToInt32( orderid.ToString()));
            //bill.Show();

        }

        // Serial Number of TOP 5 PRODUCTS
         
        private void dataGridView5_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView5.Rows[e.RowIndex].Cells["No"].Value = (e.RowIndex + 1).ToString();
        }

        // ****
        private void button1_Click(object sender, EventArgs e)
        {
            string orderid = "";

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Client values ('" + PrenomClient.Text + "','" + NomClient.Text + "','" + TypePaiement.SelectedItem.ToString() + "','" + DatePaiement.Value.ToString("yyyy-MM-dd") + "')";
            cmd.ExecuteNonQuery();

            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            //cmd1.CommandText = "select  * from Client where id= IDENT_CURRENT('Client')";
            cmd1.CommandText = "select top 1 * from Client order by id desc";
            cmd1.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(cmd1);
            da9.Fill(dt9);
            foreach (DataRow dr9 in dt9.Rows)
            {
                orderid = dr9["id"].ToString();
            }

            foreach (DataRow dr in dt.Rows)
            {
                int qty = 0;
                string pname = "";

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "insert into Commande values ('" + orderid.ToString() + "','" + dr["Produit"].ToString() + "','" + dr["Prix"].ToString() + "','" + dr["Quantité"].ToString() + "','" + dr["Total"].ToString() + "' , '" + DatePaiement.Value.ToString("yyyy-MM-dd") + "')";
                cmd3.ExecuteNonQuery();


                qty = Convert.ToInt32(dr["Quantité"].ToString());
                pname = dr["Produit"].ToString();


                SqlCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                cmd4.CommandText = "update stock set productQuantity = productQuantity -" + qty + " where productName='" + pname.ToString() + "'";
                cmd4.ExecuteNonQuery();
            }


           /* PrenomClient.Text="";
            NomClient.Text="";
            labelTotal.Text = "";*/
           // TypePaiement.SelectedItem = null;
            dt.Clear();
            VenteGridView.DataSource = dt;
            MessageBox.Show("Récord ajouté avec succes!");

            billingcs bill = new billingcs();
            bill.get_value(Convert.ToInt32(orderid.ToString()));
            bill.Show();

        }
        

       
       

       

      

        }
       
       
    
}
