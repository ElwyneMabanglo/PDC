using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDC
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create DataGrid - https://stackoverflow.com/questions/11806007/simplest-way-of-programmatically-creating-a-datagridview-with-x-columns-by-y-row
            dgv.AutoGenerateColumns = false;;
            
            // Set Height of cell based on height input
            int height = 400 / Int32.Parse(txtHeight.Text.Trim());
            dgv.RowTemplate.MinimumHeight = height;

            // Create DataGrid
            for (int i = 1; i <= Int32.Parse(txtWidth.Text.Trim()) ; i++)
            {
                dgv.Columns.Add("col" + i, "column " + i);
                dgv.Columns[i - 1].FillWeight = 1;
            }
            for (int j = 0; j < Int32.Parse(txtHeight.Text.Trim()); j++)
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
    }
}
