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
            try
            {
                if (Int32.Parse(txtHeight.Text.Trim()) < 2)
                {
                    MessageBox.Show("Value must be higher than 2");
                }
                else if (txtHomeName.Text == "")
                {
                    MessageBox.Show("Please Enter Home Name");
                }
                else
                {
                    string connString = "server=127.0.0.1;user id=root;database=pdc";
                    MySqlConnection conn = new MySqlConnection(connString);
                    conn.Open();
                    MySqlCommand sqlCmd = conn.CreateCommand();
                    sqlCmd.CommandText = "INSERT INTO home (idUser,homeName,y,x) VALUES (@idUser, @homeName,@y, @x)";
                    sqlCmd.Parameters.AddWithValue("@idUser", idUser);
                    sqlCmd.Parameters.AddWithValue("@homeName", txtHomeName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@y", txtHeight.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@x", txtHeight.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    conn.Close();
                    loadHomes();
                }
            }
            catch
            {
                MessageBox.Show("Please enter name / X/Y Value");
            }
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
            sqlCmd.CommandText = "SELECT idHome,homeName,y,x FROM home WHERE home.idUser = " + idUser;   
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;
            DataTable daTbl = new DataTable();
            da.Fill(daTbl);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = daTbl;
            dgvHomeList.DataSource = bSource;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedHome = null;
            // Get selected value from datagrid - https://stackoverflow.com/questions/7657137/datagridview-full-row-selection-but-get-single-cell-value
            if (dgvHomeList.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgvHomeList.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvHomeList.Rows[selectedrowindex];
                selectedHome = Convert.ToString(selectedRow.Cells["idHome"].Value);
            }
            string connString = "server=127.0.0.1;user id=root;database=pdc";
            string Query = "DELETE FROM home WHERE idHome='" + selectedHome + "';";
            MySqlConnection MyConn2 = new MySqlConnection(connString);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();
            MessageBox.Show("Home ID" + selectedHome + " Deleted");
            loadHomes();
            while (MyReader2.Read())
            {
            }
            MyConn2.Close();
        }
    }
}
