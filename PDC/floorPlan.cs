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
    public partial class frmFloorPlan : Form
    {
        public int idUser = 0;
        public int idHome = 0;
        public int idRoom = 0;
        public int idSensor = 0;
        public int x = 0;
        public int y = 0;


        private static room[,] roomList;

        public frmFloorPlan(int value)
        {
            InitializeComponent();
            idUser = value;
            loadHomesIntoDropDown();
        }

        private void newFloorPlan_Click(object sender, EventArgs e)
        {
            btnNewFloorPlan.Enabled = false;
            btnSaveFloorPlan.Enabled = true;

            // Create DataGrid - https://stackoverflow.com/questions/11806007/simplest-way-of-programmatically-creating-a-datagridview-with-x-columns-by-y-row
            dgv.AutoGenerateColumns = false;

            // Set Height of cell based on height input
            int height = 500 / y;
            dgv.RowTemplate.MinimumHeight = height;

            // Create DataGrid
            for (int i = 1; i <= x; i++)
            {
                dgv.Columns.Add("col" + i, "column " + i);
                dgv.Columns[i - 1].FillWeight = 1;
            }
            for (int j = 0; j < y; j++)
                dgv.Rows.Add();

            roomList = new room[x, y];

            // Create Objects and add to Objects list
            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                for (int col = 0; col < dgv.Rows[row].Cells.Count; col++)
                {
                    room newRoom = new room(0, idHome, "", 0, 0, 0, 0, row, col);
                    roomList[col, row] = newRoom;
                }
            }

            btnSaveFloorPlan.Enabled = false;
            btnUpdate.Enabled = true;

            try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection con = new MySqlConnection(connString);
                string query = "INSERT INTO  rooms (idHouse,roomType,north,east,south,west,x,y) VALUES (@idHouse,@roomType,@north,@east,@south,@west,@x,@y)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Rows.Count; col++)
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

        public void loadHomesIntoDropDown()
        {
            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT idHome,homeName FROM home WHERE home.idUser = " + idUser;
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;
            DataTable daTbl = new DataTable();

            da.Fill(daTbl);
            cmboHome.DataSource = daTbl;
            cmboHome.DisplayMember = "homeName";
            cmboHome.ValueMember = "idHome";
            cmboHome.Enabled = true;
            cmboHome.SelectedIndex = 0;

            idHome = Int32.Parse(cmboHome.SelectedValue.ToString());
            disbleEnableHomeDrop();
            getDiemensions();

        }

        public void disbleEnableHomeDrop()
        {
            try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT * FROM rooms WHERE rooms.idHouse = " + cmboHome.SelectedValue.ToString();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlCmd;
                DataTable daTbl = new DataTable();
                conn.Close();
                da.Fill(daTbl);
                if (daTbl.Rows.Count > 1)
                {
                    btnNewFloorPlan.Enabled = false;
                    btnLoadFloorPlan.Enabled = true;
                }
                else
                {
                    btnNewFloorPlan.Enabled = true;
                    btnLoadFloorPlan.Enabled = false;
                }
            }
            catch
            {

            }
        }

        public void getDiemensions()
        {
            try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT x,y FROM home WHERE home.idhome = " + idHome;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlCmd;
                DataTable daTbl = new DataTable();
                conn.Close();
                da.Fill(daTbl);
                x = Int32.Parse(daTbl.Rows[0][0].ToString());
                y = Int32.Parse(daTbl.Rows[0][1].ToString());
            }
            catch
            {

            }
        }

        private void btnLoadFloorPlan_Click(object sender, EventArgs e)
        {
            btnLoadFloorPlan.Enabled = false;
            btnUpdate.Enabled = true;

            //MessageBox.Show(cmboHome.SelectedValue.ToString());
            dgv.AutoGenerateColumns = false;

            // Set Height of cell based on height input
            int height = 500 / y;
            dgv.RowTemplate.MinimumHeight = height;

            // Create DataGrid
            for (int i = 1; i <= x; i++)
            {
                dgv.Columns.Add("col" + i, "column " + i);
                dgv.Columns[i - 1].FillWeight = 1;
            }
            for (int j = 0; j < y; j++)
                dgv.Rows.Add();

            roomList = new room[x, y];

            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT * FROM rooms WHERE rooms.idHouse = " + idHome;

            conn.Open();
            sqlCmd.ExecuteNonQuery();

            MySqlDataReader reader = sqlCmd.ExecuteReader();

            while (reader.Read())
            {
                int idRoom = reader.GetInt32("idRoom");
                int idHouse = reader.GetInt32("idHouse");
                string roomType = reader.GetString("roomType");
                int north = reader.GetInt32("north");
                int east = reader.GetInt32("east");
                int south = reader.GetInt32("south");
                int west = reader.GetInt32("west");
                int x = reader.GetInt32("x");
                int y = reader.GetInt32("y");

                room newRoom = new room(idRoom, idHouse, roomType, north, east, south, west, x, y);

                roomList[x, y] = newRoom;
                dgv.Rows[y].Cells[x].Value = roomType;

            }

            reader.Close();
        }

        private void cmboHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                idHome = Int32.Parse(cmboHome.SelectedValue.ToString());
                disbleEnableHomeDrop();
                getDiemensions();
            }
            catch
            {

            }
        }

        private void btnAddRoomType_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Cell valu using X,Y - https://stackoverflow.com/questions/4351603/get-selected-value-from-combo-box-in-c-sharp-wpf
                roomList[x, y].RoomType = cmboRoomType.SelectedItem.ToString();
                dgv.Rows[y].Cells[x].Value = cmboRoomType.SelectedItem.ToString();
            }
            catch
            {

            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get Cell Coords
            Int32 selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                for (int i = 0; i < selectedCellCount; i++)
                {
                    // Get X,Y of Selected Cell
                    txtX.Text = dgv.SelectedCells[i].ColumnIndex.ToString();
                    txtY.Text = dgv.SelectedCells[i].RowIndex.ToString();
                    x = Int32.Parse(dgv.SelectedCells[i].ColumnIndex.ToString());
                    y = Int32.Parse(dgv.SelectedCells[i].RowIndex.ToString());
                }
            }



            string connString = "server=127.0.0.1;user id=root;database=pdc";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = sqlCmd;

            sqlCmd.CommandText = "SELECT * FROM rooms WHERE idHouse = @idHouse AND x = @x AND y = @y";
            sqlCmd.Parameters.Add(new MySqlParameter("@idHouse", idHome));
            sqlCmd.Parameters.Add(new MySqlParameter("@x", x));
            sqlCmd.Parameters.Add(new MySqlParameter("@y", y));

            DataTable daTbl = new DataTable();
            BindingSource bSource = new BindingSource();

            da.SelectCommand = sqlCmd;
            da.Fill(daTbl);


            //MessageBox.Show(daTbl.Rows[0][0].ToString());

            idRoom = Int32.Parse(daTbl.Rows[0][0].ToString());

            sensorList();

            for (int i = 0; i < chkDoor.Items.Count; i++)
            {
                // For every other item in the list, set as checked.
                if (i == 0)
                {
                    if (daTbl.Rows[0][3].ToString() == "1")
                    {
                        chkDoor.SetItemChecked(i, true);
                    }
                    else
                    {
                        chkDoor.SetItemChecked(i, false);
                    }
                }

                if (i == 1)
                {
                    if (daTbl.Rows[0][4].ToString() == "1")
                    {
                        chkDoor.SetItemChecked(i, true);
                    }
                    else
                    {
                        chkDoor.SetItemChecked(i, false);
                    }
                }

                if (i == 2)
                {
                    if (daTbl.Rows[0][5].ToString() == "1")
                    {
                        chkDoor.SetItemChecked(i, true);
                    }
                    else
                    {
                        chkDoor.SetItemChecked(i, false);
                    }
                }

                if (i == 3)
                {
                    if (daTbl.Rows[0][6].ToString() == "1")
                    {
                        chkDoor.SetItemChecked(i, true);
                    }
                    else
                    {
                        chkDoor.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection con = new MySqlConnection(connString);
                string query = "UPDATE rooms SET roomType=@roomType,north=@north,east=@east,south=@south,west=@west,x=@x,y=@y WHERE idRoom=@idRoom";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Rows.Count; col++)
                    {

                        cmd.Parameters.AddWithValue("@roomType", roomList[row, col].RoomType);
                        cmd.Parameters.AddWithValue("@north", roomList[row, col].North);
                        cmd.Parameters.AddWithValue("@east", roomList[row, col].East);
                        cmd.Parameters.AddWithValue("@south", roomList[row, col].South);
                        cmd.Parameters.AddWithValue("@west", roomList[row, col].West);
                        cmd.Parameters.AddWithValue("@x", roomList[row, col].X);
                        cmd.Parameters.AddWithValue("@y", roomList[row, col].Y);
                        cmd.Parameters.AddWithValue("@idRoom", roomList[row, col].IdRoom);
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

        private void btnSaveFloorPlan_Click(object sender, EventArgs e)
        {
            btnSaveFloorPlan.Enabled = false;
            btnUpdate.Enabled = true;

            try
            {
                string connString = "server=127.0.0.1;user id=root;database=pdc";
                MySqlConnection con = new MySqlConnection(connString);
                string query = "INSERT INTO  rooms (idHouse,roomType,north,east,south,west,x,y) VALUES (@idHouse,@roomType,@north,@east,@south,@west,@x,@y)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Rows.Count; col++)
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

        private void btnAddDoor_Click(object sender, EventArgs e)
        {
            if ((string)chkDoor.Items[0] == "North" && chkDoor.GetItemCheckState(0) == CheckState.Checked)
            {
                roomList[x, y].North = 1;
            }
            else 
            {
                roomList[x, y].North = 0;
            }

            if ((string)chkDoor.Items[1] == "East" && chkDoor.GetItemCheckState(1) == CheckState.Checked)
            {
                roomList[x, y].East = 1;
            }
            else
            {
                roomList[x, y].East = 0;
            }

            if ((string)chkDoor.Items[2] == "South" && chkDoor.GetItemCheckState(2) == CheckState.Checked)
            {
                roomList[x, y].South = 1;
            }
            else
            {
                roomList[x, y].South = 0;
            }

            if ((string)chkDoor.Items[3] == "West" && chkDoor.GetItemCheckState(3) == CheckState.Checked)
            {
                roomList[x, y].West = 1;
            }
            else
            {
                roomList[x, y].West = 0;
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
                idSensor = Int32.Parse(daTbl_2.Rows[0][0].ToString());
                bSource.DataSource = daTbl_2;
                dgvSensors.DataSource = bSource;
            }
            catch
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
    }
}