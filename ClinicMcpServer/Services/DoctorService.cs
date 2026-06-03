using System.ComponentModel;
using System.Data;
using ClinicMcpServer.Data;
using ModelContextProtocol.Server;

namespace ClinicMcpServer.Services
{
    [McpServerToolType]
    public class DoctorService
    {
        [McpServerTool]
        public string GetAllDoctors()
        {
            var repo = new DoctorRepository();
            var dt = repo.ListDoctors();
            return DataTableToString(dt);
        }

        [McpServerTool]
        public string GetDoctorById([Description("Doctor ID")] int id)
        {
            var repo = new DoctorRepository();
            var row = repo.GetDoctor(id);
            if (row == null) return "Doctor not found";
            return $"ID: {row["DoctorID"]}, Name: {row["Name"]}, Specialty: {row["Specialty"]}, Phone: {row["Phone"]}, Department: {row["Department"]}";
        }

        [McpServerTool]
        public string AddDoctor(
            [Description("Doctor name")] string name,
            [Description("Specialty")] string specialty,
            [Description("Phone number")] string phone,
            [Description("Department")] string department)
        {
            var repo = new DoctorRepository();
            return repo.AddDoctor(name, specialty, phone, department) ? "Doctor added successfully" : "Failed to add doctor";
        }

        [McpServerTool]
        public string UpdateDoctor(
            [Description("Doctor ID")] int id,
            [Description("Doctor name")] string name,
            [Description("Specialty")] string specialty,
            [Description("Phone number")] string phone,
            [Description("Department")] string department)
        {
            var repo = new DoctorRepository();
            return repo.UpdateDoctor(id, name, specialty, phone, department) ? "Doctor updated successfully" : "Failed to update doctor";
        }

        [McpServerTool]
        public string DeleteDoctor([Description("Doctor ID")] int id)
        {
            var repo = new DoctorRepository();
            return repo.DeleteDoctor(id) ? "Doctor deleted successfully" : "Failed to delete doctor";
        }

        private static string DataTableToString(DataTable dt)
        {
            if (dt.Rows.Count == 0) return "No records found";
            var lines = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                var items = row.ItemArray.Select(x => x?.ToString() ?? "");
                lines.Add(string.Join(" | ", items));
            }
            return string.Join("\n", lines);
        }
    }
}
