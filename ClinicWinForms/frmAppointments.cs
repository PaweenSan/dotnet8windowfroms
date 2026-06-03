using System.Data;
using ClinicWinForms.Data;

namespace ClinicWinForms
{
    public partial class frmAppointments : Form
    {
        private readonly AppointmentRepository _repo;
        private readonly PatientRepository _patientRepo;
        private readonly DoctorRepository _doctorRepo;
        private int _selectedId;

        public frmAppointments()
        {
            _repo = new AppointmentRepository();
            _patientRepo = new PatientRepository();
            _doctorRepo = new DoctorRepository();
            InitializeComponent();
        }

        private void frmAppointments_Load(object sender, EventArgs e)
        {
            ShowData();
            LoadPatients();
            LoadDoctors();
            LoadStatus();
        }

        private void ShowData()
        {
            dgvAppointments.DataSource = _repo.ListAppointments();
            if (dgvAppointments.Columns.Count >= 6)
            {
                dgvAppointments.Columns[0].HeaderText = "รหัสนัด";
                dgvAppointments.Columns[1].HeaderText = "วันที่นัด";
                dgvAppointments.Columns[2].HeaderText = "สถานะ";
                dgvAppointments.Columns[3].HeaderText = "หมายเหตุ";
                dgvAppointments.Columns[4].HeaderText = "ผู้ป่วย";
                dgvAppointments.Columns[5].HeaderText = "แพทย์";
                dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void LoadPatients()
        {
            var dt = _patientRepo.ListPatients();
            cboPatient.DisplayMember = "Name";
            cboPatient.ValueMember = "PatientID";
            cboPatient.DataSource = dt;
        }

        private void LoadDoctors()
        {
            var dt = _doctorRepo.ListDoctors();
            cboDoctor.DisplayMember = "Name";
            cboDoctor.ValueMember = "DoctorID";
            cboDoctor.DataSource = dt;
        }

        private void LoadStatus()
        {
            cboStatus.Items.AddRange(new[] { "รอดำเนินการ", "ยืนยันแล้ว", "ยกเลิก" });
            cboStatus.SelectedIndex = 0;
            cboFilterStatus.Items.AddRange(new[] { "ทั้งหมด", "รอดำเนินการ", "ยืนยันแล้ว", "ยกเลิก" });
            cboFilterStatus.SelectedIndex = 0;
        }

        private void dgvAppointments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvAppointments.Rows[e.RowIndex];
            _selectedId = Convert.ToInt32(row.Cells[0].Value);
            dtpDate.Value = DateTime.Parse(row.Cells[1].Value?.ToString() ?? DateTime.Now.ToString());
            cboStatus.Text = row.Cells[2].Value?.ToString() ?? "รอดำเนินการ";
            txtNotes.Text = row.Cells[3].Value?.ToString() ?? "";
            var appt = _repo.GetAppointment(_selectedId);
            if (appt != null)
            {
                cboPatient.SelectedValue = Convert.ToInt32(appt["PatientID"]);
                cboDoctor.SelectedValue = Convert.ToInt32(appt["DoctorID"]);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboPatient.SelectedValue == null || cboDoctor.SelectedValue == null)
            {
                MessageBox.Show("กรุณาเลือกผู้ป่วยและแพทย์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int patientId = Convert.ToInt32(cboPatient.SelectedValue);
            int doctorId = Convert.ToInt32(cboDoctor.SelectedValue);
            if (MessageBox.Show("ต้องการเพิ่มนัดหมายหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (_repo.AddAppointment(patientId, doctorId, dtpDate.Value, cboStatus.Text, txtNotes.Text.Trim()))
            {
                MessageBox.Show("เพิ่มนัดหมายเรียบร้อย", "ยืนยัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                ShowData();
            }
            else
            {
                MessageBox.Show("เกิดข้อผิดพลาด", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("กรุณาเลือกนัดหมาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("ต้องการอัปเดตสถานะหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (_repo.UpdateAppointmentStatus(_selectedId, cboStatus.Text))
            {
                MessageBox.Show("อัปเดตสถานะเรียบร้อย", "ยืนยัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                ShowData();
            }
            else
            {
                MessageBox.Show("เกิดข้อผิดพลาด", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("กรุณาเลือกนัดหมาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("ต้องการลบข้อมูลหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_repo.DeleteAppointment(_selectedId))
                {
                    MessageBox.Show("ลบข้อมูลเรียบร้อย", "ยืนยัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    ShowData();
                }
                else
                {
                    MessageBox.Show("เกิดข้อผิดพลาด", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnFilterDate_Click(object sender, EventArgs e)
        {
            dgvAppointments.DataSource = _repo.GetAppointmentsByDate(dtpFilterDate.Value);
        }

        private void btnFilterStatus_Click(object sender, EventArgs e)
        {
            if (cboFilterStatus.SelectedIndex == 0)
            {
                ShowData();
            }
            else
            {
                dgvAppointments.DataSource = _repo.GetAppointmentsByStatus(cboFilterStatus.Text);
            }
        }

        private void ClearForm()
        {
            _selectedId = 0;
            txtNotes.Clear();
            cboStatus.SelectedIndex = 0;
            cboFilterStatus.SelectedIndex = 0;
            ShowData();
        }
    }
}
