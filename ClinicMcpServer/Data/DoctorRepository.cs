using System.Data;
using Microsoft.Data.Sqlite;

namespace ClinicMcpServer.Data
{
    public class DoctorRepository
    {
        private readonly string _connString;

        public DoctorRepository()
        {
            _connString = DatabaseConfig.GetConnectionString("doctors");
        }

        public DataTable ListDoctors()
        {
            DataTable dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Doctors ORDER BY DoctorID DESC";
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public DataRow? GetDoctor(int id)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";
            cmd.Parameters.Add(new SqliteParameter("@DoctorID", id));
            using var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public bool AddDoctor(string name, string specialty, string phone, string department)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "INSERT INTO Doctors(Name, Specialty, Phone, Department) VALUES(@Name, @Specialty, @Phone, @Department)";
                cmd.Parameters.Add(new SqliteParameter("@Name", name));
                cmd.Parameters.Add(new SqliteParameter("@Specialty", specialty));
                cmd.Parameters.Add(new SqliteParameter("@Phone", phone));
                cmd.Parameters.Add(new SqliteParameter("@Department", department));
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

        public bool UpdateDoctor(int id, string name, string specialty, string phone, string department)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "UPDATE Doctors SET Name = @Name, Specialty = @Specialty, Phone = @Phone, Department = @Department WHERE DoctorID = @DoctorID";
                cmd.Parameters.Add(new SqliteParameter("@Name", name));
                cmd.Parameters.Add(new SqliteParameter("@Specialty", specialty));
                cmd.Parameters.Add(new SqliteParameter("@Phone", phone));
                cmd.Parameters.Add(new SqliteParameter("@Department", department));
                cmd.Parameters.Add(new SqliteParameter("@DoctorID", id));
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

        public bool DeleteDoctor(int id)
        {
            using var conn = DbConnectionFactory.CreateConnection(_connString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = "DELETE FROM Doctors WHERE DoctorID = @DoctorID";
                cmd.Parameters.Add(new SqliteParameter("@DoctorID", id));
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
    }
}
