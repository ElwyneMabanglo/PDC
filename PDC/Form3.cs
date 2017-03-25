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
    public partial class Form3 : Form
    {
        public string idUser = null;
        public string widthCell = null;
        public string heightCell = null;

        public Form3(string value)
        {
            InitializeComponent();
            idUser = value;
            loadHomes();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Create DataGrid - https://stackoverflow.com/questions/11806007/simplest-way-of-programmatically-creating-a-datagridview-with-x-columns-by-y-row
            dgv.AutoGenerateColumns = false;
            
            // Set Height of cell based on height input
            int height = 400 / Int32.Parse(heightCell.Trim());
            dgv.RowTemplate.MinimumHeight = height;

            // Create DataGrid
            for (int i = 1; i <= Int32.Parse(widthCell.Trim()) ; i++)
            {
                dgv.Columns.Add("col" + i, "column " + i);
                dgv.Columns[i - 1].FillWeight = 1;
            }
            for (int j = 0; j < Int32.Parse(heightCell.Trim()); j++)
                dgv.Rows.Add();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get Cell Cordinates - https://msdn.microsoft.com/en-us/library/x8x9zk5a(v=vs.110).aspx
            Int32 selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                for (int i = 0; i < selectedCellCount; i++)
                {
                    // Get X,Y of Selected Cell
                    txtX.Text = dgv.SelectedCells[i].RowIndex.ToString();
                    txtY.Text = dgv.SelectedCells[i].ColumnIndex.ToString();
                }
            }
        }

        public void loadHomes()
        {
            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT idHome,homeName FROM home WHERE home.idUser = " + idUser;

            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;
            DataTable daTbl = new DataTable();
            conn.Close();
            da.Fill(daTbl);
            cmbHome.DataSource = daTbl;
            cmbHome.DisplayMember = "homeName";
            cmbHome.ValueMember = "idHome";
            //cmbCompName.DataBind();
            cmbHome.Enabled = true;
        }

        public void setFloor(string value)
        {
            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT width,height FROM home WHERE home.idHome = " + value;

            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;
            DataTable daTbl = new DataTable();
            conn.Close();
            da.Fill(daTbl);

            //textBox1.Text = daTbl.Rows[0][0].ToString();

            widthCell = daTbl.Rows[0][0].ToString();
            heightCell = daTbl.Rows[0][1].ToString();


        }

        private void cmbHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            String s;
            s = cmbHome.SelectedValue.ToString();
            
            try
            {
                setFloor(s);
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
