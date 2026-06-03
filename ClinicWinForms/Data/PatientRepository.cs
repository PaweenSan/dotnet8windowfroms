using System.Data;
using Microsoft.Data.Sqlite;

namespace ClinicWinForms.Data
{
    public class PatientRepository
    {
        private readonly string _connString;

        public PatientRepository()
        {
            _connString = DatabaseConfig.GetConnectionString("patients");
        }

        public DataTable ListPatients()
        {
            DataTable dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Patients ORDER BY PatientID DESC";
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public DataRow? GetPatient(int id)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Patients WHERE PatientID = @PatientID";
            cmd.Parameters.Add(new SqliteParameter("@PatientID", id));
            using var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public bool AddPatient(string name, int age, string phone, string address)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "INSERT INTO Patients(Name, Age, Phone, Address, CreatedAt) VALUES(@Name, @Age, @Phone, @Address, datetime('now'))";
                cmd.Parameters.Add(new SqliteParameter("@Name", name));
                cmd.Parameters.Add(new SqliteParameter("@Age", age));
                cmd.Parameters.Add(new SqliteParameter("@Phone", phone));
                cmd.Parameters.Add(new SqliteParameter("@Address", address));
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

        public bool UpdatePatient(int id, string name, int age, string phone, string address)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "UPDATE Patients SET Name = @Name, Age = @Age, Phone = @Phone, Address = @Address WHERE PatientID = @PatientID";
                cmd.Parameters.Add(new SqliteParameter("@Name", name));
                cmd.Parameters.Add(new SqliteParameter("@Age", age));
                cmd.Parameters.Add(new SqliteParameter("@Phone", phone));
                cmd.Parameters.Add(new SqliteParameter("@Address", address));
                cmd.Parameters.Add(new SqliteParameter("@PatientID", id));
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

        public bool DeletePatient(int id)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "DELETE FROM Patients WHERE PatientID = @PatientID";
                cmd.Parameters.Add(new SqliteParameter("@PatientID", id));
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

        public DataTable SearchPatients(string keyword)
        {
            DataTable dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Patients WHERE Name LIKE @Keyword OR Phone LIKE @Keyword ORDER BY PatientID DESC";
            cmd.Parameters.Add(new SqliteParameter("@Keyword", $"%{keyword}%"));
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public bool IsDuplicate(string name)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Patients WHERE Name = @Name";
            cmd.Parameters.Add(new SqliteParameter("@Name", name));
            var result = cmd.ExecuteScalar();
            return result != null && Convert.ToInt32(result) > 0;
        }
    }
}
