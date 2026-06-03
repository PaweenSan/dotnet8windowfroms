using System.ComponentModel;
using System.Data;
using ClinicMcpServer.Data;
using ModelContextProtocol.Server;

namespace ClinicMcpServer.Services
{
    [McpServerToolType]
    public class PatientService
    {
        [McpServerTool]
        public string GetAllPatients()
        {
            var repo = new PatientRepository();
            var dt = repo.ListPatients();
            return DataTableToString(dt);
        }

        [McpServerTool]
        public string GetPatientById([Description("Patient ID")] int id)
        {
            var repo = new PatientRepository();
            var row = repo.GetPatient(id);
            if (row == null) return "Patient not found";
            return $"ID: {row["PatientID"]}, Name: {row["Name"]}, Age: {row["Age"]}, Phone: {row["Phone"]}, Address: {row["Address"]}";
        }

        [McpServerTool]
        public string AddPatient(
            [Description("Patient name")] string name,
            [Description("Age")] int age,
            [Description("Phone number")] string phone,
            [Description("Address")] string address)
        {
            var repo = new PatientRepository();
            return repo.AddPatient(name, age, phone, address) ? "Patient added successfully" : "Failed to add patient";
        }

        [McpServerTool]
        public string UpdatePatient(
            [Description("Patient ID")] int id,
            [Description("Patient name")] string name,
            [Description("Age")] int age,
            [Description("Phone number")] string phone,
            [Description("Address")] string address)
        {
            var repo = new PatientRepository();
            return repo.UpdatePatient(id, name, age, phone, address) ? "Patient updated successfully" : "Failed to update patient";
        }

        [McpServerTool]
        public string DeletePatient([Description("Patient ID")] int id)
        {
            var repo = new PatientRepository();
            return repo.DeletePatient(id) ? "Patient deleted successfully" : "Failed to delete patient";
        }

        [McpServerTool]
        public string SearchPatients([Description("Search keyword")] string keyword)
        {
            var repo = new PatientRepository();
            var dt = repo.SearchPatients(keyword);
            return DataTableToString(dt);
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
