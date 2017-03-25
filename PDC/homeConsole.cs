using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PDC
{
    public partial class homeConsole : Form
    {
        public string idUser = null;

        public homeConsole(string value)
        {
            InitializeComponent();
            idUser = value;
            loadHomes();
        }

        private void btnBuildHome_Click(object sender, EventArgs e)
        {

            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            MySqlCommand sqlCmd = conn.CreateCommand();

            sqlCmd.CommandText = "INSERT INTO home (idUser,homeName,height,width) VALUES (@idUser, @homeName,@height, @width)";
            sqlCmd.Parameters.AddWithValue("@idUser", idUser);
            sqlCmd.Parameters.AddWithValue("@homeName", txtHomeName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@height", txtHeight.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@width", txtWidth.Text.Trim());
            sqlCmd.ExecuteNonQuery();
            conn.Close();
            loadHomes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        public void loadHomes()
        {
            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

            //sqlCmd.CommandText = "SELECT * FROM users WHERE username= '" + txtUsername.Text.Trim() + "' AND password = '" + txtPassword.Text.Trim() + "'";
            sqlCmd.CommandText = "SELECT idHome,homeName,width,length FROM home WHERE home.idUser = " + idUser;

            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;
            DataTable daTbl = new DataTable();
            da.Fill(daTbl);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = daTbl;
            dgvHomeList.DataSource = bSource;
        }
    }
}
