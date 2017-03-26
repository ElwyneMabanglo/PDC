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
        public string idHome = null;
        public string widthCell = null;
        public string heightCell = null;
        private static room[,] roomList;

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

            roomList = new room[Int32.Parse(widthCell.Trim()),Int32.Parse(heightCell.Trim())];


            // Create Objects and add to Objects list
            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                for (int col = 0; col < dgv.Rows[row].Cells.Count; col++)
                {
    
                    room newRoom = new room(Int32.Parse(cmbHome.SelectedValue.ToString()),"", 0, 0, 0, 0,row,col); ;
                    roomList[row,col] = newRoom;
                }
            }
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
                idHome = s;
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cellValue = dgv.Rows[0].Cells[0].Value.ToString();
            MessageBox.Show(cellValue);
        }

        private void button3_Click(object sender, EventArgs e)
        {


        try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection con = new MySqlConnection(connString);
                string query = "INSERT INTO  rooms (idHouse,roomType,north,east,south,west,x,y) VALUES (@idHouse,@roomType,@north,@east,@south,@west,@x,@y)";

                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();


                for (int row = 0; row < dgv.Rows.Count; row++)
                {

                    

                    for (int col = 0; col < dgv.Rows[row].Cells.Count; col++)
                    {

                        cmd.Parameters.AddWithValue("@idHouse", roomList[row, col].IdHouse);
                        cmd.Parameters.AddWithValue("@roomType", roomList[row, col].RoomType);
                        cmd.Parameters.AddWithValue("@north", roomList[row, col].North);
                        cmd.Parameters.AddWithValue("@east", roomList[row, col].East);
                        cmd.Parameters.AddWithValue("@south", roomList[row, col].South);
                        cmd.Parameters.AddWithValue("@west", roomList[row, col].West);
                        cmd.Parameters.AddWithValue("@x", roomList[row, col].X);
                        cmd.Parameters.AddWithValue("@y", roomList[row, col].Y);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                con.Close();
            }
            catch (MySqlException er)
            {
                MessageBox.Show("Error:" + er.ToString());
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            cmbHome.Enabled = false;

            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT * FROM rooms WHERE rooms.idHome = " + idHome;

            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;
            DataTable daTbl = new DataTable();
            conn.Close();
            da.Fill(daTbl);

            dgv.Rows[0].Cells[0].Value = daTbl.Rows[0][0].ToString();

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Get Cell valu using X,Y - https://stackoverflow.com/questions/4351603/get-selected-value-from-combo-box-in-c-sharp-wpf
            roomList[Int32.Parse(txtX.Text), Int32.Parse(txtY.Text)].RoomType = cmboRoomType.Text;
            dgv.Rows[Int32.Parse(txtX.Text)].Cells[Int32.Parse(txtY.Text)].Value = cmboRoomType.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create DataGrid - https://stackoverflow.com/questions/11806007/simplest-way-of-programmatically-creating-a-datagridview-with-x-columns-by-y-row
            dgv.AutoGenerateColumns = false;

            // Set Height of cell based on height input
            int height = 400 / Int32.Parse(heightCell.Trim());
            dgv.RowTemplate.MinimumHeight = height;

            // Create DataGrid
            for (int i = 1; i <= Int32.Parse(widthCell.Trim()); i++)
            {
                dgv.Columns.Add("col" + i, "column " + i);
                dgv.Columns[i - 1].FillWeight = 1;
            }
            for (int j = 0; j < Int32.Parse(heightCell.Trim()); j++)
                dgv.Rows.Add();





            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT * FROM rooms WHERE rooms.idHouse = " + idHome;
     

            //MySqlDataAdapter da = new MySqlDataAdapter();
            //da.SelectCommand = sqlCmd;
            //DataTable daTbl = new DataTable();
            //conn.Close();
            //da.Fill(daTbl);


            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();

                MySqlDataReader reader = sqlCmd.ExecuteReader();
                roomList = new room[Int32.Parse(widthCell.Trim()), Int32.Parse(heightCell.Trim())];

                while (reader.Read())
                {
                    int idHouse = reader.GetInt32("idHouse");
                    string roomType = reader.GetString("roomType");
                    int north = reader.GetInt32("north");
                    int east = reader.GetInt32("east");
                    int south = reader.GetInt32("south");
                    int west = reader.GetInt32("west");
                    int x = reader.GetInt32("x");
                    int y = reader.GetInt32("y");

                    room newRoom = new room(idHouse, roomType, north, east, south, west, x, y);

                    roomList[x, y] = newRoom;
                    dgv.Rows[x].Cells[y].Value = roomType;

                }

                reader.Close();
            }
            catch (Exception ex)
            {
       
            }













           




        }
    }
}
