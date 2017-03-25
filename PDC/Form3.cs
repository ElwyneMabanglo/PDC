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

            // https://stackoverflow.com/questions/11806007/simplest-way-of-programmatically-creating-a-datagridview-with-x-columns-by-y-row
            dgv.AutoGenerateColumns = false;;
            


            int height = 400 / Int32.Parse(txtHeight.Text.Trim());
            dgv.RowTemplate.MinimumHeight = height;

            for (int i = 1; i <= 5; i++)
            {
                dgv.Columns.Add("col" + i, "column " + i);
                dgv.Columns[i - 1].FillWeight = 1;
            }
            for (int j = 0; j < 5; j++)
                dgv.Rows.Add();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
