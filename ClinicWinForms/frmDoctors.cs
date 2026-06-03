using System.Data;
using ClinicWinForms.Data;

namespace ClinicWinForms
{
    public partial class frmDoctors : Form
    {
        private readonly DoctorRepository _repo;
        private int _selectedId;

        public frmDoctors()
        {
            _repo = new DoctorRepository();
            InitializeComponent();
        }

        private void frmDoctors_Load(object sender, EventArgs e)
        {
            ShowData();
            dgvDoctors.Columns[0].HeaderText = "รหัส";
            dgvDoctors.Columns[1].HeaderText = "ชื่อ-สกุล";
            dgvDoctors.Columns[2].HeaderText = "ความเชี่ยวชาญ";
            dgvDoctors.Columns[3].HeaderText = "โทรศัพท์";
            dgvDoctors.Columns[4].HeaderText = "แผนก";
            dgvDoctors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cboFilterSpecialty.Items.Add("ทั้งหมด");
            var dt = _repo.ListDoctors();
            var specialties = dt.AsEnumerable()
                .Select(r => r.Field<string>("Specialty"))
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct()
                .ToList();
            foreach (var s in specialties) cboFilterSpecialty.Items.Add(s);
            cboFilterSpecialty.SelectedIndex = 0;
        }

        private void ShowData()
        {
            dgvDoctors.DataSource = _repo.ListDoctors();
        }

        private void dgvDoctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvDoctors.Rows[e.RowIndex];
            _selectedId = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value?.ToString() ?? "";
            txtSpecialty.Text = row.Cells[2].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells[3].Value?.ToString() ?? "";
            txtDepartment.Text = row.Cells[4].Value?.ToString() ?? "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            if (MessageBox.Show("ต้องการเพิ่มข้อมูลหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (_repo.AddDoctor(txtName.Text.Trim(), txtSpecialty.Text.Trim(), txtPhone.Text.Trim(), txtDepartment.Text.Trim()))
            {
                MessageBox.Show("เพิ่มแพทย์เรียบร้อย", "ยืนยัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                ShowData();
            }
            else
            {
                MessageBox.Show("เกิดข้อผิดพลาด", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("กรุณาเลือกแพทย์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;
            if (MessageBox.Show("ต้องการแก้ไขข้อมูลหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (_repo.UpdateDoctor(_selectedId, txtName.Text.Trim(), txtSpecialty.Text.Trim(), txtPhone.Text.Trim(), txtDepartment.Text.Trim()))
            {
                MessageBox.Show("แก้ไขข้อมูลเรียบร้อย", "ยืนยัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("กรุณาเลือกแพทย์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("ต้องการลบข้อมูลหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_repo.DeleteDoctor(_selectedId))
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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (cboFilterSpecialty.SelectedIndex == 0)
            {
                ShowData();
            }
            else
            {
                dgvDoctors.DataSource = _repo.SearchDoctorsBySpecialty(cboFilterSpecialty.SelectedItem?.ToString() ?? "");
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("กรุณากรอกชื่อ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtSpecialty.Clear();
            txtPhone.Clear();
            txtDepartment.Clear();
            _selectedId = 0;
            cboFilterSpecialty.SelectedIndex = 0;
            ShowData();
        }
    }
}
