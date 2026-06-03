namespace ClinicWinForms
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlMenu;
        private Panel pnlContent;
        private Label lblTitle;
        private Label lblStatus;
        private Button btnStatus;
        private Button btnPatients;
        private Button btnDoctors;
        private Button btnAppointments;

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
            this.pnlMenu = new Panel();
            this.pnlContent = new Panel();
            this.lblTitle = new Label();
            this.lblStatus = new Label();
            this.btnStatus = new Button();
            this.btnPatients = new Button();
            this.btnDoctors = new Button();
            this.btnAppointments = new Button();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();

            this.pnlMenu.BackColor = Color.FromArgb(0, 120, 215);
            this.pnlMenu.Dock = DockStyle.Left;
            this.pnlMenu.Width = 200;

            this.lblTitle.Text = "ระบบจัดการคลินิก";
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(10, 20);

            this.btnPatients.Text = "จัดการผู้ป่วย";
            this.btnPatients.Location = new Point(10, 70);
            this.btnPatients.Size = new Size(180, 40);
            this.btnPatients.Click += new EventHandler(this.btnPatients_Click);

            this.btnDoctors.Text = "จัดการแพทย์";
            this.btnDoctors.Location = new Point(10, 120);
            this.btnDoctors.Size = new Size(180, 40);
            this.btnDoctors.Click += new EventHandler(this.btnDoctors_Click);

            this.btnAppointments.Text = "จัดการนัดหมาย";
            this.btnAppointments.Location = new Point(10, 170);
            this.btnAppointments.Size = new Size(180, 40);
            this.btnAppointments.Click += new EventHandler(this.btnAppointments_Click);

            this.lblStatus.Text = "สถานะ:";
            this.lblStatus.Location = new Point(10, 500);
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = Color.White;

            this.btnStatus.Text = "กำลังเชื่อมต่อ...";
            this.btnStatus.Location = new Point(60, 495);
            this.btnStatus.Size = new Size(120, 28);
            this.btnStatus.FlatStyle = FlatStyle.Flat;
            this.btnStatus.BackColor = Color.Yellow;
            this.btnStatus.ForeColor = Color.Black;
            this.btnStatus.Enabled = false;

            this.pnlMenu.Controls.Add(this.lblTitle);
            this.pnlMenu.Controls.Add(this.btnPatients);
            this.pnlMenu.Controls.Add(this.btnDoctors);
            this.pnlMenu.Controls.Add(this.btnAppointments);
            this.pnlMenu.Controls.Add(this.lblStatus);
            this.pnlMenu.Controls.Add(this.btnStatus);

            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.BackColor = Color.White;

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlMenu);
            this.Text = "ระบบจัดการคลินิก";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
