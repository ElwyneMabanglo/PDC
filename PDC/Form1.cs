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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT * FROM users WHERE username= '" + txtUsername.Text.Trim() + "' AND password = '" + txtPassword.Text.Trim() + "'";
       


            MySqlDataAdapter da = new MySqlDataAdapter();   
            da.SelectCommand = sqlCmd;
            DataTable daTbl = new DataTable();
            da.Fill(daTbl);

           
            if (daTbl.Rows.Count == 1)
            {
                // https://stackoverflow.com/questions/9022118/access-cell-value-of-datatable
                string id = daTbl.Rows[0][0].ToString();
                controlPanel x = new controlPanel(id);
                x.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Username/Password");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            register x = new register();
            x.Show();
        }
    }
}
