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
    public partial class controlPanel : Form
    {
        public string idUser = null;

        public controlPanel(string value)
        {
            InitializeComponent();
            idUser = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            homeConsole x = new homeConsole(idUser);
            x.Show();
        }

        public void button5_Click(object sender, EventArgs e)
        {
            Form3 x = new Form3(idUser);
            x.Show();
        }

        private void controlPanel_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmFloorPlan x = new frmFloorPlan(Int32.Parse(idUser));
            x.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            floorPlan_v2 x = new floorPlan_v2(Int32.Parse(idUser));
            x.Show();
        }
    }
}
