namespace ClinicWinForms
{
    partial class frmAppointments
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvAppointments;
        private ComboBox cboPatient;
        private ComboBox cboDoctor;
        private DateTimePicker dtpDate;
        private ComboBox cboStatus;
        private TextBox txtNotes;
        private ComboBox cboFilterStatus;
        private DateTimePicker dtpFilterDate;
        private Button btnAdd;
        private Button btnUpdateStatus;
        private Button btnDelete;
        private Button btnClear;
        private Button btnFilterDate;
        private Button btnFilterStatus;
        private Label lblPatient;
        private Label lblDoctor;
        private Label lblDate;
        private Label lblStatus;
        private Label lblNotes;
        private Label lblFilterStatus;
        private Label lblFilterDate;

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
            this.dgvAppointments = new DataGridView();
            this.cboPatient = new ComboBox();
            this.cboDoctor = new ComboBox();
            this.dtpDate = new DateTimePicker();
            this.cboStatus = new ComboBox();
            this.txtNotes = new TextBox();
            this.cboFilterStatus = new ComboBox();
            this.dtpFilterDate = new DateTimePicker();
            this.btnAdd = new Button();
            this.btnUpdateStatus = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            this.btnFilterDate = new Button();
            this.btnFilterStatus = new Button();
            this.lblPatient = new Label();
            this.lblDoctor = new Label();
            this.lblDate = new Label();
            this.lblStatus = new Label();
            this.lblNotes = new Label();
            this.lblFilterStatus = new Label();
            this.lblFilterDate = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();

            this.lblPatient.Text = "ผู้ป่วย:";
            this.lblPatient.Location = new Point(20, 20);
            this.lblPatient.AutoSize = true;

            this.cboPatient.Location = new Point(100, 17);
            this.cboPatient.Size = new Size(180, 23);
            this.cboPatient.DropDownStyle = ComboBoxStyle.DropDownList;

            this.lblDoctor.Text = "แพทย์:";
            this.lblDoctor.Location = new Point(300, 20);
            this.lblDoctor.AutoSize = true;

            this.cboDoctor.Location = new Point(380, 17);
            this.cboDoctor.Size = new Size(180, 23);
            this.cboDoctor.DropDownStyle = ComboBoxStyle.DropDownList;

            this.lblDate.Text = "วันที่นัด:";
            this.lblDate.Location = new Point(20, 50);
            this.lblDate.AutoSize = true;

            this.dtpDate.Location = new Point(100, 47);
            this.dtpDate.Size = new Size(180, 23);
            this.dtpDate.Format = DateTimePickerFormat.Custom;
            this.dtpDate.CustomFormat = "yyyy-MM-dd HH:mm";

            this.lblStatus.Text = "สถานะ:";
            this.lblStatus.Location = new Point(300, 50);
            this.lblStatus.AutoSize = true;

            this.cboStatus.Location = new Point(380, 47);
            this.cboStatus.Size = new Size(120, 23);
            this.cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            this.lblNotes.Text = "หมายเหตุ:";
            this.lblNotes.Location = new Point(20, 80);
            this.lblNotes.AutoSize = true;

            this.txtNotes.Location = new Point(100, 77);
            this.txtNotes.Size = new Size(460, 23);

            this.btnAdd.Text = "เพิ่ม";
            this.btnAdd.Location = new Point(20, 115);
            this.btnAdd.Size = new Size(80, 30);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnUpdateStatus.Text = "อัปเดตสถานะ";
            this.btnUpdateStatus.Location = new Point(110, 115);
            this.btnUpdateStatus.Size = new Size(90, 30);
            this.btnUpdateStatus.Click += new EventHandler(this.btnUpdateStatus_Click);

            this.btnDelete.Text = "ลบ";
            this.btnDelete.Location = new Point(210, 115);
            this.btnDelete.Size = new Size(80, 30);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClear.Text = "ล้าง";
            this.btnClear.Location = new Point(300, 115);
            this.btnClear.Size = new Size(80, 30);
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            this.lblFilterDate.Text = "กรองตามวันที่:";
            this.lblFilterDate.Location = new Point(20, 160);
            this.lblFilterDate.AutoSize = true;

            this.dtpFilterDate.Location = new Point(110, 157);
            this.dtpFilterDate.Size = new Size(130, 23);
            this.dtpFilterDate.Format = DateTimePickerFormat.Short;

            this.btnFilterDate.Text = "กรองวันที่";
            this.btnFilterDate.Location = new Point(250, 155);
            this.btnFilterDate.Size = new Size(80, 26);
            this.btnFilterDate.Click += new EventHandler(this.btnFilterDate_Click);

            this.lblFilterStatus.Text = "กรองตามสถานะ:";
            this.lblFilterStatus.Location = new Point(350, 160);
            this.lblFilterStatus.AutoSize = true;

            this.cboFilterStatus.Location = new Point(440, 157);
            this.cboFilterStatus.Size = new Size(120, 23);
            this.cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnFilterStatus.Text = "กรองสถานะ";
            this.btnFilterStatus.Location = new Point(570, 155);
            this.btnFilterStatus.Size = new Size(80, 26);
            this.btnFilterStatus.Click += new EventHandler(this.btnFilterStatus_Click);

            this.dgvAppointments.Location = new Point(20, 200);
            this.dgvAppointments.Size = new Size(640, 270);
            this.dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.CellClick += new DataGridViewCellEventHandler(this.dgvAppointments_CellClick);

            this.ClientSize = new Size(680, 490);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.cboPatient);
            this.Controls.Add(this.lblDoctor);
            this.Controls.Add(this.cboDoctor);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblFilterDate);
            this.Controls.Add(this.dtpFilterDate);
            this.Controls.Add(this.btnFilterDate);
            this.Controls.Add(this.lblFilterStatus);
            this.Controls.Add(this.cboFilterStatus);
            this.Controls.Add(this.btnFilterStatus);
            this.Controls.Add(this.dgvAppointments);
            this.Text = "จัดการนัดหมาย";
            this.Load += new EventHandler(this.frmAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
