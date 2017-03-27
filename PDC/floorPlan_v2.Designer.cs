namespace PDC
{
    partial class floorPlan_v2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmboHome = new System.Windows.Forms.ComboBox();
            this.btnLoadFloorPlan = new System.Windows.Forms.Button();
            this.btnNewFloorPlan = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnSaveFloorPlan = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmboRoomType = new System.Windows.Forms.ComboBox();
            this.btnAddRoomType = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddDoor = new System.Windows.Forms.Button();
            this.chkDoor = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRoomID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmboHome);
            this.groupBox4.Controls.Add(this.btnLoadFloorPlan);
            this.groupBox4.Controls.Add(this.btnNewFloorPlan);
            this.groupBox4.Location = new System.Drawing.Point(549, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(186, 79);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select Home";
            // 
            // cmboHome
            // 
            this.cmboHome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboHome.FormattingEnabled = true;
            this.cmboHome.Items.AddRange(new object[] {
            "Toilet",
            "Living Room",
            "Kitchen",
            "Bedroom",
            "Store Room",
            "Guest Room"});
            this.cmboHome.Location = new System.Drawing.Point(6, 19);
            this.cmboHome.Name = "cmboHome";
            this.cmboHome.Size = new System.Drawing.Size(174, 21);
            this.cmboHome.TabIndex = 36;
            this.cmboHome.SelectedIndexChanged += new System.EventHandler(this.cmboHome_SelectedIndexChanged);
            // 
            // btnLoadFloorPlan
            // 
            this.btnLoadFloorPlan.Location = new System.Drawing.Point(92, 46);
            this.btnLoadFloorPlan.Name = "btnLoadFloorPlan";
            this.btnLoadFloorPlan.Size = new System.Drawing.Size(88, 23);
            this.btnLoadFloorPlan.TabIndex = 26;
            this.btnLoadFloorPlan.Text = "Load Floor";
            this.btnLoadFloorPlan.UseVisualStyleBackColor = true;
            this.btnLoadFloorPlan.Click += new System.EventHandler(this.btnLoadFloorPlan_Click);
            // 
            // btnNewFloorPlan
            // 
            this.btnNewFloorPlan.Location = new System.Drawing.Point(6, 46);
            this.btnNewFloorPlan.Name = "btnNewFloorPlan";
            this.btnNewFloorPlan.Size = new System.Drawing.Size(81, 24);
            this.btnNewFloorPlan.TabIndex = 1;
            this.btnNewFloorPlan.Text = "New Floor";
            this.btnNewFloorPlan.UseVisualStyleBackColor = true;
            this.btnNewFloorPlan.Click += new System.EventHandler(this.btnNewFloorPlan_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.ColumnHeadersVisible = false;
            this.dgv.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.RowTemplate.Height = 20;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(500, 500);
            this.dgv.TabIndex = 37;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick_1);
            // 
            // btnSaveFloorPlan
            // 
            this.btnSaveFloorPlan.Enabled = false;
            this.btnSaveFloorPlan.Location = new System.Drawing.Point(561, 356);
            this.btnSaveFloorPlan.Name = "btnSaveFloorPlan";
            this.btnSaveFloorPlan.Size = new System.Drawing.Size(174, 23);
            this.btnSaveFloorPlan.TabIndex = 45;
            this.btnSaveFloorPlan.Text = "Save";
            this.btnSaveFloorPlan.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(561, 327);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(173, 23);
            this.btnUpdate.TabIndex = 46;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmboRoomType);
            this.groupBox3.Controls.Add(this.btnAddRoomType);
            this.groupBox3.Location = new System.Drawing.Point(549, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 51);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Add Room";
            // 
            // cmboRoomType
            // 
            this.cmboRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboRoomType.FormattingEnabled = true;
            this.cmboRoomType.Items.AddRange(new object[] {
            "Toilet",
            "Living Room",
            "Kitchen",
            "Bedroom",
            "Store Room",
            "Guest Room"});
            this.cmboRoomType.Location = new System.Drawing.Point(6, 19);
            this.cmboRoomType.Name = "cmboRoomType";
            this.cmboRoomType.Size = new System.Drawing.Size(146, 21);
            this.cmboRoomType.TabIndex = 19;
            // 
            // btnAddRoomType
            // 
            this.btnAddRoomType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRoomType.Location = new System.Drawing.Point(154, 18);
            this.btnAddRoomType.Name = "btnAddRoomType";
            this.btnAddRoomType.Size = new System.Drawing.Size(26, 23);
            this.btnAddRoomType.TabIndex = 25;
            this.btnAddRoomType.Text = "+";
            this.btnAddRoomType.UseVisualStyleBackColor = true;
            this.btnAddRoomType.Click += new System.EventHandler(this.btnAddRoomType_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddDoor);
            this.groupBox2.Controls.Add(this.chkDoor);
            this.groupBox2.Location = new System.Drawing.Point(549, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 120);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Door";
            // 
            // btnAddDoor
            // 
            this.btnAddDoor.AllowDrop = true;
            this.btnAddDoor.Location = new System.Drawing.Point(7, 89);
            this.btnAddDoor.Name = "btnAddDoor";
            this.btnAddDoor.Size = new System.Drawing.Size(173, 23);
            this.btnAddDoor.TabIndex = 1;
            this.btnAddDoor.Text = "Add Door";
            this.btnAddDoor.UseVisualStyleBackColor = true;
            // 
            // chkDoor
            // 
            this.chkDoor.FormattingEnabled = true;
            this.chkDoor.Items.AddRange(new object[] {
            "North",
            "East",
            "South",
            "West"});
            this.chkDoor.Location = new System.Drawing.Point(6, 19);
            this.chkDoor.Name = "chkDoor";
            this.chkDoor.Size = new System.Drawing.Size(174, 64);
            this.chkDoor.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(561, 432);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Room ID";
            // 
            // txtRoomID
            // 
            this.txtRoomID.Location = new System.Drawing.Point(616, 429);
            this.txtRoomID.Name = "txtRoomID";
            this.txtRoomID.Size = new System.Drawing.Size(100, 20);
            this.txtRoomID.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(671, 403);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(623, 403);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "X:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(558, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Coordinates";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(694, 400);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(20, 20);
            this.txtY.TabIndex = 50;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(646, 400);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(20, 20);
            this.txtX.TabIndex = 49;
            // 
            // floorPlan_v2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 633);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRoomID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSaveFloorPlan);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.groupBox4);
            this.Name = "floorPlan_v2";
            this.Text = "floorPlan_v2";
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmboHome;
        private System.Windows.Forms.Button btnLoadFloorPlan;
        private System.Windows.Forms.Button btnNewFloorPlan;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnSaveFloorPlan;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmboRoomType;
        private System.Windows.Forms.Button btnAddRoomType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddDoor;
        private System.Windows.Forms.CheckedListBox chkDoor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRoomID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
    }
}