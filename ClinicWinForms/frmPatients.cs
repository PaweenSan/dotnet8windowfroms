using System.Data;
using ClinicWinForms.Data;

namespace ClinicWinForms
{
    public partial class frmPatients : Form
    {
        private readonly PatientRepository _repo;
        private int _selectedId;

        public frmPatients()
        {
            _repo = new PatientRepository();
            InitializeComponent();
        }

        private void frmPatients_Load(object sender, EventArgs e)
        {
            ShowData();
            dgvPatients.Columns[0].HeaderText = "รหัส";
            dgvPatients.Columns[1].HeaderText = "ชื่อ-สกุล";
            dgvPatients.Columns[2].HeaderText = "อายุ";
            dgvPatients.Columns[3].HeaderText = "โทรศัพท์";
            dgvPatients.Columns[4].HeaderText = "ที่อยู่";
            dgvPatients.Columns[5].HeaderText = "วันที่บันทึก";
            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ShowData()
        {
            dgvPatients.DataSource = _repo.ListPatients();
        }

        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPatients.Rows[e.RowIndex];
            _selectedId = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value?.ToString() ?? "";
            txtAge.Text = row.Cells[2].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells[3].Value?.ToString() ?? "";
            txtAddress.Text = row.Cells[4].Value?.ToString() ?? "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            if (_repo.IsDuplicate(txtName.Text.Trim()))
            {
                MessageBox.Show("ชื่อผู้ป่วยนี้มีอยู่แล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("ต้องการเพิ่มข้อมูลหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (_repo.AddPatient(txtName.Text.Trim(), Convert.ToInt32(txtAge.Text), txtPhone.Text.Trim(), txtAddress.Text.Trim()))
            {
                MessageBox.Show("เพิ่มผู้ป่วยเรียบร้อย", "ยืนยัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("กรุณาเลือกผู้ป่วย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;
            if (MessageBox.Show("ต้องการแก้ไขข้อมูลหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (_repo.UpdatePatient(_selectedId, txtName.Text.Trim(), Convert.ToInt32(txtAge.Text), txtPhone.Text.Trim(), txtAddress.Text.Trim()))
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
                MessageBox.Show("กรุณาเลือกผู้ป่วย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("ต้องการลบข้อมูลหรือไม่", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_repo.DeletePatient(_selectedId))
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                ShowData();
                return;
            }
            dgvPatients.DataSource = _repo.SearchPatients(txtSearch.Text.Trim());
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("กรุณากรอกชื่อ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (!int.TryParse(txtAge.Text, out _))
            {
                MessageBox.Show("กรุณากรอกอายุเป็นตัวเลข", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtAge.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtSearch.Clear();
            _selectedId = 0;
            dgvPatients.DataSource = _repo.ListPatients();
        }
    }
}
