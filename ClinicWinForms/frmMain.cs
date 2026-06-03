using ClinicWinForms.Data;

namespace ClinicWinForms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                DatabaseInitializer.Initialize();
                btnStatus.Text = "เชื่อมต่อสำเร็จ";
                btnStatus.BackColor = Color.LimeGreen;
                btnStatus.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                btnStatus.Text = "เชื่อมต่อล้มเหลว";
                btnStatus.BackColor = Color.Red;
                btnStatus.ForeColor = Color.White;
            }
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            LoadForm(new frmPatients());
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            LoadForm(new frmDoctors());
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            LoadForm(new frmAppointments());
        }

        private void LoadForm(Form form)
        {
            pnlContent.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            pnlContent.Controls.Add(form);
            form.Show();
        }
    }
}
