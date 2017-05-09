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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT * FROM users WHERE users.username = '" + txtUsername.Text.Trim() + "'";
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlCmd;
                DataTable daTbl = new DataTable();

                da.Fill(daTbl);

                if (daTbl.Rows.Count > 0)
                {
                    MessageBox.Show("User Exists Already");
                } else
                {

            
                    MySqlConnection con = new MySqlConnection(connString);
                    string query = null;
                    query = "INSERT INTO  users (username,password) VALUES (@username,@password)"; 
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    con.Close();
                    this.Hide();
                    Form1 x = new Form1();
                    x.Show();
                    MessageBox.Show("Account Registered");
                }



            }
            catch (MySqlException er)
            {
                MessageBox.Show("Error:" + er.ToString());
            }
        }
    }
}
