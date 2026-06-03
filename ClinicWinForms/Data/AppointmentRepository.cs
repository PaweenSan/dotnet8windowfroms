using System.Data;
using Microsoft.Data.Sqlite;

namespace ClinicWinForms.Data
{
    public class AppointmentRepository
    {
        private readonly string _connString;

        public AppointmentRepository()
        {
            _connString = DatabaseConfig.GetConnectionString("appointments");
        }

        private void AttachDatabases(IDbConnection conn)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string patientsDb = Path.Combine(basePath, "Databases", "patients.db").Replace("\\", "/");
            string doctorsDb = Path.Combine(basePath, "Databases", "doctors.db").Replace("\\", "/");
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "PRAGMA database_list";
            var attached = new List<string>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    attached.Add(reader.GetString(1));
            }
            if (!attached.Contains("patients"))
            {
                cmd.CommandText = $"ATTACH DATABASE '{patientsDb}' AS patients;";
                cmd.ExecuteNonQuery();
            }
            if (!attached.Contains("doctors"))
            {
                cmd.CommandText = $"ATTACH DATABASE '{doctorsDb}' AS doctors;";
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ListAppointments()
        {
            DataTable dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            AttachDatabases(conn);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT a.AppointmentID, a.AppointmentDate, a.Status, a.Notes,
                       p.Name AS PatientName, d.Name AS DoctorName
                FROM Appointments a
                LEFT JOIN patients.Patients p ON a.PatientID = p.PatientID
                LEFT JOIN doctors.Doctors d ON a.DoctorID = d.DoctorID
                ORDER BY a.AppointmentDate DESC";
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public DataRow? GetAppointment(int id)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Appointments WHERE AppointmentID = @AppointmentID";
            cmd.Parameters.Add(new SqliteParameter("@AppointmentID", id));
            using var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public bool AddAppointment(int patientId, int doctorId, DateTime date, string status, string notes)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "INSERT INTO Appointments(PatientID, DoctorID, AppointmentDate, Status, Notes) VALUES(@PatientID, @DoctorID, @AppointmentDate, @Status, @Notes)";
                cmd.Parameters.Add(new SqliteParameter("@PatientID", patientId));
                cmd.Parameters.Add(new SqliteParameter("@DoctorID", doctorId));
                cmd.Parameters.Add(new SqliteParameter("@AppointmentDate", date.ToString("yyyy-MM-dd HH:mm:ss")));
                cmd.Parameters.Add(new SqliteParameter("@Status", status));
                cmd.Parameters.Add(new SqliteParameter("@Notes", notes));
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool UpdateAppointmentStatus(int id, string status)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "UPDATE Appointments SET Status = @Status WHERE AppointmentID = @AppointmentID";
                cmd.Parameters.Add(new SqliteParameter("@Status", status));
                cmd.Parameters.Add(new SqliteParameter("@AppointmentID", id));
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool DeleteAppointment(int id)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "DELETE FROM Appointments WHERE AppointmentID = @AppointmentID";
                cmd.Parameters.Add(new SqliteParameter("@AppointmentID", id));
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public DataTable GetAppointmentsByDate(DateTime date)
        {
            DataTable dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            AttachDatabases(conn);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT a.AppointmentID, a.AppointmentDate, a.Status, a.Notes,
                       p.Name AS PatientName, d.Name AS DoctorName
                FROM Appointments a
                LEFT JOIN patients.Patients p ON a.PatientID = p.PatientID
                LEFT JOIN doctors.Doctors d ON a.DoctorID = d.DoctorID
                WHERE date(a.AppointmentDate) = @Date
                ORDER BY a.AppointmentDate DESC";
            cmd.Parameters.Add(new SqliteParameter("@Date", date.ToString("yyyy-MM-dd")));
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public DataTable GetAppointmentsByStatus(string status)
        {
            DataTable dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            AttachDatabases(conn);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT a.AppointmentID, a.AppointmentDate, a.Status, a.Notes,
                       p.Name AS PatientName, d.Name AS DoctorName
                FROM Appointments a
                LEFT JOIN patients.Patients p ON a.PatientID = p.PatientID
                LEFT JOIN doctors.Doctors d ON a.DoctorID = d.DoctorID
                WHERE a.Status = @Status
                ORDER BY a.AppointmentDate DESC";
            cmd.Parameters.Add(new SqliteParameter("@Status", status));
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }
    }
}
