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
        public string idSensor = null;
        public string idRoom = null;
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
            int height = 500 / Int32.Parse(heightCell.Trim());
            dgv.RowTemplate.MinimumHeight = height;

            // Create DataGrid
            for (int i = 1; i <= Int32.Parse(widthCell.Trim()); i++)
            {
                dgv.Columns.Add("col" + i, "column " + i);
                dgv.Columns[i - 1].FillWeight = 1;
            }
            for (int j = 0; j < Int32.Parse(heightCell.Trim()); j++)
                dgv.Rows.Add();

            roomList = new room[Int32.Parse(widthCell.Trim()), Int32.Parse(heightCell.Trim())];

            //MessageBox.Show("Width:" + Int32.Parse(widthCell.Trim()) + " Length:" + Int32.Parse(heightCell.Trim()));

            //MessageBox.Show("Width:" + dgv.Rows[0].Cells.Count + " Length:" + dgv.Rows.Count);

            // Create Objects and add to Objects list
            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                for (int col = 0; col < dgv.Rows[row].Cells.Count; col++)
                {
                    room newRoom = new room(Int32.Parse(cmbHome.SelectedValue.ToString()), "", 0, 0, 0, 0, row, col);
                    roomList[col,row] = newRoom;
                }
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get Cell Cordinates - https://msdn.microsoft.com/en-us/library/x8x9zk5a(v=vs.110).aspx
            Int32 selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);

            string x = null;
            string y = null;

            if (selectedCellCount > 0)
            {
                for (int i = 0; i < selectedCellCount; i++)
                {
                    // Get X,Y of Selected Cell
                    txtY.Text = dgv.SelectedCells[i].RowIndex.ToString();
                    txtX.Text = dgv.SelectedCells[i].ColumnIndex.ToString();
                    y = dgv.SelectedCells[i].RowIndex.ToString();
                    x = dgv.SelectedCells[i].ColumnIndex.ToString();
                }
            }

            //MessageBox.Show("X: " + x + "Y: " + y);


            try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.CommandText = "SELECT idRoom FROM rooms WHERE x = @x AND y = @y AND idHouse = @idHouse ";
                sqlCmd.Parameters.Add(new MySqlParameter("@x", x));
                sqlCmd.Parameters.Add(new MySqlParameter("@y", y));
                sqlCmd.Parameters.Add(new MySqlParameter("@idHouse", idHome));

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlCmd;
                DataTable daTbl = new DataTable();
                da.Fill(daTbl);

                idRoom = daTbl.Rows[0][0].ToString();

                //MessageBox.Show("Room ID: " + idRoom);


                sensorList();
            }
            catch
            {

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
            cmbHome.Enabled = true;
            cmbHome.SelectedIndex = 0;
        }

        public void setFloor(string value)
        {
            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT x,y FROM home WHERE home.idHome = " + value;

            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;
            DataTable daTbl = new DataTable();
            conn.Close();
            da.Fill(daTbl);

            widthCell = daTbl.Rows[0][0].ToString();
            heightCell = daTbl.Rows[0][1].ToString();

            //MessageBox.Show("Selected width: " + widthCell + " Selected lenght: " + heightCell);
        }

        private void cmbHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            String s;
            s = cmbHome.SelectedValue.ToString();

            //MessageBox.Show("Selected Home: " + s);

            try
            {
                setFloor(s);
                idHome = s;
                //MessageBox.Show("Selected Home: " + idHome);
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


        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Get Cell valu using X,Y - https://stackoverflow.com/questions/4351603/get-selected-value-from-combo-box-in-c-sharp-wpf
                roomList[Int32.Parse(txtY.Text), Int32.Parse(txtX.Text)].RoomType = cmboRoomType.Text;
                //dgv.Rows[Int32.Parse(txtY.Text)].Cells[Int32.Parse(txtX.Text)].Value = cmboRoomType.Text;
                //MessageBox.Show("Y:" + Int32.Parse(txtY.Text) + " X:"+ Int32.Parse(txtX.Text));
                dgv.Rows[Int32.Parse(txtY.Text)].Cells[Int32.Parse(txtX.Text)].Value = cmboRoomType.Text;
            }
            catch
            {

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Create DataGrid - https://stackoverflow.com/questions/11806007/simplest-way-of-programmatically-creating-a-datagridview-with-x-columns-by-y-row
                dgv.AutoGenerateColumns = false;

                // Set Height of cell based on height input
                int height = 500 / Int32.Parse(heightCell.Trim());
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

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection con = new MySqlConnection(connString);

                string query = null;


                query = "INSERT INTO `sensors` (`idSensor`, `idRoom`, `sensorType`, `alarmStatus`, `deviceStatus`, `value`) VALUES (NULL, @idRoom, @sensorType, 'Not Monitoring', 'off', NULL);";


                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                cmd.Parameters.AddWithValue("@idRoom", idRoom);
                cmd.Parameters.AddWithValue("@sensorType", cmbSensors.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();


                sensorList();






            }
            catch
            {

            }
        }

        public void sensorList()
        {

            dgvSensors.DataSource = null;

            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;

            sqlCmd.CommandText = "SELECT idSensor,sensorType FROM sensors WHERE idRoom = @idRoom";
            sqlCmd.Parameters.Add(new MySqlParameter("@idRoom", idRoom));

            DataTable daTbl_2 = new DataTable();
            BindingSource bSource = new BindingSource();

            da.SelectCommand = sqlCmd;
            da.Fill(daTbl_2);

            try
            {
                idSensor = daTbl_2.Rows[0][0].ToString();
                bSource.DataSource = daTbl_2;
                dgvSensors.DataSource = bSource;
            }
            catch
            {

            }
        }

        private void btnDeleteSensor_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedSensor = null;
                // Get selected value from datagrid - https://stackoverflow.com/questions/7657137/datagridview-full-row-selection-but-get-single-cell-value
                if (dgvSensors.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgvSensors.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgvSensors.Rows[selectedrowindex];
                    selectedSensor = Convert.ToString(selectedRow.Cells["idSensor"].Value);
                }

                string connString = "server=127.0.0.1;user id=root;database=pdc";
                string Query = "DELETE FROM sensors WHERE idSensor='" + selectedSensor + "';";
                MySqlConnection MyConn2 = new MySqlConnection(connString);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Sensor ID" + selectedSensor + " Deleted");
                sensorList();
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbSensors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
