# ระบบจัดการคลินิก (Clinic Management System)

โปรแกรมจัดการคลินิกพัฒนาด้วย C# WinForms เชื่อมต่อฐานข้อมูล 3 ฐานข้อมูล (SQLite) พร้อม MCP Server สำหรับ AI Agent

---

## 1. สคริปต์คลิปวิดีโออธิบายการทำงาน (5-10 นาที)

### เปิดตัว (1 นาที)
```
สวัสดีครับ/คะ วันนี้จะมานำเสนอโปรแกรมระบบจัดการคลินิก
พัฒนาด้วยภาษา C# บน .NET 8 ใช้ Windows Forms เป็น UI
เชื่อมต่อฐานข้อมูล 3 ฐานข้อมูล ได้แก่
- ฐานข้อมูลผู้ป่วย (patients.db)
- ฐานข้อมูลแพทย์ (doctors.db)
- ฐานข้อมูลการนัดหมาย (appointments.db)
```

### สาธิตการทำงาน (5-6 นาที)
**หน้าหลัก:**
- แสดงเมนู 3 รายการ: จัดการผู้ป่วย, จัดการแพทย์, จัดการนัดหมาย
- แสดงปุ่มสถานะสี (เขียว = เชื่อมต่อสำเร็จ)

**จัดการผู้ป่วย:**
- เพิ่มผู้ป่วยใหม่ (ชื่อ, อายุ, โทรศัพท์, ที่อยู่)
- แสดงรายการใน DataGridView
- คลิกแถวเพื่อแก้ไขข้อมูล
- ลบข้อมูล (มี MessageBox ยืนยันทุกครั้ง)
- ค้นหาผู้ป่วยตามชื่อหรือเบอร์โทร

**จัดการแพทย์:**
- เพิ่มแพทย์ (ชื่อ, ความเชี่ยวชาญ, โทรศัพท์, แผนก)
- แก้ไข/ลบ ข้อมูลแพทย์
- กรองตามความเชี่ยวชาญ (เช่น อายุรกรรม, ศัลยกรรม)

**จัดการนัดหมาย:**
- เลือกผู้ป่วยจาก ComboBox (ดึงจาก patients.db)
- เลือกแพทย์จาก ComboBox (ดึงจาก doctors.db)
- กำหนดวัน-เวลานัดหมาย
- เลือกสถานะ (รอดำเนินการ, ยืนยันแล้ว, ยกเลิก)
- กรองตามวันที่หรือสถานะ
- แสดงชื่อผู้ป่วยและแพทย์ใน DataGridView (JOIN ข้าม database)

### อธิบายผลลัพธ์ (1 นาที)
```
โปรแกรมสามารถจัดการข้อมูลคลินิกได้ครบวงจร
เชื่อม 3 ฐานข้อมูลแยกกัน แต่แสดงผลรวมได้ผ่าน JOIN query
มีการตรวจสอบและยืนยันทุกครั้งก่อน CRUD
รองรับการค้นหาและกรองข้อมูล
```

---

## 2. อธิบาย Code

### โครงสร้างโปรแกรม
```
ClinicManagement/
├── ClinicWinForms/           -- WinForms App (หลัก)
│   ├── frmMain.cs             -- เมนูหลัก + สถานะ DB
│   ├── frmPatients.cs         -- CRUD ผู้ป่วย
│   ├── frmDoctors.cs          -- CRUD แพทย์
│   ├── frmAppointments.cs    -- CRUD นัดหมาย
│   └── Data/
│       ├── DatabaseConfig.cs       -- ตั้งค่า DB (sqlite/sqlserver/mysql)
│       ├── DbConnectionFactory.cs  -- สร้าง connection ตาม DB type
│       ├── DatabaseInitializer.cs  -- สร้างตาราง + seed data
│       ├── PatientRepository.cs    -- SQL ผู้ป่วย
│       ├── DoctorRepository.cs     -- SQL แพทย์
│       └── AppointmentRepository.cs -- SQL นัดหมาย + ATTACH DB
├── ClinicMcpServer/            -- MCP Server (Bonus)
│   └── Services/
│       ├── PatientService.cs  -- MCP tools ผู้ป่วย
│       ├── DoctorService.cs   -- MCP tools แพทย์
│       └── AppointmentService.cs -- MCP tools นัดหมาย
└── Scripts/
    ├── sqlite/setup-databases.sql
    ├── sqlserver/setup-databases.sql
    └── mysql/setup-databases.sql
```

### ส่วนเชื่อมต่อฐานข้อมูล (DatabaseConfig.cs)
```csharp
public static class DatabaseConfig
{
    public const string DB_TYPE = "sqlite";  // เปลี่ยนเป็น sqlserver หรือ mysql ได้

    public static string GetConnectionString(string dbName)
    {
        return DB_TYPE switch
        {
            "sqlite" => $"Data Source=...{dbName}.db",
            "sqlserver" => $"Data Source=localhost\\SQLEXPRESS; Database={dbName};...",
            "mysql" => $"Server=localhost;Database={dbName};...",
        };
    }
}
```
- ใช้ pattern Strategy สลับระหว่าง SQLite / SQL Server / MySQL
- แก้แค่ DB_TYPE ตัวเดียวทั้งโปรแกรมเปลี่ยน

### สร้าง Connection (DbConnectionFactory.cs)
```csharp
public static IDbConnection CreateConnection(string connectionString)
{
    return DatabaseConfig.DB_TYPE switch
    {
        "sqlite" => new SqliteConnection(connectionString),
        ...
    };
}
```
- คืนค่าเป็น `IDbConnection` interface ไม่ใช่ class จริง
- ทำให้สลับ database ได้ไม่ต้องแก้โค้ดทุกจุด

### จัดการข้อมูล (Repository Pattern)
```csharp
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
```
- ใช้ `using` statement ทุกครั้ง ป้องกัน memory leak
- ใช้ `IDbCommand`, `IDataReader` (interface) เชื่อมได้ทุก DB

### เชื่อม 3 ฐานข้อมูล (AppointmentRepository.cs)
SQLite ไม่สนับสนุน JOIN ข้ามไฟล์โดยตรง ใช้ `ATTACH DATABASE`:
```csharp
private void AttachDatabases(IDbConnection conn)
{
    using var cmd = conn.CreateCommand();
    cmd.CommandText = "ATTACH DATABASE 'patients.db' AS patients;";
    cmd.ExecuteNonQuery();
    cmd.CommandText = "ATTACH DATABASE 'doctors.db' AS doctors;";
    cmd.ExecuteNonQuery();
}
```
จากนั้น JOIN ผ่าน alias:
```sql
SELECT a.*, p.Name AS PatientName, d.Name AS DoctorName
FROM Appointments a
LEFT JOIN patients.Patients p ON a.PatientID = p.PatientID
LEFT JOIN doctors.Doctors d ON a.DoctorID = d.DoctorID
```

### ฟังก์ชันสำคัญ
- **CRUD ครบทุกฟอร์ม:** เพิ่ม, แสดง, ค้นหา, แก้ไข, ลบ
- **Transaction:** ทุก INSERT/UPDATE/DELETE ใช้ `BeginTransaction()` + `Commit/Rollback`
- **Validation:** ตรวจสอบช่องว่าง, ตัวเลข, ซ้ำกัน
- **MessageBox ยืนยัน:** ทุกปุ่ม Add/Update/Delete มี Yes/No
- **Multi-database:** รองรับ SQLite (default), SQL Server, MySQL

### MCP Server (Bonus)
```csharp
[McpServerToolType]
public class PatientService
{
    [McpServerTool]
    public string GetAllPatients() { ... }

    [McpServerTool]
    public string AddPatient(string name, int age, ...) { ... }
}
```
- ใช้ NuGet `ModelContextProtocol`
- Expose methods เป็น tools ให้ AI agent เรียกใช้
- รันแบบ stdio transport เชื่อมกับ Claude Desktop / Windsurf

---

## 3. ไฟล์ที่ต้องส่ง

### สร้าง ZIP จากโฟลเดอร์นี้:
```
ClinicManagement/
├── ClinicManagement.sln
├── ClinicWinForms/
│   ├── ClinicWinForms.csproj
│   ├── Program.cs
│   ├── frmMain.cs, frmMain.Designer.cs
│   ├── frmPatients.cs, frmPatients.Designer.cs
│   ├── frmDoctors.cs, frmDoctors.Designer.cs
│   ├── frmAppointments.cs, frmAppointments.Designer.cs
│   └── Data/
│       ├── DatabaseConfig.cs
│       ├── DbConnectionFactory.cs
│       ├── DatabaseInitializer.cs
│       ├── PatientRepository.cs
│       ├── DoctorRepository.cs
│       └── AppointmentRepository.cs
├── ClinicMcpServer/ (Bonus)
│   ├── ClinicMcpServer.csproj
│   ├── Program.cs
│   └── Services/
│       ├── PatientService.cs
│       ├── DoctorService.cs
│       └── AppointmentService.cs
└── Scripts/
    ├── sqlite/setup-databases.sql, reset-databases.sql
    ├── sqlserver/setup-databases.sql, reset-databases.sql
    └── mysql/setup-databases.sql, reset-databases.sql
```

### คำสั่งสร้าง ZIP:
```powershell
Compress-Archive -Path "C:\Users\xray\ClinicManagement\ClinicManagement.sln", `
    "C:\Users\xray\ClinicManagement\ClinicWinForms", `
    "C:\Users\xray\ClinicManagement\ClinicMcpServer", `
    "C:\Users\xray\ClinicManagement\Scripts" `
    -DestinationPath "C:\Users\xray\ClinicManagement.zip"
```

---

## หมายเหตุสำหรับการสอน/อธิบาย

**จุดเด่นที่ควรเน้นในคลิป:**
1. โชว์ปุ่มสถานะสีเขียว (เชื่อมต่อ 3 DB สำเร็จ)
2. เปิดโฟลเดอร์ Databases โชว์ 3 ไฟล์ .db
3. อธิบายว่าแต่ละฟอร์มเชื่อม DB คนละตัว
4. โชว์ Appointment ที่ JOIN ชื่อผู้ป่วย+แพทย์ จากอีก 2 DB
5. กล่าวถึง MCP Server ว่าเป็น bonus ให้ AI เรียกใช้ได้

**คำถามที่อาจถูกถาม:**
- Q: ทำไมใช้ SQLite ไม่ใช้ SQL Server?
  A: SQLite ไม่ต้องติดตั้ง server รันได้ทันที แต่รองรับ SQL Server/MySQL ด้วยการเปลี่ยน config
- Q: เชื่อม 3 DB ยังไง?
  A: ใช้ ATTACH DATABASE ใน SQLite หรือใช้ connection string คนละ database ใน SQL Server
- Q: MCP คืออะไร?
  A: Model Context Protocol ของ Anthropic ให้ AI agent เชื่อมโปรแกรมภายนอกได้
