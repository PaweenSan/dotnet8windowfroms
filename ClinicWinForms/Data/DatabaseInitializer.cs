using Microsoft.Data.Sqlite;

namespace ClinicWinForms.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            Directory.CreateDirectory(Path.Combine(basePath, "Databases"));

            InitPatientsDb(basePath);
            InitDoctorsDb(basePath);
            InitAppointmentsDb(basePath);
        }

        private static void InitPatientsDb(string basePath)
        {
            string dbPath = Path.Combine(basePath, "Databases", "patients.db");
            using var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Patients (
                    PatientID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Age INTEGER,
                    Phone TEXT,
                    Address TEXT,
                    CreatedAt TEXT DEFAULT (datetime('now'))
                );";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT COUNT(*) FROM Patients";
            long count = (long)cmd.ExecuteScalar();
            if (count == 0)
            {
                cmd.CommandText = @"
                    INSERT INTO Patients (Name, Age, Phone, Address) VALUES
                    ('สมชาย ใจดี', 30, '0812345678', 'กรุงเทพฯ'),
                    ('สมหญิง รักเรียน', 25, '0823456789', 'นนทบุรี'),
                    ('มานี มานะ', 40, '0834567890', 'ปทุมธานี'),
                    ('ประเสริฐ สุขใจ', 55, '0845678901', 'สมุทรปราการ'),
                    ('วิไลวรรณ งามตา', 35, '0856789012', 'กรุงเทพฯ');";
                cmd.ExecuteNonQuery();
            }
        }

        private static void InitDoctorsDb(string basePath)
        {
            string dbPath = Path.Combine(basePath, "Databases", "doctors.db");
            using var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Doctors (
                    DoctorID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Specialty TEXT,
                    Phone TEXT,
                    Department TEXT
                );";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT COUNT(*) FROM Doctors";
            long count = (long)cmd.ExecuteScalar();
            if (count == 0)
            {
                cmd.CommandText = @"
                    INSERT INTO Doctors (Name, Specialty, Phone, Department) VALUES
                    ('นพ.สมศักดิ์ เก่งกาจ', 'อายุรกรรม', '021111111', 'อายุรกรรม'),
                    ('พญ.ประภาส รักษาดี', 'กุมารเวชกรรม', '022222222', 'กุมารเวชกรรม'),
                    ('นพ.วิชัย ใจเย็น', 'ศัลยกรรม', '023333333', 'ศัลยกรรม'),
                    ('พญ.รัตนา สว่างไสว', 'สูติ-นรีเวช', '024444444', 'สูติ-นรีเวช'),
                    ('นพ.อนันต์ มีสุข', 'จักษุวิทยา', '025555555', 'จักษุวิทยา');";
                cmd.ExecuteNonQuery();
            }
        }

        private static void InitAppointmentsDb(string basePath)
        {
            string dbPath = Path.Combine(basePath, "Databases", "appointments.db");
            using var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Appointments (
                    AppointmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                    PatientID INTEGER NOT NULL,
                    DoctorID INTEGER NOT NULL,
                    AppointmentDate TEXT NOT NULL,
                    Status TEXT DEFAULT 'รอดำเนินการ',
                    Notes TEXT
                );";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT COUNT(*) FROM Appointments";
            long count = (long)cmd.ExecuteScalar();
            if (count == 0)
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                cmd.CommandText = $@"
                    INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Status, Notes) VALUES
                    (1, 1, '{today} 09:00:00', 'รอดำเนินการ', 'ตรวจสุขภาพประจำปี'),
                    (2, 2, '{today} 10:30:00', 'ยืนยันแล้ว', 'ตรวจพัฒนาการเด็ก'),
                    (3, 1, '{tomorrow} 14:00:00', 'รอดำเนินการ', 'ปวดท้องเรื้อรัง');";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
