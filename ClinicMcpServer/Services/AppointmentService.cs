using System.ComponentModel;
using System.Data;
using ClinicMcpServer.Data;
using ModelContextProtocol.Server;

namespace ClinicMcpServer.Services
{
    [McpServerToolType]
    public class AppointmentService
    {
        [McpServerTool]
        public string GetAllAppointments()
        {
            var repo = new AppointmentRepository();
            var dt = repo.ListAppointments();
            return DataTableToString(dt);
        }

        [McpServerTool]
        public string GetAppointmentById([Description("Appointment ID")] int id)
        {
            var repo = new AppointmentRepository();
            var row = repo.GetAppointment(id);
            if (row == null) return "Appointment not found";
            return $"ID: {row["AppointmentID"]}, PatientID: {row["PatientID"]}, DoctorID: {row["DoctorID"]}, Date: {row["AppointmentDate"]}, Status: {row["Status"]}";
        }

        [McpServerTool]
        public string AddAppointment(
            [Description("Patient ID")] int patientId,
            [Description("Doctor ID")] int doctorId,
            [Description("Appointment date (yyyy-MM-dd HH:mm)")] string date,
            [Description("Status")] string status,
            [Description("Notes")] string notes)
        {
            var repo = new AppointmentRepository();
            if (DateTime.TryParse(date, out var dt))
                return repo.AddAppointment(patientId, doctorId, dt, status, notes) ? "Appointment added successfully" : "Failed to add appointment";
            return "Invalid date format";
        }

        [McpServerTool]
        public string UpdateAppointmentStatus(
            [Description("Appointment ID")] int id,
            [Description("New status")] string status)
        {
            var repo = new AppointmentRepository();
            return repo.UpdateAppointmentStatus(id, status) ? "Status updated successfully" : "Failed to update status";
        }

        [McpServerTool]
        public string DeleteAppointment([Description("Appointment ID")] int id)
        {
            var repo = new AppointmentRepository();
            return repo.DeleteAppointment(id) ? "Appointment deleted successfully" : "Failed to delete appointment";
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
