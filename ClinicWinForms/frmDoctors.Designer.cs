namespace ClinicWinForms
{
    partial class frmDoctors
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvDoctors;
        private TextBox txtName;
        private TextBox txtSpecialty;
        private TextBox txtPhone;
        private TextBox txtDepartment;
        private ComboBox cboFilterSpecialty;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnFilter;
        private Label lblName;
        private Label lblSpecialty;
        private Label lblPhone;
        private Label lblDepartment;
        private Label lblFilter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvDoctors = new DataGridView();
            this.txtName = new TextBox();
            this.txtSpecialty = new TextBox();
            this.txtPhone = new TextBox();
            this.txtDepartment = new TextBox();
            this.cboFilterSpecialty = new ComboBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            this.btnFilter = new Button();
            this.lblName = new Label();
            this.lblSpecialty = new Label();
            this.lblPhone = new Label();
            this.lblDepartment = new Label();
            this.lblFilter = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).BeginInit();
            this.SuspendLayout();

            this.lblName.Text = "ชื่อ-สกุล:";
            this.lblName.Location = new Point(20, 20);
            this.lblName.AutoSize = true;

            this.txtName.Location = new Point(100, 17);
            this.txtName.Size = new Size(200, 23);

            this.lblSpecialty.Text = "ความเชี่ยวชาญ:";
            this.lblSpecialty.Location = new Point(20, 50);
            this.lblSpecialty.AutoSize = true;

            this.txtSpecialty.Location = new Point(100, 47);
            this.txtSpecialty.Size = new Size(150, 23);

            this.lblPhone.Text = "โทรศัพท์:";
            this.lblPhone.Location = new Point(20, 80);
            this.lblPhone.AutoSize = true;

            this.txtPhone.Location = new Point(100, 77);
            this.txtPhone.Size = new Size(150, 23);

            this.lblDepartment.Text = "แผนก:";
            this.lblDepartment.Location = new Point(20, 110);
            this.lblDepartment.AutoSize = true;

            this.txtDepartment.Location = new Point(100, 107);
            this.txtDepartment.Size = new Size(150, 23);

            this.btnAdd.Text = "เพิ่ม";
            this.btnAdd.Location = new Point(320, 17);
            this.btnAdd.Size = new Size(80, 30);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text = "แก้ไข";
            this.btnUpdate.Location = new Point(320, 52);
            this.btnUpdate.Size = new Size(80, 30);
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnDelete.Text = "ลบ";
            this.btnDelete.Location = new Point(320, 87);
            this.btnDelete.Size = new Size(80, 30);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClear.Text = "ล้าง";
            this.btnClear.Location = new Point(410, 17);
            this.btnClear.Size = new Size(80, 30);
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            this.lblFilter.Text = "กรองตามความเชี่ยวชาญ:";
            this.lblFilter.Location = new Point(20, 150);
            this.lblFilter.AutoSize = true;

            this.cboFilterSpecialty.Location = new Point(150, 147);
            this.cboFilterSpecialty.Size = new Size(150, 23);
            this.cboFilterSpecialty.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnFilter.Text = "กรอง";
            this.btnFilter.Location = new Point(310, 145);
            this.btnFilter.Size = new Size(80, 26);
            this.btnFilter.Click += new EventHandler(this.btnFilter_Click);

            this.dgvDoctors.Location = new Point(20, 190);
            this.dgvDoctors.Size = new Size(640, 280);
            this.dgvDoctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoctors.ReadOnly = true;
            this.dgvDoctors.AllowUserToAddRows = false;
            this.dgvDoctors.CellClick += new DataGridViewCellEventHandler(this.dgvDoctors_CellClick);

            this.ClientSize = new Size(680, 490);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblSpecialty);
            this.Controls.Add(this.txtSpecialty);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.cboFilterSpecialty);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dgvDoctors);
            this.Text = "จัดการแพทย์";
            this.Load += new EventHandler(this.frmDoctors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
