namespace ClinicWinForms
{
    partial class frmPatients
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvPatients;
        private TextBox txtName;
        private TextBox txtAge;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnSearch;
        private Label lblName;
        private Label lblAge;
        private Label lblPhone;
        private Label lblAddress;
        private Label lblSearch;

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
            this.dgvPatients = new DataGridView();
            this.txtName = new TextBox();
            this.txtAge = new TextBox();
            this.txtPhone = new TextBox();
            this.txtAddress = new TextBox();
            this.txtSearch = new TextBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            this.btnSearch = new Button();
            this.lblName = new Label();
            this.lblAge = new Label();
            this.lblPhone = new Label();
            this.lblAddress = new Label();
            this.lblSearch = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
            this.SuspendLayout();

            this.lblName.Text = "ชื่อ-สกุล:";
            this.lblName.Location = new Point(20, 20);
            this.lblName.AutoSize = true;

            this.txtName.Location = new Point(100, 17);
            this.txtName.Size = new Size(200, 23);

            this.lblAge.Text = "อายุ:";
            this.lblAge.Location = new Point(20, 50);
            this.lblAge.AutoSize = true;

            this.txtAge.Location = new Point(100, 47);
            this.txtAge.Size = new Size(80, 23);

            this.lblPhone.Text = "โทรศัพท์:";
            this.lblPhone.Location = new Point(20, 80);
            this.lblPhone.AutoSize = true;

            this.txtPhone.Location = new Point(100, 77);
            this.txtPhone.Size = new Size(150, 23);

            this.lblAddress.Text = "ที่อยู่:";
            this.lblAddress.Location = new Point(20, 110);
            this.lblAddress.AutoSize = true;

            this.txtAddress.Location = new Point(100, 107);
            this.txtAddress.Size = new Size(250, 23);

            this.btnAdd.Text = "เพิ่ม";
            this.btnAdd.Location = new Point(370, 17);
            this.btnAdd.Size = new Size(80, 30);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text = "แก้ไข";
            this.btnUpdate.Location = new Point(370, 52);
            this.btnUpdate.Size = new Size(80, 30);
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnDelete.Text = "ลบ";
            this.btnDelete.Location = new Point(370, 87);
            this.btnDelete.Size = new Size(80, 30);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClear.Text = "ล้าง";
            this.btnClear.Location = new Point(460, 17);
            this.btnClear.Size = new Size(80, 30);
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            this.lblSearch.Text = "ค้นหา:";
            this.lblSearch.Location = new Point(20, 150);
            this.lblSearch.AutoSize = true;

            this.txtSearch.Location = new Point(100, 147);
            this.txtSearch.Size = new Size(200, 23);

            this.btnSearch.Text = "ค้นหา";
            this.btnSearch.Location = new Point(310, 145);
            this.btnSearch.Size = new Size(80, 26);
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            this.dgvPatients.Location = new Point(20, 190);
            this.dgvPatients.Size = new Size(640, 280);
            this.dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatients.ReadOnly = true;
            this.dgvPatients.AllowUserToAddRows = false;
            this.dgvPatients.CellClick += new DataGridViewCellEventHandler(this.dgvPatients_CellClick);

            this.ClientSize = new Size(680, 490);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvPatients);
            this.Text = "จัดการผู้ป่วย";
            this.Load += new EventHandler(this.frmPatients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
